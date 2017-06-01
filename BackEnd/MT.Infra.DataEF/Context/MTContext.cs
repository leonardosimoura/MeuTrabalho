using MT.Domain.Models;
using MT.Infra.DataEF.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.DataEF.Context
{
    public class DbConnConfiguration : DbConfiguration
    {
        public DbConnConfiguration()
        {
            //SetExecutionStrategy(
            //    "System.Data.SqlClient",
            //    () => new SqlAzureExecutionStrategy(2, TimeSpan.FromSeconds(30)));

            //Work a round missing ddl
            SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
    [DbConfigurationType(typeof(DbConnConfiguration))]
    public class MTContext : DbContext
    {
        static MTContext()
        {
            Database.SetInitializer<MTContext>(null);
        }

        public MTContext() : base("DefaultConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = true;
            //this.Configuration.AutoDetectChangesEnabled = false;
            //Database.SetInitializer(new CreateDatabaseIfNotExists< ProfHelpContext>());
            //Database.Initialize(true);
        }

        public virtual IDbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(a => a.HasMaxLength(250).IsVariableLength().IsUnicode(false));
            //modelBuilder.Properties().Where(prop => prop.Name == prop.ReflectedType.Name + "Id")
            //    .Configure(config => config.IsKey());

            modelBuilder.Configurations.Add(new UsuarioConfig());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var retorno = base.SaveChanges();

            foreach (var item in ChangeTracker.Entries())
            {
                item.State = EntityState.Detached;
            }

            return retorno;
        }

        public override Task<int> SaveChangesAsync()
        {

            var retorno = base.SaveChangesAsync();

            retorno.ContinueWith((a) =>
            {
                foreach (var item in ChangeTracker.Entries())
                {
                    item.State = EntityState.Detached;
                }
            });

            return retorno;
        }
    }
}
