using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2
{
    class House : Buildings
    {
        public void initialize() 
        {
            BuildingColor = Color.LightGreen;
        }
    }
}
