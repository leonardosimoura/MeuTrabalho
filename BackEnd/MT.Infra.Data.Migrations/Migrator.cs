using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.Data.Migrations
{
    internal class Migrator
    {
        string connectionString;
        bool testing = false;
        public Migrator(string connectionString, bool testing = false)
        {
            this.connectionString = connectionString;
            this.testing = testing;
        }
        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }

            public string ProviderSwitches => "";
        }
        public void Migrate(Action<IMigrationRunner> runnerAction)
        {
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 0 };
            var assembly = Assembly.GetExecutingAssembly();

            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var migrationContext = new RunnerContext(announcer)
            {
#if DEBUG
                // will create testdata 
                Profile = "development"
#endif
            };


            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2014ProcessorFactory();
            var processor = factory.Create(this.connectionString, announcer, options);

            var runner = new MigrationRunner(assembly, migrationContext, processor);

            runnerAction(runner);
        }
    }

    public class DatabaseConfig
    {
        public static void MigrateDatabase(string connectionString, bool testing = false)
        {
            try
            {
                var migrator = new Migrator(connectionString, testing);



                migrator.Migrate(runner => runner.MigrateUp());
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
