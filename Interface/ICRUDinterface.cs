
public interface ICRUDinterface<T>
{
    // Create Read Update Delete
    T Post(T target);
    T? Get(int id);
    List<T> GetAll();
    List<T> GetAll(int pageNumber, int pageSize);
    bool Put(T target); //replace entirely
    // Task Patch(T target); // replace partially // these 2 i find way too hard to use with generics
    bool Delete(int id);
    bool Delete(T target);
    
}