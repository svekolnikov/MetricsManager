using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace MetricsManager.DAL.Migrations
{
    [Migration(0)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("agents")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Url").AsString();

            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("AgentId").AsInt32();

            Create.Table("dotnetmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("AgentId").AsInt32();

            Create.Table("hddmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("AgentId").AsInt32();

            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("AgentId").AsInt32();

            Create.Table("rammetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("AgentId").AsInt32();
        }

        public override void Down()
        {
            Delete.Table("agents");
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
        }
    }
}
