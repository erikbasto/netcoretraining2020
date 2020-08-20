using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AT.Model.Common;

namespace AT.DataAccess.Data
{
    public class ATDbContext:DbContext
    {
        private readonly IConfiguration configuration;

        public ATDbContext(){
            
        }

        public   ATDbContext(DbContextOptions options, 
        IConfiguration configuration): base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public virtual DbSet<User> Users{get;set;}
        public virtual DbSet<Product> Products{get;set;}
        public virtual DbSet<ProductType> ProductTypes{get;set;}
    }
}