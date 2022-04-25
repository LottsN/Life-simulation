using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LifeSimulator2
{
    class Plant
    {
        protected Cell<int, int> location = new Cell<int, int>(0, 0);
        protected int lifeTime;
        private int seedsAmount;
        private int satietyPoints;
        private int seedsPeriod;
        protected Boolean isEaten = false;
        private Random rnd = new Random();
        internal Boolean isEatable = true;
        internal Boolean isCanSeed = true;
        internal Boolean isHealthy = true;
        internal Color PlantColor = Color.Green;
        protected Boolean blockEatable;
        protected Boolean blockReproductionAndSeeding;
        internal Image imageToDraw = Image.FromFile("C:\\Users\\Lotts\\source\\repos\\LifeSimulator2\\LifeSimulator2\\Images\\seed.png");

        private void RenewLifeTime()
        {
            lifeTime = lifeTime - 1;
        }

        private void Fade(PlantsController allPlatns)
        {
            allPlatns.DeleteFaded(this);
        }

        public Plant seeds(Cell<int, int> givingLocation, int lifeTime, int seedsAmount, int satietyPoints, Boolean IsEatable, Boolean IsCanSeed, Boolean IsHealthy, Color PlantColor)
        {
            location.First = givingLocation.First * 10 * rnd.Next(1, 180);
            location.Second = givingLocation.Second * 10 * rnd.Next(1, 100);
            string Coord = "Spawn Food at: X" + location.First.ToString() + "; Y " + location.Second.ToString();
            MessageBox.Show(Coord);
            this.lifeTime = lifeTime * 1;
            this.seedsAmount = seedsAmount * 2;
            this.satietyPoints = satietyPoints * rnd.Next(1, 15);
            this.seedsPeriod = (lifeTime / seedsAmount) + 1;
            this.isEatable = IsEatable;
            this.isCanSeed = IsCanSeed;
            this.isHealthy = IsHealthy;
            this.PlantColor = PlantColor;
            return this;
        }

        public virtual void CheckAge(int SeasonDamage, string CurrentSeason)
        {
            if (80 >= lifeTime && lifeTime > 70) //семя
            {
                blockEatable = true;
                blockReproductionAndSeeding = true;
                PlantColor = Color.Red;
                lifeTime += SeasonDamage;
            }

            if (70 >= lifeTime && lifeTime > 50) //росток
            {
                blockEatable = false;
                blockReproductionAndSeeding = true;
                PlantColor = Color.Yellow;
                lifeTime += SeasonDamage;
            }

            if (50 >= lifeTime && lifeTime > 10) //растение
            {
                blockEatable = false;
                blockReproductionAndSeeding = false;
                PlantColor = Color.Pink;
                lifeTime += SeasonDamage;
            }

            if (10 >= lifeTime && lifeTime > 0) //пожилое
            {
                blockEatable = true;
                blockReproductionAndSeeding = true;
                PlantColor = Color.Black;
                lifeTime += SeasonDamage;
            }

        }

        public virtual void SeedFood(PlantsController allPlatns)
        {
            var newPlant = new Plant();
            allPlatns.addPlant(newPlant.seeds(new Cell<int, int>(1, 1), 80, 4, 30, this.isEatable, this.isCanSeed, this.isHealthy, this.PlantColor));
        }

        private void CheckSeason(string CurrentSeason) 
        {
            if (CurrentSeason == "summer") blockReproductionAndSeeding = false;
            if (CurrentSeason == "winter") blockReproductionAndSeeding = true;
        }

        public virtual void MakeTurn(PlantsController allPlatns, int SeasonDamage, string CurrentSeason)
        {
            if (isEaten)
            {
                Fade(allPlatns);
            }
            else
            {

                CheckSeason(CurrentSeason);
                CheckAge(SeasonDamage, CurrentSeason);

                if (lifeTime % seedsPeriod == 0)
                {
                    if (isCanSeed == true && blockReproductionAndSeeding == false)
                    {
                        SeedFood(allPlatns);                        

                        //сбросить плоды
                        if (isEatable)
                        {
                            var newFruit = new Fruits();
                            allPlatns.addPlant(newFruit.drop(this.isHealthy, location.First + (10 * rnd.Next(-1, 1)), location.Second + (10 * rnd.Next(-1, 1))));
                        }
                    }
                }
                RenewLifeTime();

                if (lifeTime <= 0)
                {
                    Fade(allPlatns);
                }
            }
        }

        public Cell<int, int> GetLocation()
        {
            return location;
        }

        public int BecomeEaten()
        {
            isEaten = true;
            return satietyPoints;
        }

        public Color getPlantColor()
        {
            return PlantColor;
        }

        public Boolean CheckIsFoodEatable()
        {
            if (blockEatable)
            {
                return false;
            }
            else
            {
                return isEatable;
            }
            
        }

        public Image GetImage()
        {
            return imageToDraw;
        }

        public Boolean CheckIfIsFoodHealthy()
        {
            return isHealthy;
        }
    }
    /*
    class Base
    {
        // внутренние поля класса Base
        private int private_Item;
        protected int protected_Item;
        internal int internal_Item;
        protected internal int protIntern_Item;
        public int public_Item;
    }

    // Производный класс от класса Base
    class Derived : Base
    {
        // метод, который изменяет поля класса Base
        void Method()
        {
            protected_Item = 10;
            internal_Item = 20;
            protIntern_Item = 30;
            public_Item = 40;
            // private_item = 50; - ошибка, элемент недоступен
        }
    }
    */
    
}
