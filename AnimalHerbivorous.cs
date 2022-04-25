using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LifeSimulator2
{
    class AnimalHerbivorous : Animal  //травоядные
    {

        public override dynamic FindPathTo(AnimalsController allAnimals, List<Plant> uneatablePlants)
        {
            var allPlants = allAnimals.giveAllPlants();
            var allPlantsUnits = allPlants.giveAllUnits();
            var minDistance = 100000000.0;
            if (allPlantsUnits.Count != 0) 
            {
                Plant nearestFood = allPlantsUnits[0];

                for (int i = 0; i < allPlantsUnits.Count; i++)
                {
                    if (!uneatablePlants.Contains(allPlantsUnits[i]))
                    {
                        var unitLocation = allPlantsUnits[i].GetLocation();
                        var currentDistance = CountDistance(currentLocation, unitLocation);
                        if (currentDistance < minDistance)
                        {
                            minDistance = currentDistance;
                            nearestFood = allPlantsUnits[i];
                        }
                    }

                }
                return nearestFood;
            }
            else 
            {
                return null;
            }
        }

        public override void eatFood(dynamic currentPlant, AnimalsController allAnimals)
        {
            pastFood = currentPlant;
            if (currentPlant.CheckIsFoodEatable())
            {
                if (currentPlant.CheckIfIsFoodHealthy())
                {
                    satiety += currentPlant.BecomeEaten();
                    if (satiety > 0 && isHungry == true)
                    {
                        ChangeHungary();
                    }
                    
                    CheckIfItIsLastFood(allAnimals);
                }
                else
                {
                    currentPlant.BecomeEaten();
                    DieByHunger(allAnimals);
                }
            }
            else
            {
                uneatablePlants.Add(currentPlant);
            }
        }



    }
}
