using System;
using Microsoft.AspNetCore.Mvc;
using Poz1.UpdatingNutrition.Models;

namespace Poz1.UpdatingNutrition.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class WODController : Controller
    {

        private readonly IRepository<WOD> ItemRepository;

        public WODController(IRepository<WOD> itemRepository)
        {
            ItemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(ItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public WOD GetItem(string id)
        {
            WOD item = ItemRepository.Get(id);
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody]WOD item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                ItemRepository.Add(item);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] WOD item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                ItemRepository.Update(item);
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
            ItemRepository.Remove(id);
        }
    }
}
