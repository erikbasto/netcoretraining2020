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

        public Product Create(Product entity)
        {
            if(entity.IdProductType<=0)
            {
                throw new ArgumentNullException(nameof(entity.IdProductType));
            }
            entity.ProductType=null;

           context.Products.Add(entity);
           context.SaveChanges();
           return entity;
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

        public Product Update(Product entity)
        {
            if(entity==null)
                throw new ArgumentNullException(nameof(entity));

            if(entity.IdProductType<=0)
            {
                throw new ArgumentNullException(nameof(entity.IdProductType));
            }
            entity.ProductType=null;

            var item = context.Products.Find(entity.Id);
            if(item!=null)
            {
                context.Products.Update(entity);
                context.SaveChanges();
                return entity;
            }
            return null;
        }
    }
}