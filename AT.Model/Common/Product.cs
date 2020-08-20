using AT.IModel.Common;

namespace AT.Model.Common
{
    public class Product:IEntity
    {
       public int Id{get;set;}
       public string ProductName{get;set;}

       #region ProductType FK
        public int IdProductType{get;set;}
        public ProductType ProductType {get;set;}
       #endregion
    }
}