
using Microsoft.AspNetCore.Mvc;
using Scith.Repositories;
using Scith.Entities;

namespace Scith.Controllers

{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase{
        private readonly InMemItemsRepository repository;

        public ItemsController(){
            repository = new InMemItemsRepository();
        }

        [HttpGet]
        public IEnumerable<Item> GetItems(){
            var items = repository.GetItems();
            return items;
        }
        [HttpGet("/{id}")]
        //actionresult allows for returning multiple types 
        public ActionResult<Item> GetItem(Guid id){
            var item = repository.GetItem(id);
            if (item is null){
                return NotFound();
            }
            return item;
        }

    }


}