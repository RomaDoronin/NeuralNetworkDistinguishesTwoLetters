using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perzeptron
{
    public partial class Form1 : Form
    {
        private int penWidth;
        private int gridSize;
        private bool isPressed;
        private Point currentPoint;
        private Point prevPoint;

        private Color myWhite;
        private Color myBlack;

        private Bitmap screenshot;

        private PerNetWork perNetWork;

        private List<List<float>> gridG;

        public Form1()
        {
            penWidth = 2;
            gridG = new List<List<float>>();
            isPressed = false;
            perNetWork = new PerNetWork();
            gridSize = perNetWork.GetGridSize();

            InitializeComponent();

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            screenshot = new Bitmap(width, height);
        }

        private void CleanScreenshot()
        {
            for (int x = 0; x < screenshot.Height; x++)
            {
                for (int y = 0; y < screenshot.Width; y++)
                {
                    screenshot.SetPixel(x, y, myWhite);
                }
            }
        }

        private void DrawImagePixel(int x, int y)
        {
            for (int i = x - penWidth; i <= x + penWidth; i++)
            {
                for (int j = y - penWidth; j <= y + penWidth; j++)
                {
                    screenshot.SetPixel(i, j, Color.Black);
                }
            }
        }

        private Bitmap BinarizationImage(Bitmap image)
        {
            Bitmap binImage = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color curr = image.GetPixel(i, j);
                    if ((curr.R == 0) && (curr.G == 0) && (curr.B == 0))
                    {
                        binImage.SetPixel(i, j, curr);
                    }
                    else if ((curr.R == 255) && (curr.G == 255) && (curr.B == 255))
                    {
                        binImage.SetPixel(i, j, curr);
                    }
                    else
                    {
                        double toBlack = Math.Pow(255 - curr.R, 2) + Math.Pow(255 - curr.G, 2) + Math.Pow(255 - curr.B, 2);
                        double toWhite = Math.Pow(curr.R, 2) + Math.Pow(curr.G, 2) + Math.Pow(curr.B, 2);
                        if (toBlack < toWhite)
                        {
                            binImage.SetPixel(i, j, Color.Black);
                        }
                        else
                        {
                            binImage.SetPixel(i, j, Color.White);
                        }
                    }
                }
            }

            return binImage;
        }

        private List<List<float>> ImageToGrid(Bitmap bitmap)
        {
            List<List<float>> grid = new List<List<float>>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                List<float> gridRow = new List<float>();
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if ((pixelColor.A == 0) && (pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                    {
                        gridRow.Add(0);
                    }
                    else
                    {
                        gridRow.Add(1);
                    }
                }

                grid.Add(gridRow);
            }

            return grid;
        }

        private void PrintGrid(List<List<float>> grid)
        {
            string gridStr = "";

            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    gridStr += grid[y][x].ToString() + " ";
                }

                gridStr += '\n';
            }

            gridLable.Text = gridStr;
        }

        private void PrintBitmap(Bitmap bitmap)
        {
            string grid = "";
            Dictionary<Color, int> dict = new Dictionary<Color, int>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (dict.ContainsKey(pixelColor))
                    {
                        dict[pixelColor]++;
                    }
                    else
                    {
                        dict[pixelColor] = 1;
                    }

                    if ((pixelColor.A == 0) && (pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                    {
                        myWhite = pixelColor;
                        grid += "_";
                    }
                    else
                    {
                        myBlack = pixelColor;
                        grid += "1";
                    }
                }

                grid += '\n';
            }

            gridLable.Text = grid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CleanScreenshot();
            pictureBox1.Image = screenshot;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap gridImage = new Bitmap(screenshot, new Size(gridSize, gridSize));
            PrintBitmap(gridImage);

            gridG = ImageToGrid(gridImage);
            PrintGrid(gridG);
            int Result = perNetWork.RunIteration(gridG);
            if (Result == 1)
            {
                label1.Text = "Perc = A";
            }
            else
            {
                label1.Text = "Perc = B";
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            currentPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                prevPoint = currentPoint;
                currentPoint = e.Location;

                label1.Text = currentPoint.ToString();

                if ((currentPoint.X > (0   + penWidth)) &&
                    (currentPoint.X < (100 - penWidth)) &&
                    (currentPoint.Y > (0   + penWidth)) && 
                    (currentPoint.Y < (100 - penWidth)))
                {
                    DrawImagePixel(prevPoint.X, prevPoint.Y);
                    pictureBox1.Image = screenshot;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void printWeightButton_Click(object sender, EventArgs e)
        {
            if (printWeightButton.Text == "Print Weight")
            {
                weightLable.Text = perNetWork.ToString();
                weightLable.Visible = true;
                printWeightButton.Text = "Print Grid";
            }
            else
            {
                weightLable.Visible = false;
                gridLable.Visible = true;
                printWeightButton.Text = "Print Weight";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            perNetWork.IncWeight(gridG);
            weightLable.Text = perNetWork.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            perNetWork.DecWeight(gridG);
            weightLable.Text = perNetWork.ToString();
        }
    }
}
