using System;
using System.Collections.Generic;
using System.Linq;
using AT.DataAccess.Data;
using AT.IDataAccess.IRepositoryPattern;
using AT.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace AT.DataAccess.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly ATDbContext context;

        public UserRepository(ATDbContext context){
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public User Create(User Entity)
        {
           context.Users.Add(Entity);
           context.SaveChanges();
           return Entity;
        }

        public void Delete(User Entity)
        {
            var userToBeDeleted = context.Users.Find(Entity.Id);
            if(userToBeDeleted!=null){
                context.Users.Remove(userToBeDeleted);
                context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(int Id)
        {
            return context.Users.Find(Id);
        }

        public User Update(User Entity)
        {
            if(Entity==null)
                throw new ArgumentNullException(nameof(Entity));

            var userToBeUpdated = context.Users.Find(Entity.Id);
            if(userToBeUpdated!=null)
            {
                context.Users.Update(Entity);
                context.SaveChanges();
                return Entity;
            }
            return null;
        }
    }
}