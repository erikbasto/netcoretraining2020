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
    public class UserController: ControllerBase
    {
        private readonly IRepository<User> userRepository;

        public UserController(IRepository<User> UserRepository)
        {
            userRepository = UserRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(userRepository.GetAll());
        }

        [HttpGet("Id")]
        public ActionResult<User> GetById(int Id)
        {
            return Ok(userRepository.GetById(Id));
        }

        [HttpPost]
        public ActionResult<User> NewUser(User user)
        {
            return Ok(userRepository.Create(user));
        }

        [HttpDelete]
        public ActionResult DeleteUser(User user){
            userRepository.Delete(user);
            return Ok();
        }

        [HttpPut]
        public ActionResult<User> EditUser(User user)
        {
            try
            {
                return Ok(userRepository.Update(user));
            }
            catch(ArgumentNullException )
            {
                return BadRequest();
            }
            catch(Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}