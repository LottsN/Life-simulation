using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LifeSimulator2
{
    class AnimalOmnivores : Animal //всеядные - третьи
    {

        public override dynamic FindPathTo(AnimalsController allAnimals, List<Plant> uneatablePlants)
        {
            var minDistance = 100000000.0;

            var allPlants = allAnimals.giveAllPlants();
            var allPlantsUnits = allPlants.giveAllUnits();
            
            dynamic nearestFood = allPlantsUnits[0];

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
            var allAnimalUnits = allAnimals.giveAllUnits();
            var currentAnimal = allAnimalUnits[0];

            for (int i = 0; i < allAnimalUnits.Count; i++)
            {
                currentAnimal = allAnimalUnits[i];


                if (currentAnimal.GetType().BaseType.ToString() != "LifeSimulator2.AnimalOmnivores")
                {
                    var unitLocation = currentAnimal.GetLocation();
                    var currentDistance = CountDistance(currentLocation, unitLocation);

                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        nearestFood = currentAnimal;
                    }
                }

            }
            return nearestFood;
        }

        public override void eatFood(dynamic currentFood, AnimalsController allAnimals)
        {
            pastFood = currentFood;
            if (currentFood.GetType().BaseType.ToString() == "LifeSimulator2.AnimalHerbivorous" || currentFood.GetType().BaseType.ToString() == "LifeSimulator2.AnimalCarnivores" || currentFood.GetType().BaseType.ToString() == "LifeSimulator2.Human")
            {
                satiety += 80;
                if (satiety > 0 && isHungry == true)
                {
                    ChangeHungary();
                }
                currentFood.DieByHunger(allAnimals);
            }
            else
            {
                if (currentFood.CheckIsFoodEatable())
                {
                    if (currentFood.CheckIfIsFoodHealthy())
                    {
                        satiety += currentFood.BecomeEaten();
                        if (satiety > 0 && isHungry == true)
                        {
                            ChangeHungary();
                        }
                        CheckIfItIsLastFood(allAnimals);
                    }
                    else
                    {
                        currentFood.BecomeEaten();
                        DieByHunger(allAnimals);
                    }
                }
                else
                {
                    uneatablePlants.Add(currentFood);
                }
            }

        }

    

    }
}
