using System;
using System.Collections.Generic;
using AT.IDataAccess.IRepositoryPattern;
using AT.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController: ControllerBase
    {
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductTypeController(IRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductType>> GetAll()
        {
            return Ok(_productTypeRepository.GetAll());
        }

        [HttpGet("Id")]
        public ActionResult<ProductType> GetById(int Id)
        {
            return Ok(_productTypeRepository.GetById(Id));
        }

        [HttpPost]
        public ActionResult<ProductType> NewProductType(ProductType productType)
        {
            return Ok(_productTypeRepository.Create(productType));
        }

        [HttpDelete]
        public ActionResult DeleteProductType(ProductType productType){
            _productTypeRepository.Delete(productType);
            return Ok();
        }

        [HttpPut]
        public ActionResult<ProductType> EditProductType(ProductType productType)
        {
            try
            {
                productType = _productTypeRepository.Update(productType);
                if(productType!=null)
                    return Ok(productType);
                else
                    return NotFound();
            }
            catch(ArgumentNullException )
            {
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}