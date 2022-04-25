using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LifeSimulator2
{
    class Human : Animal //всеядные - третьи
    {
        Bank ownBank = null;
        Blacksmith ownBlacksmith = null;
        Quarry ownQuarry = null;
        Sawmill ownSawmill = null;
        House ownHouse = null;

        
        public override void initialize()
        {
            AnimalColor = Color.FromArgb(255, 255, 255, 255);
            isCanHibernate = false;
            canProduce = true;
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\human.png");
        }

        public List<dynamic> Backpack = new List<dynamic>(4);


        public override dynamic FindPathTo(AnimalsController allAnimals, List<Plant> uneatablePlants)
        {
            var minDistance = 100000000.0;

            var allPlants = allAnimals.giveAllPlants();
            var allPlantsUnits = allPlants.giveAllUnits();

            if (allPlantsUnits.Count == 0) return null;

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


                if (currentAnimal.GetType().ToString() != "LifeSimulator2.Human")
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


        public void CollectFoodToBackpack(dynamic currentFood)
        {
            Backpack.Add(currentFood);
            //MessageBox.Show("INBACKPACK" + Backpack.Count().ToString());
        }

        private void EatFoodFromBackpack(dynamic currentFood) 
        {
            satiety += 80;
            ChangeHungary();
            Backpack.Remove(currentFood);
            currentFood = null;
        }

        public List<dynamic> getBackpack() 
        {
            return Backpack;
        }

        public void GiveFoodFromBackpack() 
        {
            if (Backpack.Count > 0) 
            {
                Backpack.Remove(Backpack[0]);
            }
            return;
        }

        public override void EatExistingFood() 
        {
            if (satiety <= 0) 
            {
                if (Backpack.Count != 0)
                {
                    for (int i = 0; i < getBackpack().Count; i++)
                    {
                        if (getBackpack()[0].GetType().BaseType.ToString() != "ResourceUnit")
                        {
                            currentFood = getBackpack()[0];
                            EatFoodFromBackpack(currentFood);
                        }
                    }
                    
                }
            }
        }


        private ResourceUnit SearchForResource(ResourcesController allResources, string type)
        {
            
            
            var minDistance = 100000000.0;

            var allResourceUnits = allResources.GetAllUnits();
            

            if (allResourceUnits.Count != 0) 
                {
                    ResourceUnit nearestResource = null;
                    ResourceUnit currentResource = null;

                    for (int i = 0; i < allResourceUnits.Count; i++)
                    {
                        currentResource = allResourceUnits[i];




                    if (currentResource.GetType().ToString() == type)
                    {
                        var unitLocation = currentResource.GetLocation();
                        var currentDistance = CountDistance(currentLocation, unitLocation);

                        if (currentDistance < minDistance)
                        {
                            minDistance = currentDistance;
                            nearestResource = currentResource;
                        }
                    }

                    }
                    return nearestResource;
                }
                else 
                {
                    return null;
                }

            
        }

        private void DropResources() 
        {

        }



        private Boolean IsThereKindOf(string type) 
        {
            for (int i =0; i < Backpack.Count; i++) 
            {
                if (Backpack[i].GetType().ToString() == type) 
                {
                    return true;
                }
            }
            return false;
        }

        private void CollectResources(Buildings build) 
        {
            if (build.GetType().ToString() == "Bank")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Gold")
                    {
                        build.PutInStorage(Backpack[i]);
                        Backpack.Remove(Backpack[i]);
                    }
                }
            }

            if (build.GetType().ToString() == "Sawmill")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Wood")
                    {
                        build.PutInStorage(Backpack[i]);
                        Backpack.Remove(Backpack[i]);
                    }
                }
            }

            if (build.GetType().ToString() == "Blacksmith")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Iron")
                    {
                        build.PutInStorage(Backpack[i]);
                        Backpack.Remove(Backpack[i]);
                    }
                }
            }

            if (build.GetType().ToString() == "Quarry")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Stone")
                    {
                        build.PutInStorage(Backpack[i]);
                        Backpack.Remove(Backpack[i]);
                    }
                }
            }
        }

        private void SpendResources(ResourcesController resourcesController, Buildings build, AnimalsController allAnimals) 
        {
            if (build.GetType().ToString() == "Bank")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Gold")
                    {
                        build.IncCompleteness(build);
                        Backpack.Remove(Backpack[i]);
                        if (build.CheckCompleteness()) 
                        {
                            currentResource = SearchForResource(resourcesController, "Gold");
                        }                     
                    }
                }
            }

            if (build.GetType().ToString() == "Sawmill")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Wood")
                    {
                        build.IncCompleteness(build);
                        Backpack.Remove(Backpack[i]);
                        if (build.CheckCompleteness())
                        {
                            currentResource = SearchForResource(resourcesController, "Wood");
                        }


                    }
                }
            }

            if (build.GetType().ToString() == "Blacksmith")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Iron")
                    {
                        build.IncCompleteness(build);
                        Backpack.Remove(Backpack[i]);
                        if (build.CheckCompleteness())
                        {
                            currentResource = SearchForResource(resourcesController, "Iron");
                        }


                    }
                }
            }

            if (build.GetType().ToString() == "Quarry")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Stone")
                    {
                        build.IncCompleteness(build);
                        Backpack.Remove(Backpack[i]);
                        if (build.CheckCompleteness())
                        {
                            currentResource = SearchForResource(resourcesController, "Stone");
                        }


                    }
                }
            }

            if (build.GetType().ToString() == "House")
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == "Stone")
                    {
                        build.IncCompleteness(build);
                        Backpack.Remove(Backpack[i]);
                    }
                    if (Backpack[i].GetType().ToString == "Gold")
                    {
                        build.IncCompleteness(build);
                        Backpack.Remove(Backpack[i]);
                        if (build.CheckCompleteness()) 
                        {
                            Human newHuman = new Human();
                            newHuman.initialize();
                            allAnimals.addAnimal(newHuman.Born(new Cell<int, int>(currentLocation.First + (10 * rnd.Next(-1, 1)), currentLocation.Second + (10 * rnd.Next(-1, 1))), 40, 100, false, AnimalColor));
                        }
                    }
                }
            }

        }


        private void MoveToBuilding(ResourcesController resourcesController, Buildings build, AnimalsController allAnimals) 
        {
            if (currentLocation.First < build.GetLocation().First)
            {
                currentLocation.First += 10;
            }

            if (currentLocation.First > build.GetLocation().First)
            {
                currentLocation.First -= 10;
            }

            if (currentLocation.Second < build.GetLocation().Second)
            {
                currentLocation.Second += 10;
            }

            if (currentLocation.Second > build.GetLocation().Second)
            {
                currentLocation.Second -= 10;
            }

            if ((build.GetLocation().First == currentLocation.First) && (build.GetLocation().Second == currentLocation.Second))
            {
                if (!build.CheckCompleteness()) 
                {
                    SpendResources(resourcesController, build, allAnimals);
                    //SpendResources();
                    //продолжаем искать этот ресурс
                }
                else 
                {
                    CollectResources(build);
                }
            }
        }

        private void BuildStorage(string type, BuildingsController allBuildings)
        {
            if (type == "Bank") 
            {
                var bank = new Bank();
                bank.initialize();
                bank.SetOwner(this);
                bank.currentLocation.First = 10 * rnd.Next(1, 180);
                bank.currentLocation.First = 10 * rnd.Next(1, 180);
                allBuildings.AddUnit(bank);
                ownBank = bank;
            }
            if (type == "Sawmill")
            {
                var sawmill = new Sawmill();
                sawmill.initialize();
                sawmill.SetOwner(this);
                sawmill.currentLocation.First = 10 * rnd.Next(1, 180);
                sawmill.currentLocation.First = 10 * rnd.Next(1, 180);
                allBuildings.AddUnit(sawmill);
                ownSawmill = sawmill;
            }
            if (type == "Quarry")
            {
                var quarry = new Quarry();
                quarry.initialize();
                quarry.SetOwner(this);
                quarry.currentLocation.First = 10 * rnd.Next(1, 180);
                quarry.currentLocation.First = 10 * rnd.Next(1, 180);
                allBuildings.AddUnit(quarry);
                ownQuarry = quarry;
            }
            if (type == "Blacksmith")
            {
                var blacksmith = new Blacksmith();
                blacksmith.initialize();
                blacksmith.SetOwner(this);
                blacksmith.currentLocation.First = 10 * rnd.Next(1, 180);
                blacksmith.currentLocation.First = 10 * rnd.Next(1, 180);
                allBuildings.AddUnit(blacksmith);
                ownBlacksmith = blacksmith;
            }

            if (type == "House")
            {
                var house = new House();
                house.initialize();
                house.SetOwner(this);
                house.currentLocation.First = 10 * rnd.Next(1, 180);
                house.currentLocation.First = 10 * rnd.Next(1, 180);
                allBuildings.AddUnit(house);
                ownHouse = house;
            }
        }

        private void CollectToBackpack(ResourceUnit collectedUnit, ResourcesController resourcesController, AnimalsController allAnimals, BuildingsController allBuildings)
        {
            if (Backpack.Count < 4)
            {
                Backpack.Add(collectedUnit);
                collectedUnit.ReduceResourceAmount(collectedUnit, resourcesController);
            }
                if (Backpack.Count >= 4 ) 
                {
                    

                    if (IsThereKindOf("Gold")) 
                    {
                        if (ownBank == null) 
                        {
                            BuildStorage("Bank", allBuildings);
                        }
                    MoveToBuilding(resourcesController, ownBank, allAnimals) ;
                    }
                    else 
                    {
                        if (IsThereKindOf("Wood")) 
                        {
                            if (ownSawmill == null)
                            {
                                BuildStorage("Sawmill", allBuildings);
                            }
                            MoveToBuilding(resourcesController, ownSawmill, allAnimals);
                        }
                        else 
                        {
                            if (IsThereKindOf("Stone")) 
                            {
                                if (ownQuarry == null) 
                                {
                                    BuildStorage("Quarry", allBuildings);
                                }
                                MoveToBuilding(resourcesController, ownQuarry, allAnimals);
                            }
                            else 
                            {
                                if (IsThereKindOf("Iron"))
                                {
                                    if (ownBlacksmith == null)
                                    {
                                        BuildStorage("Blacksmith", allBuildings);
                                    }
                                    MoveToBuilding(resourcesController, ownBlacksmith, allAnimals);
                                }
                                else 
                                {
                                if (ownHouse != null)
                                {
                                    MoveToBuilding(resourcesController, ownHouse, allAnimals);
                                }
                                
                            }

                            }
                            
                        }
                    }
                
                }
        }
        
        private void MoveForResource(ResourceUnit resourceUnit, ResourcesController resourcesController, AnimalsController allAnimals, BuildingsController allBuildings)
        {
            if (currentLocation.First < resourceUnit.GetLocation().First)
            {
                currentLocation.First += 10;
            }

            if (currentLocation.First > resourceUnit.GetLocation().First)
            {
                currentLocation.First -= 10;
            }

            if (currentLocation.Second < resourceUnit.GetLocation().Second)
            {
                currentLocation.Second += 10;
            }

            if (currentLocation.Second > resourceUnit.GetLocation().Second)
            {
                currentLocation.Second -= 10;
            }

            if ((resourceUnit.GetLocation().First == currentLocation.First) && (resourceUnit.GetLocation().Second == currentLocation.Second))
            {
                CollectToBackpack(resourceUnit, resourcesController, allAnimals, allBuildings);
            }
        }

        private int FoundInBackpack(string type) 
        {
            var count = 0;
              for (int i = 0; i < Backpack.Count; i++)
                {
                    if (Backpack[i].GetType().ToString == type)
                    {
                    count += 1;
                    }
                }
            return count;
        }

        private bool CanBuildHouse() 
        {
            if (ownBank.GetStorage().Count + FoundInBackpack("Gold") >= 2 && ownQuarry.GetStorage().Count + FoundInBackpack("Stone") >= 2) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void CollectToBackpackFromBuilding(Buildings build) 
        {
            if (Backpack.Count < 4) 
            {
                Backpack.Add(build.GetItem());
            }
        }

        private void MoveToTakeFrom(Buildings build) 
        {
            if (currentLocation.First < build.GetLocation().First)
            {
                currentLocation.First += 10;
            }

            if (currentLocation.First > build.GetLocation().First)
            {
                currentLocation.First -= 10;
            }

            if (currentLocation.Second < build.GetLocation().Second)
            {
                currentLocation.Second += 10;
            }

            if (currentLocation.Second > build.GetLocation().Second)
            {
                currentLocation.Second -= 10;
            }

            if ((build.GetLocation().First == currentLocation.First) && (build.GetLocation().Second == currentLocation.Second))
            {
                CollectToBackpackFromBuilding(build);
            }
        }


        public override void FindResource(ResourcesController allResources, AnimalsController allAnimals, BuildingsController allBuildings)
        {
            
            if (ownHouse == null) 
            {
                BuildStorage("House", allBuildings);
            }

            if (CanBuildHouse() && !ownHouse.CheckCompleteness()) 
            {
                if (FoundInBackpack("Stone") >= 1 || FoundInBackpack("Gold") >= 1) 
                {
                    MoveToBuilding(allResources, ownHouse, allAnimals) ;
                }
                else 
                {
                    if (ownBank.GetStorage().Count > 0) 
                    {
                        MoveToTakeFrom(ownBank);
                    }
                    else if (ownQuarry.GetStorage().Count > 0)
                        {
                        MoveToTakeFrom(ownQuarry);
                    }
                    else 
                    {
                        currentResource = SearchForResource(allResources, "Gold");
                    }
                }
                
            }

            if (currentResource == null)
            {
                var search = rnd.Next(1, 5);
                switch (search)
                {
                    case 1:
                        currentResource = SearchForResource(allResources, "Gold");
                        break;
                    case 2:
                        currentResource = SearchForResource(allResources, "Iron");
                        break;
                    case 3:
                        currentResource = SearchForResource(allResources, "Wood");
                        break;
                    case 4:
                        currentResource = SearchForResource(allResources, "Stone");
                        break;
                }
                    
            }

            MoveForResource(currentResource, allResources, allAnimals, allBuildings);
        }

        public bool CheckIsFoodEatable() 
        {
            return true;
        }


        public override void eatFood(dynamic currentFood, AnimalsController allAnimals)
        {

            if (currentFood.GetType().BaseType.ToString() == "LifeSimulator2.AnimalOmnivores" || currentFood.GetType().BaseType.ToString() == "LifeSimulator2.AnimalHerbivorous" || currentFood.GetType().BaseType.ToString() == "LifeSimulator2.AnimalCarnivores")
            {
                currentFood.DieByHunger(allAnimals);
                Meat meat = new Meat();
                CollectFoodToBackpack(meat);

                if (satiety > 0 && isHungry == true && Backpack.Count() == 4)
                {
                    ChangeHungary();
                }
            }
            else
            {
                if (currentFood.GetType().BaseType.ToString() == "EatableHealthySeedableFood")
                    {
                    if (Backpack.Count < 4) 
                        {
                        Backpack.Add(currentFood.GiveWood());
                        }
                    }

                if (currentFood.CheckIsFoodEatable())
                {
                    if (currentFood.CheckIfIsFoodHealthy())
                    {
                        var collectedFood = currentFood;
                        CollectFoodToBackpack(collectedFood);
                        currentFood.BecomeEaten();

                        if (satiety > 0 && isHungry == true && Backpack.Count() == 4)
                        {
                            ChangeHungary();
                        }
                        CheckIfItIsLastFood(allAnimals);
                    }
                    else
                    {
                        var collectedFood = currentFood;
                        CollectFoodToBackpack(collectedFood);
                        currentFood.BecomeEaten();
                    }
                }
                else
                {
                    uneatablePlants.Add(currentFood);
                }
            }

            if (satiety <= 0) EatFoodFromBackpack(currentFood);

        }

    }
}
