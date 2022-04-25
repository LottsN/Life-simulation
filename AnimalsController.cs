using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulator2
{
    class AnimalsController
    {
            private List<Animal> allUnits = new List<Animal>();
            private PlantsController allPlants;
            private ResourcesController allResources;
            private BuildingsController allBuildings;
            public GameSession CurrentGameSession;
            private double satietyCoef;

            private void CheckSeason() 
            {
            if (CurrentGameSession.GetSeason() == "summer") 
            {
                satietyCoef = 1.0;
            }
            else 
            {
                satietyCoef = 0.93;
            }
            }

            public void addAnimal(Animal unit)
            {
            allUnits.Add(unit);
            return;
            }


            //для системы
            public void AnimalsTurn()
            {
            CheckIfIsThereAnimal();
            for (int i = 0; i < allUnits.Count; i++)
                {
                    CheckSeason();
                    allUnits[i].MakeTurn(this, satietyCoef, CurrentGameSession.GetSeason(), allResources, allBuildings);
                }
            }



            public void DeleteDeadAnimal(Animal deadAnimal)
            {
            allUnits.Remove(deadAnimal);
            deadAnimal = null;
            return;
            }

            public void getAllPlants(PlantsController Plants)
            {
                allPlants = Plants;
            }
            
            public void getAllResources(ResourcesController Resources)
            {
                allResources = Resources;
            }

            public void getAlBuildings(BuildingsController builds)
            {
                allBuildings = builds;
            }

        public void RenewPathsToFood()
            {
            for (int i = 0; i < allUnits.Count; i++)
            {
                allUnits[i].MakeTurn(this, satietyCoef, CurrentGameSession.GetSeason(), allResources, allBuildings);
            }
            }

            private void CheckIfIsThereAnimal()
            {
                if (allUnits.Count == 0)
                {
                    MessageBox.Show("Game Over!") ;
                    Application.Exit();
                }
            }

            public PlantsController giveAllPlants()
            {
                return allPlants;
            }

            public List<Animal> giveAllUnits()
            {
            return allUnits;
            }
    }
}
