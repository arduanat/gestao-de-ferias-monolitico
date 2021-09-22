using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Dominio.Context.Mapping
{
    public class HomologacaoDeFeriasMap : IEntityTypeConfiguration<HomologacaoDeFerias>
    {
        public void Configure(EntityTypeBuilder<HomologacaoDeFerias> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SituacaoDasFerias).HasConversion(x => x.ToString(), x => (SituacaoDasFerias)Enum.Parse(typeof(SituacaoDasFerias), x));

            builder.HasOne(x => x.Ferias).WithOne(x => x.Homologacao).HasForeignKey<HomologacaoDeFerias>(x => x.FeriasId);
        }
    }
}
