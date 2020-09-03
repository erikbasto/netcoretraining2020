using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AT.IModel.Common;

namespace AT.Model.Common
{
    public class Product:IEntity
    {
       public int Id{get;set;}
       public string ProductName{get;set;}

       #region ProductType FK
        public int IdProductType{get;set;}
        
        [ForeignKey("IdProductType")]
        public ProductType ProductType {get;set;}
       #endregion

       public ICollection<ProductStore> ProductStores{get;set;}
    }
}