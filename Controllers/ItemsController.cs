
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

        [HttpPost]
        public ActionResult<ItemDTO> CreateItem(CreateItemDTO itemDTO)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Price = itemDTO.Price,
                CreatedDate = DateTimeOffset.Now
            };
            repository.CreateItem(item);
            //action result, nameof specifies route values for GET request, id = route param, item.Id = created items id. convert to DTO
            //returns 201 status code + location header that specifies where newly created item can be found
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDTO());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDTO itemDTO)
        {
            var storedItem = repository.GetItem(id);
            if (storedItem == null)
            {
                return NotFound();
            }

            //create copy of storedItem WITH following properties modified for the new values
            Item updatedItem = storedItem with
            {
                Name = itemDTO.Name,
                Price = itemDTO.Price
            };

            repository.UpdateItem(updatedItem);
            //convention to return no content when with PUT methods
            return NoContent();
        }

    }


}