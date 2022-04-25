using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2.Animals
{
    class Pig : AnimalOmnivores
    {
        public override void initialize()
        {
            AnimalColor = Color.FromArgb(255, 110, 30, 30);
            isCanHibernate = false;
            canBeTamed = true;
            StrollStyle = 3;
            EatMovingStyle = 2;
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\pig.jpg");
            ability = "fillBackpack";
        }
    }
}
