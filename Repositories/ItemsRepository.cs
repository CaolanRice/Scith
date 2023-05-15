using Scith.Entities;

namespace Scith.Repositories
{
    //implements
    public class ItemsRepository : InterfaceItemsRepository
    {
        private readonly List<Item> items = new(){
            new Item { Id=Guid.NewGuid(), Name = "Health potion", Price = 5, CreatedDate = DateTimeOffset.Now },
            new Item { Id=Guid.NewGuid(), Name = "Bronze Dagger", Price = 25, CreatedDate = DateTimeOffset.Now },
            new Item { Id=Guid.NewGuid(), Name = "Wooden Shield", Price = 30, CreatedDate = DateTimeOffset.Now }
        };

        //IEnumberable = interface that can return a collection
        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return item;
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            //search list for 1st element whose id matches the param item obj, then replace old item with updated
            var index = items.FindIndex(storedItem => storedItem.Id == item.Id);
            items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            var index = items.FindIndex(storedItem => storedItem.Id == id);
            items.RemoveAt(index);
        }
    }
}