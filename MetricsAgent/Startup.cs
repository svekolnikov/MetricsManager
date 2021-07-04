using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.Mapping;
using MetricsAgent.Quartz;
using MetricsAgent.Quartz.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAgent
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
            services.AddSingleton<IDotNetMetricRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricRepository, RamMetricsRepository>();

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
            services.AddSingleton(new JobSchedule(typeof(CpuMetricJob),"0/5 * * * * ?")); // запускать каждые 5 секунд

            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton(new JobSchedule(typeof(DotNetMetricJob), "0/5 * * * * ?"));

            services.AddSingleton<HddMetricJob>();
            services.AddSingleton(new JobSchedule(typeof(HddMetricJob), "0/5 * * * * ?"));

            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(typeof(NetworkMetricJob), "0/5 * * * * ?"));

            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(typeof(RamMetricJob), "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();
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

            // запускаем миграции
            migrationRunner.MigrateUp(1);
        }
    }
}

