using System.Collections.Generic;
using System.Drawing;

namespace LifeSimulator2
{
    class Buildings
    {
        internal Cell<int, int> currentLocation = new Cell<int, int>(0, 0);
        List<dynamic> storage = new List<dynamic>();
        private Human owner;
        public int completness = 0;
        public bool isCanKeep = false;
        internal Color BuildingColor;

        public void SetOwner(Human man) 
        {
            this.owner = man;
        }

        public Cell<int, int> GetLocation()
        {
            return currentLocation;
        }

        public bool CheckCompleteness() 
        {
            if (completness >= 100) 
            {
                isCanKeep = true;
                return true;
            }
            else 
            {
                isCanKeep = false;
                return false;
            }
        }


        public void IncCompleteness(Buildings build) 
        {
            build.completness += 25;
        }

        public void PutInStorage(ResourceUnit unit) 
        {
            storage.Add(unit);
        }

        public List<dynamic> GetStorage() 
        {
            return storage;
        }

        public dynamic GetItem() 
        {
            var x = storage.Remove(storage[0]);
            return x;
        }

        public Color getColor() 
        {
            return BuildingColor;
        }
    }
}