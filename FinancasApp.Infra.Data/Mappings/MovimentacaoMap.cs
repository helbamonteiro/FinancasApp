using FinancasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Mappings
{
    public class MovimentacaoMap : IEntityTypeConfiguration<Movimentacao>
    {
        public void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            builder.ToTable("MOVIMENTACAO");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("ID");

            builder.Property(m => m.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Data)
                .HasColumnName("DATA")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(m => m.Valor)
                .HasColumnName("VALOR")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(m => m.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(m => m.Tipo)
                .HasColumnName("TIPO")
                .IsRequired();

            builder.Property(m => m.CategoriaId)
                .HasColumnName("CATEGORIAID")
                .IsRequired();

            builder.Property(m => m.UsuarioId)
                .HasColumnName("USUARIOID")
                .IsRequired();

            //Movimentação TEM 1 Categoria / Categoria TEM N Movimentações
            builder.HasOne(m => m.Categoria)
                .WithMany(c => c.Movimentacoes)
                .HasForeignKey(m => m.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            //Movimentação TEM 1 Usuário / Usuário TEM N Movimentações
            builder.HasOne(m => m.Usuario)
                .WithMany(u => u.Movimentacoes)
                .HasForeignKey(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
