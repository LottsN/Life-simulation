using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2.Animals
{
    class Beaver : AnimalHerbivorous
    {
        public override void initialize()
        {
            AnimalColor = Color.FromArgb(255, 200, 54, 54);
            isCanHibernate = true;
            canBeTamed = false;
            StrollStyle = 3;
            EatMovingStyle = 2;
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\beaver.png");
        }
    }
}
