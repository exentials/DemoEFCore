using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoEFCore
{
    public class TableDetail
    {
        public int Id { get; set; }
        public TableMaster TableMaster { get; set; }
        public DateTime InsertDate { get; set; }
        public string DetailCode { get; set; }
        public decimal Quantity { get; set; }
    }

    public class TableDetailConfiguration : IEntityTypeConfiguration<TableDetail>
    {
        public void Configure(EntityTypeBuilder<TableDetail> builder)
        {
            builder.HasIndex(p => p.DetailCode);
        }
    }
}