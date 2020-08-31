using System;
using System.Collections.Generic;
using System.Linq;
using AT.DataAccess.Data;
using AT.IDataAccess.IRepositoryPattern;
using AT.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace AT.DataAccess.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ATDbContext context;

        public ProductRepository(ATDbContext context){
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public Product Create(Product Entity)
        {
           context.Products.Add(Entity);
           context.SaveChanges();
           return Entity;
        }

        public void Delete(Product Entity)
        {
            var item = context.Products.Find(Entity.Id);
            if(item!=null){
                context.Products.Remove(Entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetById(int Id)
        {
            return context.Products.Find(Id);
        }

        // SaveOrUpdate strategy
        public Product Update(Product Entity)
        {
            if(Entity==null)
                throw new ArgumentNullException(nameof(Entity));

            var item = context.Products.Find(Entity.Id);
            if(item!=null)
            {
                context.Products.Update(Entity);
            }
            else
            {
                context.Products.Add(Entity);
            }
            context.SaveChanges();
            return Entity;
        }
    }
}