using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Remoting;
using LifeSimulator2.Animals;

namespace LifeSimulator2
{
    abstract class Animal
    {

        internal Cell<int, int> currentLocation = new Cell<int, int>(0, 0);
        internal int satiety;
        internal Boolean isHungry = false;
        internal dynamic pastFood;
        internal Random rnd = new Random();
        
        internal ResourceUnit currentResource;
        internal Cell<int, int> HomeLocation;

        internal List<Plant> uneatablePlants = new List<Plant>();
        internal List<Animal> TamedAnimals = new List<Animal>();
        internal Color AnimalColor = Color.Brown;
        internal Boolean isMale;
        internal Boolean isCanHibernate;
        internal Boolean canProduce = false;
        internal Boolean canBeTamed = true;
        internal Animal partner = null;
        internal int healthPoints;
        internal int lifeTime;
        internal int StrollStyle = 1;
        internal int EatMovingStyle = 1;
        internal int ProduceBlockTimer = 5;
        internal dynamic currentFood;
        internal Human owner = null;
        internal Image imageToDraw;
        internal string ability;

        private void SetUpStyles()
        {
            StrollStyle = (rnd.Next(0, 100) % 3) + 1;
            EatMovingStyle = (rnd.Next(0, 100) % 2) + 1;
        }

        private void SetUpSex()
        {
            lifeTime = rnd.Next(70, 120);
            if (rnd.Next(0, 100) < 50)
            {
                isMale = true;
            }
            else
            {
                isMale = false;
            }
        }

        public void ReduceLifetime()
        {
            lifeTime -= 1;
            return;
        }

        public void StrollByStyle(AnimalsController allAnimals)
        {
            switch (StrollStyle)
            {
                case 1:
                    {
                        Stroll();
                        break;
                    }
                case 2:
                    {
                        StrollNCells();
                        break;
                    }
                case 3:
                    {
                        StrollProbabilities(allAnimals, this);
                        break;
                    }
            }
        }

        private void Stroll()
        {
            //int[] rand = new int[] { -1, 1 };
            //currentLocation.First += 10 * rand[rnd.Next(0, rand.Length)];
            if (currentLocation.First - 10 > 0 && currentLocation.First + 10 < 1800)
            {
                currentLocation.First += 10 * rnd.Next(-1, 2);
            }

            if (currentLocation.Second - 10 > 0 && currentLocation.Second + 10 < 1000)
            {
                currentLocation.Second += 10 * rnd.Next(-1, 2);
            }
            return;
        }

        private void TameAnimal(Animal tamedAnimal, Human humanOwner)
        {
            tamedAnimal.owner = humanOwner;
            humanOwner.TamedAnimals.Add(tamedAnimal);
            return;
        }

        public void MoveByProbabilities(double averageX, double averageY)
        {
            if (currentLocation.First < averageX)
            {
                if (rnd.Next(0, 100) < 75) //вероятность 3/4
                {
                    currentLocation.First += 10;
                }
                else
                {
                    currentLocation.First -= 10;
                }
            }

            if (currentLocation.First > averageX)
            {
                if (rnd.Next(0, 100) < 75) //вероятность 3/4
                {
                    currentLocation.First -= 10;
                }
                else
                {
                    currentLocation.First += 10;
                }
            }

            if (currentLocation.Second < averageY)
            {
                if (rnd.Next(0, 100) < 75) //вероятность 3/4
                {
                    currentLocation.Second += 10;
                }
                else
                {
                    currentLocation.Second -= 10;
                }
            }

            if (currentLocation.Second > averageY)
            {
                if (rnd.Next(0, 100) < 75) //вероятность 3/4
                {
                    currentLocation.Second -= 10;
                }
                else
                {
                    currentLocation.Second += 10;
                }
            }
        }


        private void StrollProbabilities(AnimalsController allAnimals, Animal unit)
        {
            double commonX = 0;
            double commonY = 0;
            double averageX = 0;
            double averageY = 0;
            int amount = 0;

            var allAnimalUnits = allAnimals.giveAllUnits();

            for (int i = 0; i < allAnimalUnits.Count; i++)
            {

                if (allAnimalUnits[i].GetType().Name.ToString() == unit.GetType().Name)
                {
                    commonX += allAnimalUnits[i].GetLocation().First;
                    commonY += allAnimalUnits[i].GetLocation().Second;
                    amount += 1;
                }

            }

            averageX = commonX / amount;
            averageY = commonY / amount;

            MoveByProbabilities(averageX, averageY);
        }


