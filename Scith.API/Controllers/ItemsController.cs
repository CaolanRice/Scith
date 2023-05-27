
using Microsoft.AspNetCore.Mvc;
using Scith.API.DTOs;
using Scith.API.Entities;

namespace Scith.API.Controllers

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
        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
        {
            //convert each item in repository to a DTO
            //first call GetItemsAsync and THEN select
            var items = (await repository.GetItemsAsync())
                            .Select(item => item.AsDTO());
            return items;
        }

        [HttpGet("{id}")]
        //actionresult allows for returning multiple types so we can return NotFound result
        public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDTO();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO itemDTO)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Price = itemDTO.Price,
                CreatedDate = DateTimeOffset.Now
            };
            await repository.CreateItemAsync(item);
            //action result, nameof specifies route values for GET request, id = route param, item.Id = created items id. convert to DTO
            //returns 201 status code + location header that specifies where newly created item can be found
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDTO itemDTO)
        {
            var storedItem = await repository.GetItemAsync(id);
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

            await repository.UpdateItemAsync(updatedItem);
            //convention to return no content when with PUT methods
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var storedItem = repository.GetItemAsync(id);
            if (storedItem == null)
            {
                return NotFound();
            }
            await repository.DeleteItemAsync(id);
            return NoContent();
        }

    }


}