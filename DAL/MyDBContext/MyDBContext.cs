using ENT.Ent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext() : base("name=MyDBContext")
        {
            Database.SetInitializer<MyDBContext>(new CreateDatabaseIfNotExists<MyDBContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Person> Areas { get; set; }
        public virtual DbSet<Country> Contry { get; set; }

    }
}
