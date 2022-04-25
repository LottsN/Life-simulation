using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2
{
    class Meat : Plant
    {
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
