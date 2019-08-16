using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoEFCore
{
    public class TableMaster
    {
        public int Id { get; set; }
        public string MasterKey { get; set; }
        public DateTime InsertDate { get; set; }

        public ICollection<TableDetail> Details { get; set; }
    }

    public class TableMasterConfiguration : IEntityTypeConfiguration<TableMaster>
    {
        public void Configure(EntityTypeBuilder<TableMaster> builder)
        {
            builder.HasAlternateKey(p => p.MasterKey);
        }
    }
}