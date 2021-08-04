using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Dominio.Context.Mapping
{
    public class PeriodoDeFeriasMap : IEntityTypeConfiguration<PeriodoDeFerias>
    {
        public void Configure(EntityTypeBuilder<PeriodoDeFerias> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataInicial).IsRequired();
            builder.Property(x => x.DataFinal).IsRequired();
            builder.Property(x => x.TipoDePeriodoDeFerias).HasConversion(x => x.ToString(), x => (TipoDePeriodoDeFerias)Enum.Parse(typeof(TipoDePeriodoDeFerias), x));
            
            builder.HasOne(x => x.Ferias).WithMany(x => x.PeriodosDeFerias).HasForeignKey(x => x.FeriasId);
        }
    }
}