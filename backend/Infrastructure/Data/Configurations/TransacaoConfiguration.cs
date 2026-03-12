using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();
        builder.Property(t => t.Descricao).IsRequired().HasMaxLength(400);
        builder.Property(t => t.Valor).IsRequired().HasPrecision(18, 2);
        builder.Property(t => t.Tipo).IsRequired().HasConversion<int>();
        builder.Property(t => t.CategoriaId).IsRequired();
        builder.Property(t => t.PessoaId).IsRequired();
    }
}
