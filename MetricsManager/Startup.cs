using System;
using AutoMapper;
using FluentMigrator.Runner;
using MediatR;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Mapping;
using MetricsManager.Quartz;
using MetricsManager.Quartz.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();

            services.AddMediatR(typeof(Startup));

            var mapperConfiguration = new MapperConfiguration(mp =>
                mp.AddProfile(new MapperProfile())).CreateMapper();
            services.AddSingleton(mapperConfiguration);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                        .AddSQLite()                                        // добавляем поддержку SQLite
                        .WithGlobalConnectionString(Configuration
                            .GetConnectionString("default"))                // устанавливаем строку подключения
                        .ScanIn(typeof(Startup).Assembly).For.Migrations()  // подсказываем где искать классы с миграциями
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(typeof(CpuMetricJob), "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();

            services.AddHttpClient();

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp(0);
        }
    }
}
