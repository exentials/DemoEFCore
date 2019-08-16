using System;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DemoEFCoreContext>();
            optionsBuilder.UseNpgsql($"Server=192.168.74.10;Port=5432;Database=DemoEFCore;User Id=postgres;Password = dbpass;");

            using var db = new DemoEFCoreContext(optionsBuilder.Options);
            db.Database.EnsureDeleted();    // ensure to delete the database if present
            db.Database.EnsureCreated();    // ensure the database creation

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
        }
    }
}
