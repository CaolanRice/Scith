
using Microsoft.AspNetCore.Mvc;
using Scith.DTOs;
using Scith.Entities;

namespace Scith.Controllers

{
    [ApiController]
    [Route("items")]
    //implements
    public class ItemsController : ControllerBase
    {
        private readonly InterfaceItemsRepository repository;

        public ItemsController(InterfaceItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDTO> GetItems()
        {
            //convert each item in repository to a DTO
            var items = repository.GetItems().Select(item => item.AsDTO());
            return items;
        }

        [HttpGet("{id}")]
        //actionresult allows for returning multiple types so we can return NotFound result
        public ActionResult<ItemDTO> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDTO();
        }

    }


}