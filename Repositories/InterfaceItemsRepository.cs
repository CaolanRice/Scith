
using Scith.Entities;
//this interface defines the contract for any classes that implement it

public interface InterfaceItemsRepository
{
    Item GetItem(Guid id);
    IEnumerable<Item> GetItems();

    void CreateItem(Item item);

    void UpdateItem(Item item);

    void DeleteItem(Guid id);
}