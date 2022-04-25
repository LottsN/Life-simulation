using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulator2
{
    class PlantsController
    {
        private List<Plant> allUnits = new List<Plant>();
        private int SeasonDamage = 0;
        public GameSession CurrentGameSession;

        private void CheckSeason()
        {
            if (CurrentGameSession.GetSeason() == "summer")
            {
                SeasonDamage = 0;
            }
            else
            {
                SeasonDamage = 10;
            }
        }

        public void DeleteFaded(Plant fadedPlant)
            {
            allUnits.Remove(fadedPlant);
            fadedPlant = null;
            return;
            }

            public void addPlant(Plant unit)
            {
            allUnits.Add(unit);
            return;
            }

            private void CheckIfIsThereFood()
            {
            if (allUnits.Count == 0)
            {
                MessageBox.Show("Game Over!");
                Application.Exit();
            }
        }

        public void FoodsTurn()
            {
            CheckIfIsThereFood();
            for (int i = 0; i < allUnits.Count; i++)
                {
                    CheckSeason();
                    allUnits[i].MakeTurn(this, SeasonDamage, CurrentGameSession.GetSeason());
                }
            }

        public List<Plant> giveAllUnits()
            {
                return allUnits;
            }
        }
    
}
