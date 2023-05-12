
using Microsoft.AspNetCore.Mvc;
using Scith.Repositories;
using Scith.Entities;

namespace Scith.Controllers

{
    [ApiController]
    [Route("items")]
    //implements
    public class ItemsController : ControllerBase
    {
        private readonly InterfaceItemsRepository repository;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(InterfaceItemsRepository repository, ILogger<ItemsController> logger)
        {
            //new instance, generates new GUID
            this.repository = repository;
            this.logger = logger;
            logger.LogInformation("ItemsController created");
        }

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }

        [HttpGet("{id}")]
        //actionresult allows for returning multiple types so we can return NotFound result
        public ActionResult<Item> GetItem(Guid id)
        {
            logger.LogInformation("Received request for item with ID {Id}", id);
            var item = repository.GetItem(id);
            logger.LogInformation("Retrieved item with ID {Id}: {@Item}", id, item);
            if (item is null)
            {
                return NotFound();
            }
            return item;
        }

    }


}