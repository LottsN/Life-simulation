using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace LifeSimulator2.Animals
{
    class Wolf : AnimalCarnivores
    {
        public override void initialize()
        {
            AnimalColor = Color.FromArgb(255, 165, 42, 42);
            isCanHibernate = false;
            canBeTamed = true;
            StrollStyle = 2;
            EatMovingStyle = 2;
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\wolf.png");
        }
    }
}
