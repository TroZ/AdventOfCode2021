using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day11 : Day
    {

        int flashcount = 0;
        int width = 0;
        int height = 0;

        /*
        SolidBrush[] colors = new SolidBrush[10];
        /*/
        ImageMagick.MagickColor[] colors = new ImageMagick.MagickColor[10];
        //*/

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"5483143223",
"2745854711",
"5264556173",
"6141336146",
"6357385478",
"4167524645",
"2176841721",
"6882881134",
"4846848554",
"5283751526"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");
            /*
            colors[0] = new SolidBrush(Color.Yellow);
            colors[1] = new SolidBrush(Color.Navy);
            colors[2] = new SolidBrush(Color.MediumBlue);
            colors[3] = new SolidBrush(Color.Blue);
            colors[4] = new SolidBrush(Color.RoyalBlue);
            colors[5] = new SolidBrush(Color.SteelBlue);
            colors[6] = new SolidBrush(Color.DeepSkyBlue);
            colors[7] = new SolidBrush(Color.Turquoise);
            colors[8] = new SolidBrush(Color.Aquamarine);
            colors[9] = new SolidBrush(Color.PaleTurquoise);
            
            /*/
            colors[0] = ImageMagick.MagickColors.Yellow;
            colors[1] = ImageMagick.MagickColors.Navy;
            colors[2] = ImageMagick.MagickColors.MediumBlue;
            colors[3] = ImageMagick.MagickColors.Blue;
            colors[4] = ImageMagick.MagickColors.RoyalBlue;
            colors[5] = ImageMagick.MagickColors.SteelBlue;
            colors[6] = ImageMagick.MagickColors.DeepSkyBlue;
            colors[7] = ImageMagick.MagickColors.Turquoise;
            colors[8] = ImageMagick.MagickColors.Aquamarine;
            colors[9] = ImageMagick.MagickColors.PaleTurquoise;
            
            ImageMagick.MagickImageCollection gif = Program.getAnimatedImage();
            //*/

            string[] data = getData();

            width = data[0].Length;
            height = data.Length;

            int[,] one = new int[width, height];

            for(int y = 0; y < height; y++)
            {
                char[] line = data[y].ToCharArray();
                for(int x = 0; x < width; x++)
                {
                    one[x, y] = int.Parse("" + line[x]);
                }
            }

            //save initial state
            {
                /*
                Bitmap b = new Bitmap(5 * width+1, 5 * height+1);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.Black);
                /*/
                ImageMagick.MagickImage img = new ImageMagick.MagickImage(ImageMagick.MagickColors.Black, 51, 51);
                ImageMagick.Drawables draw = new ImageMagick.Drawables();
                draw.FillColor(ImageMagick.MagickColors.Black).Rectangle(0, 0, 51, 51);
                //*/
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (one[x, y] != 0)
                        {
                            //g.FillRectangle(colors[one[x, y]], x * 5 + 1, y * 5 + 1, 4, 4);
                            draw.FillColor(colors[one[x, y]]).Rectangle(x * 5 + 1, y * 5 + 1, x * 5 + 4, y * 5 + 4);

                        }
                        else
                        {
                            //g.FillRectangle(colors[one[x, y]], x * 5 , y * 5 , 6, 6);
                            draw.FillColor(colors[one[x, y]]).Rectangle(x * 5, y * 5, x * 5 + 5, y * 5 + 5); ;
                        }
                    }
                }
                draw.Draw(img);
                Program.addAnimatedImageFrame(gif, img);
                //Program.saveImg(b, 11, i);
            }

            //main loop
            for (int i = 0; i < 1000; i++)
            {
                //add 1
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        one[x, y]++;
                    }
                }

                //check for flashes
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (one[x, y] > 9)
                        {
                            flash(one, x, y);
                        }
                    }
                }

                if (i == 99)
                {
                    Console.WriteLine("Total flashes at step 100 (part1): " + flashcount);
                }


                //check for all flash
                bool all = true;
                for (int y = 0; all && y < height; y++)
                {
                    for (int x = 0; all && x < width; x++)
                    {
                        all = one[x, y] == 0;
                    }
                }
                if (all)
                {
                    Console.WriteLine("All flashed at " + (i + 1));
                }


                //print
                //*
                Console.WriteLine("Step " + (i + 1) + ", flashes "+flashcount+" :");
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Console.Write(one[x, y]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                //*/

                //image
                if (i < 600)
                {
                    /*
                    Bitmap b = new Bitmap(5 * width+1, 5 * height+1);
                    Graphics g = Graphics.FromImage(b);
                    g.Clear(Color.Black);
                    /*/
                    ImageMagick.MagickImage img = new ImageMagick.MagickImage(new ImageMagick.MagickColor("#000000"), 51, 51);
                    ImageMagick.Drawables draw = new ImageMagick.Drawables();
                    //*/
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0;x < width; x++)
                        {
                            if (one[x, y] != 0)
                            {
                                //g.FillRectangle(colors[one[x, y]], x * 5 + 1, y * 5 + 1, 4, 4);
                                draw.FillColor(colors[one[x, y]]).Rectangle(x * 5 + 1, y * 5 + 1, x * 5 + 3, y * 5 + 3);

                            }
                            else
                            {
                                //g.FillRectangle(colors[one[x, y]], x * 5 , y * 5 , 6, 6);
                                draw.FillColor(colors[one[x, y]]).Rectangle(x * 5, y * 5, x * 5 + 5, y * 5 + 5); ;
                            }
                        }
                    }
                    draw.Draw(img);
                    Program.addAnimatedImageFrame(gif, img, (i<599)?10:500);
                    //Program.saveImg(b, 11, i);
                }
                
            }

            Program.saveAnimatedImage("Day11", gif, 256);

            
        }

        void flash(int[,] data,int x, int y)
        {
            flashcount++;
            data[x, y] = 0;
            for(int i = -1; i < 2; i++)
            {

                if (y - 1 >= 0 && x + i >= 0 && x + i < width && data[x + i, y - 1] != 0)
                {
                    data[x + i, y - 1]++;
                }
                if (y + 1 < height && x + i >= 0 && x + i < width && data[x + i, y + 1] != 0)
                {
                    data[x + i, y + 1]++;
                }
                if (x + i >= 0 && x + i < width && i != 0 && data[x + i, y] != 0)
                {
                    data[x + i, y]++;
                }
            }

            for (int i = -1; i < 2; i++)
            {
                if (y - 1 >= 0 && x + i >= 0 && x + i < width && data[x + i, y - 1] > 9)
                {
                    flash(data,x + i, y - 1);
                }
                if (y + 1 < height && x + i >= 0 && x + i < width && data[x + i, y + 1] > 9)
                {
                    flash(data,x + i, y + 1);
                }
                if (x + i >= 0 && x + i < width && i != 0 && data[x + i, y] > 9)
                {
                    flash(data,x + i, y);
                }
            }
        }
    }
}
