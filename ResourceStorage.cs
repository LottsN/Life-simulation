using System.Collections.Generic;

namespace LifeSimulator2
{
    public class ResourceStorage<T> 
    {
        private List<T> units;

        public List<T> GetItems()
        {
            return units;
        }

        public void AddItem(T newItem)
        {
            units.Add(newItem);
        }

        public void DeleteItem(T deletedItem)
        {
            if (deletedItem != null)
            {
                units.Remove(deletedItem);
            }
        }
    }
}