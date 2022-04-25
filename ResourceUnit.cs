using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LifeSimulator2
{
    abstract class ResourceUnit
    {
        protected Cell<int, int> location = new Cell<int, int>(0, 0);
        internal Color PlantColor;
        private int AmountOfResource;
        private Random rnd = new Random();

        public ResourceUnit PlaceUnit(Cell<int, int> givingLocation, Color PlantColor)
        {
            
            location.First = givingLocation.First * 10 * rnd.Next(1, 180);
            location.Second = givingLocation.Second * 10 * rnd.Next(1, 100);
            AmountOfResource = 100;
            string Coord = "Spawn resource at: X" + location.First.ToString() + "; Y " + location.Second.ToString();
            MessageBox.Show(Coord);
            this.PlantColor = PlantColor;
            return this;
        }
        
        public void DeleteBroken(ResourceUnit brokenResource)
        {
            brokenResource = null;
            return;
        }

        public Color getColor()
        {
            return PlantColor;
        }

        public Cell<int, int> GetLocation()
        {
            return location;
        }
        
        private void Broke(ResourceUnit unit) 
        {

        }

        public void ReduceResourceAmount(ResourceUnit unit, ResourcesController resourcesController)
        {
            unit.AmountOfResource -= 50;
            if (AmountOfResource <= 0)
            {
                resourcesController.DeleteResource(unit);
            }
        }
        
    }
}
