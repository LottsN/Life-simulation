using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2.Animals
{
    class Fox : AnimalCarnivores
    {
        public override void initialize()
        {
            AnimalColor = Color.FromArgb(255, 165, 42, 42);
            isCanHibernate = true;
            canBeTamed = false;
            StrollStyle = 1;
            EatMovingStyle = 1;
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\fox.png");
        }
    }
}
