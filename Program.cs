using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.CommandLineUtils;

namespace DemoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            var dbHostOption = app.Option("--db-host <host>", "database host", CommandOptionType.SingleValue);
            var dbPortOption = app.Option("--db-port <port>", "database host port", CommandOptionType.SingleValue);
            var dbNameOption = app.Option("--db-name <name>", "database name", CommandOptionType.SingleValue);
            var dbPasswordOption = app.Option("--db-pass <password>", "database password", CommandOptionType.SingleValue);
            app.HelpOption("-? | -h | --help");
            app.OnExecute(() =>
            {
                string dbHost = dbHostOption.HasValue() ? dbHostOption.Value() : string.Empty;
                string dbPort = dbPortOption.HasValue() ? dbPortOption.Value() : "5432";
                string dbName = dbNameOption.HasValue() ? dbNameOption.Value() : "DemoEFCore";
                string dbPassword = dbPasswordOption.HasValue() ? dbPasswordOption.Value() : "dbpass";
                if (dbHost == string.Empty)
                {
                    app.Error.WriteLine("Database host option is missing");
                    return 1;
                }

                var optionsBuilder = new DbContextOptionsBuilder<DemoEFCoreContext>();
                optionsBuilder.UseNpgsql($"Server={dbHost};Port={dbPort};Database={dbName};User Id=postgres;Password={dbPassword};");

                using var db = new DemoEFCoreContext(optionsBuilder.Options);
                // ensure to delete the database if present
                if (db.Database.EnsureDeleted())
                {
                    Console.WriteLine($"Database {dbName} deleted.");
                }
                // ensure the database creation
                if (db.Database.EnsureCreated())
                {
                    Console.WriteLine($"Database {dbName} created.");
                }

                // add some data to Master and Detail
                db.Add(new TableMaster
                {
                    InsertDate = DateTime.Now,
                    MasterKey = "KEY001",
                    Details = new TableDetail[] {
                    new TableDetail {
                        InsertDate = DateTime.Now,
                        DetailCode = "Code001",
                        Quantity = 1
                    },
                    new TableDetail {
                        InsertDate = DateTime.Now,
                        DetailCode = "Code002",
                        Quantity = 9
                    }
                }
                });
                db.SaveChanges();   // save the changes

                return 0;
            });
            app.Execute(args);
        }
    }
}
