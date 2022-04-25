using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulator2
{
    class BuildingsController
    {
        List<Buildings> allBuildings = new List<Buildings>();
        public GameSession CurrentGameSession;

        public void AddUnit(Buildings unit) 
        {
            allBuildings.Add(unit);
            return;
        }

        public List<Buildings> GetAllUnits() 
        {
            return allBuildings;
        }
    }
}
