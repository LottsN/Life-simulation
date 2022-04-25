using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace LifeSimulator2
{
    public partial class Form1 : Form
    {
        GameSession game = new GameSession();
        string CurrentSeason = "summer";
        dynamic currentSelectedUnit;

        [DllImport("Kernel32.dll")]
        static extern Boolean AllocConsole();
        public Form1()
        {
            InitializeComponent();
            this.picture.MouseWheel += new MouseEventHandler(ZoomInOut);
            this.picture.MouseClick += new MouseEventHandler(Clicking);
            DrawMap();
 
            game.GameStart(10, 10, 10);
            setGameSession();

        }

        private void Clicking(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowInfo(foundCoordinates(MousePosition.X, MousePosition.Y, game.ReternAllAnimals(), game.ReternAllFood()));
            }
            else
            {
                game.MainSession(1);
                RedrawUnits(game.ReternAllAnimals(), game.ReternAllFood(), game.GetResourcesController(), game.GetBuildingsController());
                ShowInfo(currentSelectedUnit);
            }
        }

        private dynamic foundCoordinates(int PosX, int PosY, List<Animal> allAnimals, List<Plant> allFood) 
        {
            currentSelectedUnit = null;
            //MessageBox.Show("X: " + (PosX / 10 * 10).ToString() + " Y " + (PosY / 10 * 10 - 20).ToString());
            PosX = PosX / 10 * 10;
            int PosYlow = (PosY / 10 * 10) - 20 - 10;
            int PosYhigh = (PosY / 10 * 10) - 20;

            for (int i = 0; i < allFood.Count(); i++)
            {
                var currentUnit = allFood[i];
                if (currentUnit.GetLocation().First == PosX)
                {
                    if (currentUnit.GetLocation().Second == PosYlow || currentUnit.GetLocation().Second == PosYhigh)
                    {
                        //MessageBox.Show(currentUnit.GetType().ToString());
                        currentSelectedUnit = currentUnit;
                    }
                }
            }

            for (int i = 0; i < allAnimals.Count(); i++)
            {
                var currentUnit = allAnimals[i];
                if (currentUnit.GetLocation().First == PosX)
                {
                    if (currentUnit.GetLocation().Second == PosYlow || currentUnit.GetLocation().Second == PosYhigh)
                    {
                        //MessageBox.Show(currentUnit.GetType().ToString());
                        currentSelectedUnit = currentUnit;
                    }
                }
            }

            return currentSelectedUnit;
        }

        private string BackpackSearch(dynamic currentSelectedUnit) 
        {
            string paw;
            var backpack = currentSelectedUnit.getBackpack();
            //lines.Add("Backpack: " + currentSelectedUnit.getBackpack().Count.ToString() + "/4");

            int P = 0;
            int F = 0;
            int M = 0;

            for (int i = 0; i < backpack.Count; i++) 
            {
                if (backpack[i].GetType().Name == "Meat") M += 1;
                if (backpack[i].GetType().Name == "Fruits") F += 1;
                if (backpack[i].GetType().Name == "Plant") P += 1;
            }

            paw = "Backpack: " + backpack.Count.ToString() + "/4: " + M.ToString() + "M, " + F.ToString() + "F, " + P.ToString() + "P";
            return paw;
        }

        private void ShowInfo(dynamic currentSelectedUnit) 
        {
            if (currentSelectedUnit == null) 
            {
                textBox1.Visible = false;
            }
            else
            {
                textBox1.Visible = true;
                if (currentSelectedUnit.GetType().Name == "Human") 
                {
                    List<string> lines = new List<string>();
                    string paw = "";
                    lines.Add("Human");
                    lines.Add("X: " + currentSelectedUnit.GetLocation().First.ToString() + "; Y: " + currentSelectedUnit.GetLocation().Second.ToString());
                    lines.Add("HP: " + currentSelectedUnit.healthPoints.ToString());
                    lines.Add("Satiety: " + currentSelectedUnit.satiety.ToString());
                    lines.Add(BackpackSearch(currentSelectedUnit));
                    if (currentSelectedUnit.partner != null) 
                    {
                        lines.Add("Partner X: " + currentSelectedUnit.GetLocation().First.ToString() + "; Y: " + currentSelectedUnit.GetLocation().Second.ToString());
                    }
                    else 
                    {
                        lines.Add("Partner: null");
                    }

                    if (currentSelectedUnit.TamedAnimals.Count != 0)
                    {
                        lines.Add("Tamed Animals:");

                        for (int i = 0; i < currentSelectedUnit.TamedAnimals.Count; i++) 
                        {
                            lines.Add(currentSelectedUnit.TamedAnimals[i].GetType().Name.ToString() + " - X: " + currentSelectedUnit.TamedAnimals[i].GetLocation().First.ToString()
                                + "; Y: " + currentSelectedUnit.TamedAnimals[i].GetLocation().Second.ToString());
                        }

                        lines.Add("Partner X: " + currentSelectedUnit.GetLocation().First.ToString() + "; Y: " + currentSelectedUnit.GetLocation().Second.ToString());
                    }
                    else
                    {
                        lines.Add("Tamed animals: null");
                    }


                    for (int i = 0; i < lines.Count; i++) 
                    {
                        paw += lines[i] + Environment.NewLine;
                    }

                    textBox1.Text = paw;
                }
                else 
                {
                    if (currentSelectedUnit.GetType().BaseType.Name == "AnimalHerbivorous" || currentSelectedUnit.GetType().BaseType.Name == "AnimalOmnivores" ||
                        currentSelectedUnit.GetType().BaseType.Name == "AnimalCarnivores")
                    {
                        List<string> lines = new List<string>();
                        string paw = "";
                        lines.Add(currentSelectedUnit.GetType().Name.ToString());
                        lines.Add("X: " + currentSelectedUnit.GetLocation().First.ToString() + "; Y: " + currentSelectedUnit.GetLocation().Second.ToString());
                        lines.Add("HP: " + currentSelectedUnit.healthPoints.ToString());
                        lines.Add("Satiety: " + currentSelectedUnit.satiety.ToString());
                        lines.Add("Type: " + currentSelectedUnit.GetType().BaseType.Name.ToString());

                        for (int i = 0; i < lines.Count; i++)
                        {
                            paw += lines[i] + Environment.NewLine;
                        }

                        textBox1.Text = paw;
                    }
                    else 
                    {
                        List<string> lines = new List<string>();
                        string paw = "";
                        lines.Add("Plant");
                        lines.Add("X: " + currentSelectedUnit.GetLocation().First.ToString() + "; Y: " + currentSelectedUnit.GetLocation().Second.ToString());
                        lines.Add("Type: " + currentSelectedUnit.GetType().Name.ToString());

                        for (int i = 0; i < lines.Count; i++)
                        {
                            paw += lines[i] + Environment.NewLine;
                        }

                        textBox1.Text = paw;
                    }
                }
            }

        }

        private void ZoomInOut(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Bitmap bmp = new Bitmap(picture.Image, picture.Image.Width + 100, picture.Image.Height + 100);
                Graphics graphic = Graphics.FromImage(bmp);
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                picture.Image = bmp;
            }
            else
            {
                if (picture.Image.Width - 100 > 0 && picture.Image.Height - 100 > 0)
                {
                    Bitmap bmp = new Bitmap(picture.Image, picture.Image.Width - 100, picture.Image.Height - 100);
                    Graphics graphic = Graphics.FromImage(bmp);
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    picture.Image = bmp;
                }
            }
        }

        private void setGameSession() 
        {
            game.GetAnimalsController().CurrentGameSession = game;
            game.GetPlantsController().CurrentGameSession = game;
            game.GetResourcesController().CurrentGameSession = game;
            game.GetBuildingsController().CurrentGameSession = game;
            return;
        }

        private void DrawMap()
        {
            Bitmap bmp = new Bitmap(1940, 1100);
            Graphics graphic = Graphics.FromImage(bmp);
            Pen pencil = new Pen(Color.Black, Width = 1);

            //трава
            CurrentSeason = game.CheckSeason();
            if (CurrentSeason == "summer")
            {
                Brush GroundBrush = new SolidBrush(Color.Green);
                graphic.FillRectangle(GroundBrush, 0, 0, bmp.Width, bmp.Height);
            }
            else
            {
                if (CurrentSeason == "autumn")
                {
                    Brush GroundBrush = new SolidBrush(Color.Yellow);
                    graphic.FillRectangle(GroundBrush, 0, 0, bmp.Width, bmp.Height);
                }
                else 
                {
                    if (CurrentSeason == "winter")
                    {
                        Brush GroundBrush = new SolidBrush(Color.LightBlue);
                        graphic.FillRectangle(GroundBrush, 0, 0, bmp.Width, bmp.Height);
                    }
                    else 
                    {
                        if (CurrentSeason == "spring")
                        {
                            Brush GroundBrush = new SolidBrush(Color.LightGreen);
                            graphic.FillRectangle(GroundBrush, 0, 0, bmp.Width, bmp.Height);
                        }
                    }
                }
            }



            for (int i = 0; i < bmp.Width; i++)
            {

                if (i % 10 == 0)
                {
                    graphic.DrawLine(pencil, i, 0, i, bmp.Height);
                }
            }

            for (int i = 0; i < bmp.Height; i++)
            {

                if (i % 10 == 0)
                {
                    graphic.DrawLine(pencil, 0, i, bmp.Width, i);
                }
            }

            graphic.DrawLine(pencil, bmp.Width, 0, bmp.Width, bmp.Height);
            graphic.DrawLine(pencil, 0, bmp.Height, bmp.Width, bmp.Height);

            picture.Image = bmp;
        }

        private void DrawRecources(Graphics graphic, ResourcesController allResources)
        {
            List<ResourceUnit> items = allResources.GetAllUnits();
            
            for (int i = 0; i < items.Count; i++)
            {
                var currentUnit = items[i];
                Brush PlantBrush = new SolidBrush(items[i].getColor());
                graphic.FillRectangle(PlantBrush, currentUnit.GetLocation().First + 1, currentUnit.GetLocation().Second + 1, 9, 9);
            }
        }

        private void DrawBuildings(Graphics graphic, BuildingsController allBuildings)
        {
            List<Buildings> items = allBuildings.GetAllUnits();

            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    var currentUnit = items[i];
                    Brush PlantBrush = new SolidBrush(items[i].getColor());
                    graphic.FillRectangle(PlantBrush, currentUnit.GetLocation().First + 1, currentUnit.GetLocation().Second + 1, 9, 9);
                }
            }
        }

        private void RedrawUnits(List<Animal> allAnimals, List<Plant> allFood, ResourcesController allResources, BuildingsController allBuildings)
        {
            DrawMap();
            Bitmap bmp = new Bitmap(picture.Image, picture.Image.Width, picture.Image.Height);
            Graphics graphic = Graphics.FromImage(bmp);

            for (int i = 0; i < allFood.Count; i++)
            {
                var currentUnit = allFood[i];
                var imageToDraw = currentUnit.GetImage();
                //Brush PlantBrush = new SolidBrush(allFood[i].getPlantColor());
                //string Coord = "current unit: " + currentUnit.GetLocation().First.ToString();
                //MessageBox.Show(Coord);
                //graphic.FillRectangle(PlantBrush, currentUnit.GetLocation().First + 1, currentUnit.GetLocation().Second + 1, 9, 9);
                graphic.DrawImage(imageToDraw, currentUnit.GetLocation().First + 1, currentUnit.GetLocation().Second + 1, 9, 9);
            }

            for (int i = 0; i < allAnimals.Count; i++)
            {
                var currentUnit = allAnimals[i];
                var imageToDraw = currentUnit.GetImage();
                //Brush AnimalBrush = new SolidBrush(currentUnit.getAnimalColor());
                //string Coord = "current unit: " + currentUnit.ReturnLocation().First.ToString();
                //MessageBox.Show(Coord);
                //graphic.FillRectangle(AnimalBrush, currentUnit.ReturnLocation().First + 1, currentUnit.ReturnLocation().Second + 1, 9, 9);
                graphic.DrawImage(imageToDraw, currentUnit.GetLocation().First + 1, currentUnit.GetLocation().Second + 1, 9, 9);
            }

            DrawRecources(graphic, allResources);
            DrawBuildings(graphic, allBuildings);
            
            picture.Image = bmp;
        }


   

        /*private void CheckSeason() 
        {
            if (YearDay < 91 && YearDay >=1) CurrentSeason = "summer";
            else
            {
                if (YearDay < 182 && YearDay >= 91) CurrentSeason = "autumn";
                else 
                {
                    if (YearDay < 273 && YearDay >= 182) CurrentSeason = "winter";
                    else
                    {
                        CurrentSeason = "spring";
                        if (YearDay > 365) YearDay = 1;
                    }
                }
            }
            
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void pictureBoxInfo_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
