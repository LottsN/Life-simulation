using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2.Animals
{
    class Tiger : AnimalCarnivores
    {
        public override void initialize()
        {
            AnimalColor = Color.FromArgb(255, 165, 42, 42);
            isCanHibernate = false;
            canBeTamed = false;
            StrollStyle = 1;
            EatMovingStyle = 3;
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\tiger.png");
        }
    }
}
