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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType {Id = 1, ProductTypeName = "Electronics"},
                new ProductType {Id = 2, ProductTypeName = "Vegetables"},
                new ProductType {Id = 3, ProductTypeName = "Pets"}
            );

            modelBuilder.Entity<ProductStore>()
                .HasKey(bc => new { bc.ProductId, bc.StoreId });  
            modelBuilder.Entity<ProductStore>()
                .HasOne(bc => bc.Product)
                .WithMany(b => b.ProductStores)
                .HasForeignKey(bc => bc.ProductId);  
            modelBuilder.Entity<ProductStore>()
                .HasOne(bc => bc.Store)
                .WithMany(c => c.ProductStores)
                .HasForeignKey(bc => bc.StoreId);
        }


        public virtual DbSet<User> Users{get;set;}
        public virtual DbSet<Product> Products{get;set;}
        public virtual DbSet<ProductType> ProductTypes{get;set;}
        public virtual DbSet<Store> Stores{get;set;}
        public virtual DbSet<ProductStore> ProductStores{get;set;}
    }
}