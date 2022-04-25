using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulator2
{
    class EatableHealthySeedableFood : Plant //green
    {
        public void initialize()
        {
            isEatable = true;
            isCanSeed = true;
            isHealthy = true;
        }

        public override void SeedFood(PlantsController allPlants)
        {
            var newPlant = new EatableHealthySeedableFood();
            allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 30, this.isEatable, this.isCanSeed, this.isHealthy, this.PlantColor));
        }

        public Wood GiveWood() 
        {
            var wood = new Wood();
            return wood;
        }

        public override void CheckAge(int SeasonDamage, string CurrentSeason)
        {
            if (80 >= lifeTime && lifeTime > 70) //семя
            {
                blockEatable = true;
                blockReproductionAndSeeding = true;
                imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\seed.png");
                if (CurrentSeason == "winter") lifeTime += 1;
            }

            if (70 >= lifeTime && lifeTime > 50) //росток
            {
                blockEatable = false;
                blockReproductionAndSeeding = true;
                imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\sprout.png");
                //lifeTime -= SeasonDamage;
            }

            if (50 >= lifeTime && lifeTime > 10) //растение
            {
                blockEatable = false;
                blockReproductionAndSeeding = false;
                imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\eatablehealthyseedable.jpg");
                //lifeTime -= SeasonDamage;
            }

            if (10 >= lifeTime) //пожилое
            {
                blockEatable = true;
                blockReproductionAndSeeding = true;
                imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\faded.png");
                //lifeTime -= SeasonDamage;
            }

        }
    }
}
