using MT.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.DataEF.EntityConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(k => k.UsuarioId);
            Property(p => p.UsuarioId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Nome).IsRequired();
            Property(p => p.Email.Endereco)
                .HasColumnName("Email").IsRequired();
            Property(p => p.Senha.CodigoAcesso)
                .HasColumnName("Senha").IsRequired();          
        }
    }
}
