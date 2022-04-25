using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2.Animals
{
    class Rabbit : AnimalHerbivorous
    {
        public override void initialize()
        {
            AnimalColor = Color.FromArgb(255, 200, 54, 54);
            isCanHibernate = false;
            canBeTamed = true;
            StrollStyle = 2;
            EatMovingStyle = 1;
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\rabbit.jpg");
            ability = "reduceHunger";
        }
    }
}
