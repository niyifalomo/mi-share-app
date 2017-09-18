using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Model;
using System.Data.Entity;
using Mi_Share.Data.Configuration;

namespace Mi_Share.Data
{
    public class Mi_ShareEntities : DbContext
    {
        public Mi_ShareEntities() : base("Mi_ShareDB"){ }

        public DbSet<User> Users { get; set;}
        public DbSet<Item> Items { get; set; }
        public DbSet<CollectionAccess> CollectionAccesses { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Category> Categories { get; set;}

        public virtual int Commit()
        {
            return base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Mi_ShareEntities>(null);

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ItemConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CollectionAccessConfiguration());
            modelBuilder.Configurations.Add(new RequestConfiguration());
            modelBuilder.Configurations.Add(new LoanConfiguration());
            
          
        }

    }


}
