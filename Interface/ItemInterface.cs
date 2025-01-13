
public interface ItemInterface
{
    bool Delete(string id);
    bool Delete(Item target);
    Item? Get(string id);
    List<Item> GetAll();
    (bool, bool, bool, bool, bool, bool, Item) Post(Item target);
    (bool, bool, bool, bool, bool) Put(Item target);
    List<Item> GetItemsForItemLine(int ItemID);
    List<Item> GetItemsForItemType(int ItemID);

    List<Item> GetItemsForItemGroup(int ItemID);
    List<Item> GetItemsForSupplier(int ItemID);


}
