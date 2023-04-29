using Scith.Entities;

namespace Scith.Repositories
{
    public class InMemItemsRepository{
        private readonly List<Item> items = new(){
            new Item { Id=Guid.NewGuid(), Name = "Health potion", Price = 5, CreatedDate = DateTimeOffset.Now },
            new Item { Id=Guid.NewGuid(), Name = "Bronze Dagger", Price = 25, CreatedDate = DateTimeOffset.Now },
            new Item { Id=Guid.NewGuid(), Name = "Wooden Shield", Price = 30, CreatedDate = DateTimeOffset.Now }
        };

        //interface that can return a collection
        public IEnumerable<Item> GetItems(){
            return items;
        }
        
        public Item GetItem(Guid id){
            //return item by id or default 
            return items.Where(item => item.Id == id).SingleOrDefault();
        }
    
    }   
}