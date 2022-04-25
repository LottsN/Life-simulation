using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2
{
    class Fruits : Plant
    {
        public Fruits drop(Boolean IsHealthy, int X, int Y)
        {
            isEaten = false;
            this.isHealthy = IsHealthy;
            if (isHealthy) this.imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\fruit.png");
            else this.imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\harmful.jpg"); ;
            location.First = X;
            location.Second = Y;
            return this;
        }

        private void Fade(PlantsController allFood)
        {
            allFood.DeleteFaded(this);
        }

        public override void MakeTurn(PlantsController allFood, int SeasonDamage, string CurrentSeason)
        {
            if (isEaten)
            {
                Fade(allFood);
            }
        }
    }
}
