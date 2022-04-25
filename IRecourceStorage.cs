using System.Collections.Generic;

namespace LifeSimulator2
{
    public interface IRecourceStorage<TItem>
    {
        ICollection<TItem> GetItems();
        TItem GetItem(TItem key);
        void AddItem(TItem newItem);
        void DeleteItem(TItem key);
        
    }
}