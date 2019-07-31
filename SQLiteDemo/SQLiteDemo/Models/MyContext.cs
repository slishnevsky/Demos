using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
    public class MyContext : DbContext
    {
		public MyContext()
		{
			Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<CardHolder> CardHolders { get; set; }
        public DbSet<AccessCard> AccessCards { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
