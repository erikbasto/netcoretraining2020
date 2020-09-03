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
    public class  ProductController: ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductController(IRepository<Product> productRepository,
            IRepository<ProductType> productTypeRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpGet("Id")]
        public ActionResult<Product> GetById(int id)
        {
            return Ok(_productRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult<Product> NewProduct(Product product)
        {   
            try
            {               
                return Ok(_productRepository.Create(product));
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

        [HttpDelete]
        public ActionResult DeleteProduct(Product product){
            _productRepository.Delete(product);
            return Ok();
        }

        [HttpPut]
        public ActionResult<Product> EditProduct(Product product)
        {
            try
            {        
                 product = _productRepository.Update(product);
                if(product!=null)
                    return Ok(product);
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