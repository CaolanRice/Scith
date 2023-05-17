using Scith.DTOs;
using Scith.Entities;

namespace Scith
{
    //this class defines the extensions for the Item class. This allows Item to convert itself into a DTO object without modifying it's own implememtation
    public static class Extensions
    {
        //receives an item and returns it as a DTO
        public static ItemDTO AsDTO(this Item item)
        {
            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}
