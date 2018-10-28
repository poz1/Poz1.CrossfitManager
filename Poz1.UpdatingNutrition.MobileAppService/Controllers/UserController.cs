using System;
using Microsoft.AspNetCore.Mvc;

using Poz1.UpdatingNutrition.Models;

namespace Poz1.UpdatingNutrition.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserRepository UserRepository;

        public UserController(IUserRepository itemRepository)
        {
            UserRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(UserRepository.GetAll());
        }

        [HttpGet("{id}")]
        public User GetUser(string id)
        {
            User item = UserRepository.Get(id);
            return item;
        }

        [HttpPost("{login}")]
        public IActionResult Login([FromBody]pwd lol)
        {
            var res = UserRepository.Login(lol.Email, lol.Password);
            if (res)
                return BadRequest("Wrong Password!");
            else
                return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody]User item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                UserRepository.Add(item);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] User item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                UserRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            UserRepository.Remove(id);
        }
    }
}
