using LifeSimulator2.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulator2
{
       class GameSession
        {
        public int step;
        public string CurrentSeason = "summer";
        public double satietyCoef = 1.0;
        public int SeasonDamage = 0;
        private Random rnd = new Random();
        private AnimalsController allAnimals = new AnimalsController();
        private PlantsController allPlants = new PlantsController();
        private ResourcesController allResources = new ResourcesController();
        private BuildingsController allBuildings = new BuildingsController();
        

        private void SpawnAnimals(int n)
            {
                
                for (int i = 0; i < n; i++)
                {
                int animalType = rnd.Next(7, 8); //вернуть
                //int animalType = 10;

                switch (animalType) 
                {
                    case 1:
                        {
                            var newAnimal = new Bear();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            //MessageBox.Show(newAnimal.GetType().ToString());
                            break;
                        }
                    case 2:
                        {
                            var newAnimal = new Beaver();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 3:
                        {
                            var newAnimal = new Fox();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 4:
                        {
                            var newAnimal = new Horse();
                            newAnimal.initialize();
                            //MessageBox.Show(newAnimal.GetType().BaseType.Name.ToString());
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 5:
                        {
                            var newAnimal = new Pig();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 6:
                        {
                            var newAnimal = new Rabbit();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 7:
                        {
                            var newAnimal = new Rat();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 8:
                        {
                            var newAnimal = new Tiger();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 9:
                        {
                            var newAnimal = new Wolf();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                    case 10:
                        {
                            var newAnimal = new Human();
                            newAnimal.initialize();
                            allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(1 * 10 * rnd.Next(1, 180), 1 * 1 * 10 * rnd.Next(1, 100)),
                                    40, 100, false, newAnimal.AnimalColor));
                            break;
                        }
                }
                
                }
            }

            private void SpawnPlants(int n)
            {
                for (int i = 0; i < n; i++)
                {
                int plantType = rnd.Next(1, 6); //вернуть
                //int plantType = 1;

                switch (plantType)
                {
                    case 1:
                        {
                            var newPlant = new EatableHealthySeedableFood();//тут рандомный выбор
                            newPlant.initialize();
                            allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 2:
                        {
                            var newPlant = new EatablePoisonousSeedableFood();//тут рандомный выбор
                            newPlant.initialize();
                            allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 3:
                        {
                            var newPlant = new EatableHealthyUnseedable();//тут рандомный выбор
                            newPlant.initialize(); 
                            allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 4:
                        {
                            var newPlant = new EatablePoisonousUnseedable();//тут рандомный выбор
                            newPlant.initialize();
                            allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 5:
                        {
                            var newPlant = new UneatableSeedable();//тут рандомный выбор
                            newPlant.initialize();
                            allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 6:
                        {
                            var newPlant = new UneatableUnseedable();//тут рандомный выбор
                            newPlant.initialize();
                            allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        } 

                }
                 
                }
            }
            
            
            private void SpawnRecources(int n)
            {
                for (int i = 0; i < n; i++)
                {
                int resourceType = rnd.Next(1, 5); //вернуть
                //int resourceType = 3;

                switch (resourceType)
                {
                    case 1:
                        {
                            var newResource = new Gold();//тут рандомный выбор
                            newResource.initialize();
                            allResources.AddUnit(newResource.PlaceUnit(new Cell<int, int>(1, 1), newResource.PlantColor));
                            //allResources.GoldStorage.AddItem(newResource);
                            //allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 2:
                        {
                            var newResource = new Stone();//тут рандомный выбор
                            newResource.initialize();
                            allResources.AddUnit(newResource.PlaceUnit(new Cell<int, int>(1, 1), newResource.PlantColor));
                            //allResources.StoneStorage.AddItem(newResource);
                            //allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 3:
                        {
                            var newResource = new Wood();//тут рандомный выбор
                            newResource.initialize(); 
                            allResources.AddUnit(newResource.PlaceUnit(new Cell<int, int>(1, 1), newResource.PlantColor));
                            //allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }
                    case 4:
                        {
                            var newResource = new Iron();//тут рандомный выбор
                            newResource.initialize();
                            allResources.AddUnit(newResource.PlaceUnit(new Cell<int, int>(1, 1), newResource.PlantColor));
                            //allResources.IronStorage.AddItem(newResource);
                            //allPlants.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 20, newPlant.isEatable, newPlant.isCanSeed, newPlant.isHealthy, newPlant.PlantColor));
                            break;
                        }

                }
                 
                }
            }

            private void FoodsTurn()
            {
                allPlants.FoodsTurn();
            }

            private void AnimalsTurn()
            {
                allAnimals.getAllPlants(allPlants);
                allAnimals.getAllResources(allResources);
                allAnimals.AnimalsTurn();
                allAnimals.getAlBuildings(allBuildings);
            }

            private void IncreaseGameStep()
            {
                step += 1;
                CheckSeason();
            }

            public void MainSession(int stepsAmount)
            {
                for (int i = 0; i < stepsAmount; i++)
                {
                    FoodsTurn();
                    AnimalsTurn();
                    IncreaseGameStep();

                }
            }

            public void GameStart(int foodAmount, int animalsAmount, int ResourceSourcesAmount)
            {
                SpawnPlants(foodAmount);
                SpawnAnimals(animalsAmount);
                SpawnRecources(ResourceSourcesAmount);
            }

            public List<Animal> ReternAllAnimals()
            {
            return allAnimals.giveAllUnits();
            }

            public ResourcesController ReturnAllResources()
            {
                return allResources;
            }

             public List<Plant> ReternAllFood()
             {
             return allPlants.giveAllUnits();
             }

            public string CheckSeason()
            {
            if (step < 10 && step >= 1) 
            {
                CurrentSeason = "summer";
            } 
            else
            {
              CurrentSeason = "winter";
              if (step > 20) step = 1;
            }
                
                
            return CurrentSeason;
            }

            public AnimalsController GetAnimalsController() 
            {
            return allAnimals;
            }

        public PlantsController GetPlantsController()
        {
            return allPlants;
        }

        public ResourcesController GetResourcesController()
        {
            return allResources;
        }

        public BuildingsController GetBuildingsController()
        {
            return allBuildings;
        }

        public string GetSeason() 
        {
            return CurrentSeason;
        }
    }
}
