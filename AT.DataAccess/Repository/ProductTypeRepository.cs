using System;
using System.Collections.Generic;
using System.Linq;
using AT.DataAccess.Data;
using AT.IDataAccess.IRepositoryPattern;
using AT.Model.Common;

namespace AT.DataAccess.Repository
{
    public class ProductTypeRepository: IRepository<ProductType>
    {
        private readonly ATDbContext context;

        public ProductTypeRepository(ATDbContext context){
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public ProductType Create(ProductType Entity)
        {
           context.ProductTypes.Add(Entity);
           context.SaveChanges();
           return Entity;
        }

        public void Delete(ProductType Entity)
        {
            var item = context.ProductTypes.Find(Entity.Id);
            if(item!=null){
                context.ProductTypes.Remove(Entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<ProductType> GetAll()
        {
            return context.ProductTypes.ToList();
        }

        public ProductType GetById(int Id)
        {
            return context.ProductTypes.Find(Id);
        }

        // SaveOrUpdate strategy
        public ProductType Update(ProductType Entity)
        {
            if(Entity==null)
                throw new ArgumentNullException(nameof(Entity));

            var item = context.ProductTypes.Find(Entity.Id);
            if(item!=null)
            {
                context.ProductTypes.Update(Entity);
            }
            else
            {
                context.ProductTypes.Add(Entity);
            }
            context.SaveChanges();
            return Entity;
        }
    }
}