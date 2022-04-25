using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LifeSimulator2
{
    class AnimalCarnivores : Animal  //плотоядные - другими животными, не сородичами
    {

        public override dynamic FindPathTo(AnimalsController allAnimals, List<Plant> uneatablePlants)
        {
            var minDistance = 100000000.0;

            var allAnimalUnits = allAnimals.giveAllUnits();

            if (allAnimalUnits.Count != 0) 
            {
                var nearestAnimal = allAnimalUnits[0];
                var currentAnimal = allAnimalUnits[0];

                for (int i = 0; i < allAnimalUnits.Count; i++)
                {
                    currentAnimal = allAnimalUnits[i];



                    if (currentAnimal.GetType().BaseType.ToString() != "LifeSimulator2.AnimalCarnivores")
                    {
                        var unitLocation = currentAnimal.GetLocation();
                        var currentDistance = CountDistance(currentLocation, unitLocation);

                        if (currentDistance < minDistance)
                        {
                            minDistance = currentDistance;
                            nearestAnimal = currentAnimal;
                        }
                    }

                }
                return nearestAnimal;
            }
            else 
            {
                return null;
            }

        }

        public override void eatFood(dynamic currentFood, AnimalsController allAnimals) //едим животное
        {
            pastFood = currentFood;
            satiety += 80;
            if (satiety > 0 && isHungry == true)
               {
                    ChangeHungary();
               }
            currentFood.DieByHunger(allAnimals);
        }

    }
}
