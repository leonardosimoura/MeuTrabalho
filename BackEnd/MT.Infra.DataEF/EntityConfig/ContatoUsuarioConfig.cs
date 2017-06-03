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
    public class ContatoUsuarioConfig : EntityTypeConfiguration<ContatoUsuario>
    {
        public ContatoUsuarioConfig()
        {
            HasKey(k => k.UsuarioId);
            Property(p => p.UsuarioId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Nome).IsRequired();
            Property(p => p.Empresa).IsRequired();
            Property(p => p.TelefoneCelular.Numero)
                .HasColumnName("NumeroCelular");
            Property(p => p.TelefoneCelular.CodigoDDD)
               .HasColumnName("CodigoDDDCelular");
            Property(p => p.TelefoneFixo.Numero)
               .HasColumnName("NumeroFixo");
            Property(p => p.TelefoneFixo.CodigoDDD)
               .HasColumnName("CodigoDDDFixo");
               
        }
    }
}
