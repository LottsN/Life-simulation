using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

namespace LifeSimulator2
{
    class ResourcesController
    {
        public GameSession CurrentGameSession;
        private List<ResourceUnit> allUnits = new List<ResourceUnit>();

        public List<ResourceUnit> GetAllUnits()
        {
            return allUnits;
        }

        public void AddUnit(ResourceUnit unit)
        {
            allUnits.Add(unit);
            return;
        }

        public void DeleteResource(ResourceUnit deletingUnit)
        {
            allUnits.Remove(deletingUnit);
            deletingUnit = null;
            return;
        }
    }
}