        private void StrollNCells()
        {
            if (pastFood != null)
            {
                if (currentLocation.First - 10 > 0 && currentLocation.First + 10 < 1800 && currentLocation.First + 10 < pastFood.GetLocation().First + 5 * 10)
                {
                    currentLocation.First += 10 * rnd.Next(0, 2);
                }

                if (currentLocation.First - 10 > 0 && currentLocation.First + 10 < 1800 && currentLocation.First - 10 > pastFood.GetLocation().First - 5 * 10)
                {
                    currentLocation.First -= 10 * rnd.Next(0, 2);
                }

                if (currentLocation.Second - 10 > 0 && currentLocation.Second + 10 < 1000 && currentLocation.Second + 10 > pastFood.GetLocation().Second - 5 * 10)
                {
                    currentLocation.Second += 10 * rnd.Next(0, 2);
                }

                if (currentLocation.Second - 10 > 0 && currentLocation.Second + 10 < 1000 && currentLocation.Second - 10 < pastFood.GetLocation().Second + 5 * 10)
                {
                    currentLocation.Second -= 10 * rnd.Next(0, 2);
                }
            }
            else
            {
                Stroll();
            }
        }

        public void ChangeHungary()
        {
            if (isHungry)
            {
                isHungry = false;
            }
            else
            {
                isHungry = true;
            }
        }

        public void ReduceSatiety(double satietyCoef)
        {
            satiety = Convert.ToInt32((satiety - 2 * rnd.Next(1, 10)) * satietyCoef);
            if (satiety <= 0)
            {
                satiety = 0;
            }

            if (satiety == 0 && isHungry == false)
            {
                ChangeHungary();
            }
        }



        public void DieByHunger(AnimalsController allAnimals)
        {
            allAnimals.DeleteDeadAnimal(this);
        }

        public Boolean TakeDamageByHunger()
        {
            healthPoints = healthPoints - 2 * rnd.Next(1, 5);
            if (healthPoints <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double CountDistance(Cell<int, int> currentLocation, Cell<int, int> unitLocation)
        {
            var distanceX = currentLocation.First - unitLocation.First;
            var distanceY = currentLocation.Second - unitLocation.Second;
            return Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));
        }


