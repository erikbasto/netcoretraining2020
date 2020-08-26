using System.Collections.Generic;
using AT.IModel.Common;

namespace AT.Model.Common
{
    public class Store:IEntity
    {
       public int Id{get;set;}
       public string Name{get;set;}
        public ICollection<ProductStore> ProductStores{get;set;}
    }
}