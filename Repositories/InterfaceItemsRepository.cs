
using Scith.Entities;
//this interface defines the contract for any classes that implement it

public interface InterfaceItemsRepository
{
    //task represents an asynchronous operation that can return a value
    Task<Item> GetItemAsync(Guid id);
    Task<IEnumerable<Item>> GetItemsAsync();

    Task CreateItemAsync(Item item);
    Task UpdateItemAsync(Item item);
    Task DeleteItemAsync(Guid id);
}