        public virtual dynamic FindPathTo(AnimalsController allAnimals, List<Plant> uneatablePlants)
        {
            var allPlants = allAnimals.giveAllPlants();
            var allPlantsUnits = allPlants.giveAllUnits();
            var minDistance = 100000000.0;
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

        internal void CheckIfItIsLastFood(AnimalsController allAnimals)
        {
            if (allAnimals.giveAllPlants().giveAllUnits().Count - 1 == 0)
            {
                MessageBox.Show("Game Over!");
                Application.Exit();
            }

        }

        public virtual void eatFood(dynamic currentFood, AnimalsController allAnimals)
        {
            pastFood = currentFood;
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

        private void MoveToOwner(Human owner) 
        {
            if (currentLocation.First < owner.GetLocation().First)
            {
                currentLocation.First += 10;
            }

            if (currentLocation.First > owner.GetLocation().First)
            {
                currentLocation.First -= 10;
            }

            if (currentLocation.Second < owner.GetLocation().Second)
            {
                currentLocation.Second += 10;
            }

            if (currentLocation.Second > owner.GetLocation().Second)
            {
                currentLocation.Second -= 10;
            }
        }

        public void MoveStepToFoodByStyle(dynamic Food, AnimalsController allAnimals, Animal currentAnimal)
        {
            switch (EatMovingStyle)
            {
                case 1:
                    {
                        Moving4Cells(Food, allAnimals, currentAnimal);
                        break;
                    }
                case 2:
                    {
                        Moving8Cells(Food, allAnimals, currentAnimal);
                        break;
                    }
                case 3:
                    {
                        Moving4EuclidCells(Food, allAnimals, currentAnimal);
                        break;
                    }
            }
        }

        //алгоритмы движения к пище
        public void Moving4Cells(dynamic Food, AnimalsController allAnimals, dynamic currentAnimal)
        {
            if (currentLocation.First < Food.GetLocation().First)
            {
                currentLocation.First += 10;
            }
            else
            {
                if (currentLocation.First > Food.GetLocation().First)
                {
                    currentLocation.First -= 10;
                }
                else
                {
                    if (currentLocation.Second < Food.GetLocation().Second)
                    {
                        currentLocation.Second += 10;
                    }
                    else
                    {
                        if (currentLocation.Second > Food.GetLocation().Second)
                        {
                            currentLocation.Second -= 10;
                        }
                    }
                }
            }



            if ((Food.GetLocation().First == currentLocation.First) && (Food.GetLocation().Second == currentLocation.Second))
            {
                if (Food.GetType().BaseType.Name == "Animal" && currentAnimal.GetType().Name == "Human")
                {
                    if (Food.canBeTamed == true)
                    {
                        TameAnimal(Food, currentAnimal);
                    }
                    else
                    {
                        eatFood(Food, allAnimals);
                    }
                }
                else
                {
                    eatFood(Food, allAnimals);
                }
            }

        }

        private void Moving8Cells(dynamic Food, AnimalsController allAnimals, dynamic currentAnimal)
        {
            if (currentLocation.First < Food.GetLocation().First)
            {
                currentLocation.First += 10;
            }

            if (currentLocation.First > Food.GetLocation().First)
            {
                currentLocation.First -= 10;
            }

            if (currentLocation.Second < Food.GetLocation().Second)
            {
                currentLocation.Second += 10;
            }

            if (currentLocation.Second > Food.GetLocation().Second)
            {
                currentLocation.Second -= 10;
            }

            if ((Food.GetLocation().First == currentLocation.First) && (Food.GetLocation().Second == currentLocation.Second))
            {
                if (Food.GetType().BaseType.Name == "Animal" && currentAnimal.GetType().Name == "Human")
                {
                    if (Food.canBeTamed == true)
                    {
                        TameAnimal(Food, currentAnimal);
                    }
                    else
                    {
                        eatFood(Food, allAnimals);
                    }
                }
                else
                {
                    eatFood(Food, allAnimals);
                }
            }


        }


        private void Moving4EuclidCells(dynamic Food, AnimalsController allAnimals, dynamic currentAnimal)
        {
            var betterLocation = currentLocation;
            double minDistance = CountDistance(currentLocation, Food.GetLocation());

            var currentLocationCopy = currentLocation;
            currentLocationCopy.First += 10;
            if (CountDistance(currentLocationCopy, Food.GetLocation()) <= minDistance)
            {
                betterLocation = currentLocationCopy;
                minDistance = CountDistance(currentLocationCopy, Food.GetLocation());
            }


            currentLocationCopy = currentLocation;
            currentLocationCopy.First -= 10;
            if (CountDistance(currentLocationCopy, Food.GetLocation()) <= minDistance)
            {
                betterLocation = currentLocationCopy;
                minDistance = CountDistance(currentLocationCopy, Food.GetLocation());
            }

            currentLocationCopy = currentLocation;
            currentLocationCopy.Second += 10;
            if (CountDistance(currentLocationCopy, Food.GetLocation()) <= minDistance)
            {
                betterLocation = currentLocationCopy;
                minDistance = CountDistance(currentLocationCopy, Food.GetLocation());
            }

            currentLocationCopy = currentLocation;
            currentLocationCopy.Second -= 10;
            if (CountDistance(currentLocationCopy, Food.GetLocation()) <= minDistance)
            {
                betterLocation = currentLocationCopy;
                minDistance = CountDistance(currentLocationCopy, Food.GetLocation());
            }



            if ((Food.GetLocation().First == currentLocation.First) && (Food.GetLocation().Second == currentLocation.Second))
            {
                if (Food.GetType().BaseType.Name == "Animal" && currentAnimal.GetType().Name == "Human")
                {
                    if (Food.canBeTamed == true)
                    {
                        TameAnimal(Food, currentAnimal);
                    }
                    else
                    {
                        eatFood(Food, allAnimals);
                    }
                }
                else
                {
                    eatFood(Food, allAnimals);
                }
            }

        }


        private Animal PathToPartner(AnimalsController allAnimals, Animal currentAnimal)
        {
            var minDistance = 100000000.0;

            var allAnimalUnits = allAnimals.giveAllUnits();
            dynamic nearestPartner = null;
            dynamic currentPartner = null;

            for (int i = 0; i < allAnimalUnits.Count; i++)
            {
                currentPartner = allAnimalUnits[i];


                if (currentPartner.GetType().ToString() == currentAnimal.GetType().ToString() && currentPartner.CheckIfIsHungry() == false && currentPartner.CheckIfItCanProduce() == true)
                {
                    
                    if (isMale)
                    {
                        if (!currentPartner.CheckIfIsMale())
                        {
                            var unitLocation = currentPartner.GetLocation();
                            var currentDistance = CountDistance(currentLocation, unitLocation);

                            if (currentDistance < minDistance)
                            {
                                minDistance = currentDistance;
                                nearestPartner = currentPartner;
                            }
                        }
                    }
                    else
                    {
                        if (currentPartner.CheckIfIsMale())
                        {
                            var unitLocation = currentPartner.GetLocation();
                            var currentDistance = CountDistance(currentLocation, unitLocation);

                            if (currentDistance < minDistance)
                            {
                                minDistance = currentDistance;
                                nearestPartner = currentPartner;
                            }
                        }
                    }

                }

            }
            return nearestPartner;
        }

        private void ChangeProduceBlocking()
        {
            if (canProduce)
            {
                ProduceBlockTimer = 10;
                canProduce = false;
            }
            else
            {
                canProduce = true;
            }
        }

        public Boolean CheckIfIsFoodHealthy() 
        {
            return true;
        }

        public void ReduceProduceBlockTimer()
        {
            ProduceBlockTimer -= 1;
            if (ProduceBlockTimer == 0)
            {
                ChangeProduceBlocking();
            }
        }

        public void MoveStepToDate(Animal animalPartner, AnimalsController allAnimals)
        {
            if (currentLocation.First < animalPartner.GetLocation().First)
            {
                currentLocation.First += 10;
            }

            if (currentLocation.First > animalPartner.GetLocation().First)
            {
                currentLocation.First -= 10;
            }

            if (currentLocation.Second < animalPartner.GetLocation().Second)
            {
                currentLocation.Second += 10;
            }

            if (currentLocation.Second > animalPartner.GetLocation().Second)
            {
                currentLocation.Second -= 10;
            }

            if ((animalPartner.GetLocation().First == currentLocation.First) && (animalPartner.GetLocation().Second == currentLocation.Second))
            {
                Reproduce(allAnimals, this);
                ChangeProduceBlocking();
            }
        }

        private void Reproduce(AnimalsController allAnimals, Animal currentAnimal)
        {
            if (isMale == false)
            {
                string type = currentAnimal.GetType().Name;
                Dictionary<string, Animal> typesList = new Dictionary<string, Animal>();

                typesList.Add("Bear", new Bear());
                typesList.Add("Beaver", new Beaver());
                typesList.Add("Fox", new Fox());
                typesList.Add("Horse", new Horse());
                typesList.Add("Pig", new Pig());
                typesList.Add("Rabbit", new Rabbit());
                typesList.Add("Rat", new Rat());
                typesList.Add("Tiger", new Tiger());
                typesList.Add("Wolf", new Wolf());
                typesList.Add("Human", new Human());

                Animal newAnimal = typesList[type];

                newAnimal.initialize();
                allAnimals.addAnimal(newAnimal.Born(new Cell<int, int>(currentLocation.First + (10 * rnd.Next(-1, 1)), currentLocation.Second + (10 * rnd.Next(-1, 1))), 40, 100, false, AnimalColor));
            }

        }


        public Animal Born(Cell<int, int> givingLocation, int healthPoints, int satiety, Boolean isHungry, Color AnimalColor)
        {
            this.currentLocation.First = givingLocation.First;
            this.currentLocation.Second = givingLocation.Second;
            this.healthPoints = healthPoints * rnd.Next(5, 15);
            this.satiety = 100;
            this.isHungry = isHungry;
            this.AnimalColor = AnimalColor;
            SetUpStyles();
            SetUpSex();
            string Coord = "Spawn Animal at: X" + currentLocation.First.ToString() + "; Y " + currentLocation.Second.ToString();
            MessageBox.Show(Coord);
            return this;
        }

        private void Hibernate() 
        {
            healthPoints = 80;
            return;
        }

        public virtual void initialize() 
        {
            imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\wolf.png");
        }

        public virtual void EatExistingFood() 
        {
            return;
        }

        private void AskForFood(Human owner) 
        {
            satiety += 100;
            owner.GiveFoodFromBackpack();
            return;
        }

        private void UseAbility(AnimalsController allAnimals, double satietyCoef) 
        {
            if (ability == "boost") 
            {
                if (owner.currentFood != null) 
                {
                    owner.MoveStepToFoodByStyle(currentFood, allAnimals, this);
                }
                else 
                {
                    owner.StrollByStyle(allAnimals);
                }
            }
            else if (ability == "fillBackpack") 
            {
                for (int i = 1; i <= 4 - owner.getBackpack().Count; i++) 
                {
                    Meat meat = new Meat();
                    owner.CollectFoodToBackpack(meat);
                }
                DieByHunger(allAnimals);
            }
            else 
            {
                owner.satiety = 200;
                DieByHunger(allAnimals);
            }
            return;
        }



        
        
        
        public virtual void FindResource(ResourcesController allResources, AnimalsController allAnimals, BuildingsController allBuildings)
        {
            
        }

        private void MoveToRecourse()
        {
            
        }

        public virtual void MakeTurn(AnimalsController allAnimals, double satietyCoef, string CurrentSeason, ResourcesController allResources, BuildingsController allBuildings)
        {
            ReduceLifetime();
            if (lifeTime <= 0)
            {
                DieByHunger(allAnimals);
            }
            else
            {
                if (CurrentSeason == "winter" && isCanHibernate == true)
                {
                    Hibernate();
                }

                else
                {
                    if (!isHungry)
                    {   
                        if (owner != null) 
                        {
                            MoveToOwner(owner);
                            ReduceSatiety(satietyCoef);
                            if (rnd.Next(0, 100) < 20) 
                            {
                                UseAbility(allAnimals, satietyCoef);
                            }
                        }

                        else if (canProduce)
                        {

                            if (partner == null)
                            {
                                partner = PathToPartner(allAnimals, this);
                            }

                            if (partner != null)
                            {
                                partner.SetCoupleWith(this);
                                
                                if (this.GetType().ToString() == "Human")
                                {

                                    if (currentResource == null)
                                    {
                                        FindResource(allResources, allAnimals, allBuildings) ;
                                    }
                                    
                                    ReduceSatiety(satietyCoef);
                                }
                                else
                                {
                                    MoveStepToDate(partner, allAnimals);
                                    ReduceSatiety(satietyCoef); 
                                }
                                
                                
                            }
                            else
                            {
                                
                                StrollByStyle(allAnimals);
                                ReduceSatiety(satietyCoef);
                                
                            }


                        }
                        else
                        {
                            
                            
                            StrollByStyle(allAnimals);
                            ReduceSatiety(satietyCoef);
                            ReduceProduceBlockTimer();
                        }
                    }
                    else
                    {
                        if (owner != null) 
                        {
                            AskForFood(owner);
                            MoveToOwner(owner);
                        }

                        currentFood = FindPathTo(allAnimals, uneatablePlants);

                        EatExistingFood();
                        MoveStepToFoodByStyle(currentFood, allAnimals, this);

                        if (satiety >= 0)
                        {
                            if (TakeDamageByHunger())
                            {
                                DieByHunger(allAnimals);
                            }
                        }
                       
                    }
                }
            }
        }

        public Cell<int, int> ReturnLocation()
        {
            return currentLocation;
        }

        public Boolean CheckIfIsHungry()
        {
            return isHungry;
        }

        public Boolean CheckIfItCanProduce()
        {
            return canProduce;
        }

        public Boolean CheckIfIsMale()
        {
            return isMale;
        }

        public Color getAnimalColor()
        {
            return AnimalColor;
        }

        public Cell<int, int> GetLocation()
        {
            return currentLocation;
        }

        public void SetCoupleWith(Animal SecondPartner)
        {
            partner = SecondPartner;
        }

        public Image GetImage() 
        {
            return imageToDraw;
        }

    }






}
