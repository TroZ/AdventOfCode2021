using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day15 : Day
    {
        int w = 0;
        int h = 0;

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/

            string[] lines = {
"1163751742",
"1381373672",
"2136511328",
"3694931569",
"7463417111",
"1319128137",
"1359912421",
"3125421639",
"1293138521",
"2311944581"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            w = data[0].Length;
            h = data.Length;
            int[,] riskmap = new int[w, h];
            int[,] totalrisk = new int[w, h];

            //init data
            for (int y = 0; y < h; y++)
            {
                char[] line = data[y].ToCharArray();
                for (int x = 0; x < w; x++)
                {
                    riskmap[x, y] = int.Parse("" + line[x]);
                    totalrisk[x, y] = int.MaxValue;
                }
            }
            totalrisk[0, 0] = 0;

            calcRisk(riskmap, totalrisk);

            Console.WriteLine("Min Cost Path: " + totalrisk[w - 1, h - 1]);
            Console.WriteLine();
            Console.WriteLine();

            //part2
            int ww = w;
            int hh = h;
            w = w * 5;
            h = h * 5;
            riskmap = new int[w, h];
            totalrisk = new int[w, h];

            //init data
            for (int y = 0; y < hh; y++)
            {
                char[] line = data[y].ToCharArray();
                for (int x = 0; x < ww; x++)
                {
                    int val = int.Parse("" + line[x]);
                    for(int i = 0; i < 5; i++)
                    {
                        for(int j = 0; j < 5; j++)
                        {
                            int assignVal = (val + (i + j));
                            while (assignVal > 9)
                                assignVal -= 9;
                            riskmap[x + (i * ww), y + (j * hh)] = assignVal;
                            totalrisk[x + (i * ww), y + (j * hh)] = int.MaxValue;
                        }
                    }
                }
            }
            totalrisk[0, 0] = 0;

            //print(riskmap);

            calcRisk(riskmap, totalrisk);
            //calcRiskImg(riskmap, totalrisk);

            //print(totalrisk);

            Console.WriteLine("Min Cost Path: " + totalrisk[w - 1, h - 1]);

            //paint(totalrisk);
        }

        private void calcRisk(int[,] riskmap, int[,] totalrisk)
        {
            bool anychange = true;
            while (anychange)
            {
                anychange = false;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int min = getMin(totalrisk, riskmap, x, y);
                        if (min < totalrisk[x, y])
                        {
                            totalrisk[x, y] = min;
                            anychange = true;
                        }
                    }
                }
            }
        }

        void print(int[,] data)
        {
            for(int y = 0; y < h; y++)
            {
                for(int x = 0; x < w; x++)
                {
                    Console.Write(String.Format("{0,3}", data[x, y]));
                }
                Console.WriteLine();
            }
        }

        int getMin(int[,]totalrisk,int[,]riskmap,int x,int y)
        {
            if(x==0 && y == 0)
            {
                return 0;
            }

            int min = int.MaxValue;
            /*
            for(int i = x - 1; i < x + 2; i++)
            {
                for(int j = y - 1; j < y + 2; j++)
                {
                    if (i < 0 || i >= w || y < 0 || y >= h || (i==x && j==y))
                        continue;

                }
            }*/
            if (x > 0 && totalrisk[x - 1,y] < min)
            {
                min = totalrisk[x - 1, y];
            }
            if(x < (w - 1) && totalrisk[x + 1, y] < min)
            {
                min = totalrisk[x + 1, y];
            }
            if (y > 0 && totalrisk[x, y - 1] < min)
            {
                min = totalrisk[x, y - 1];
            }
            if (y < (h - 1) && totalrisk[x, y + 1] < min)
            {
                min = totalrisk[x, y + 1];
            }
            min += riskmap[x, y];
            return min;
        }


        void paint(int[,] array)
        {
            int min = 0;
            int max = 0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    max = Math.Max(max,array[x, y]);
                }
            }
            max++;

            ImageMagick.IMagickColor<byte>[] colors = new ImageMagick.IMagickColor<byte>[max];
            int stepsize = 500;
            int steps = (max / stepsize) + 1;
            double sat = 0.05;
            double satstep = 0.75 / steps;
            for(int c = min; c < max; c++)
            {
                double hue = (c % stepsize) / (double)stepsize;
                double saturation = sat + (satstep * (c / stepsize));
                colors[c] = (new ImageMagick.ColorHSL(hue, saturation, 0.5)).ToMagickColor();
            }

            ImageMagick.MagickImage img = new ImageMagick.MagickImage(ImageMagick.MagickColors.Black, w*3, h*3);
            ImageMagick.Drawables draw = new ImageMagick.Drawables();
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    draw.FillColor(colors[array[x, y]]).Rectangle(x * 3, y * 3, x * 3 + 2, y * 3 + 2);
                }
            }
            draw.Draw(img);
            Program.saveImageMagickImage("day15", img);
        }


        private void calcRiskImg(int[,] riskmap, int[,] totalrisk)
        {
            ImageMagick.MagickImageCollection gif = Program.getAnimatedImage();
            ImageMagick.MagickImage img = new ImageMagick.MagickImage(ImageMagick.MagickColors.Black, w, h);
            ImageMagick.Drawables draw = new ImageMagick.Drawables();
            draw.FillColor(ImageMagick.MagickColors.White).Rectangle(0, 0, 1, 1);
            draw.Draw(img);
            Program.addAnimatedImageFrame(gif, img, 50);

            bool anychange = true;
            while (anychange)
            {
                anychange = false;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int min = getMin(totalrisk, riskmap, x, y);
                        if (min < totalrisk[x, y])
                        {
                            totalrisk[x, y] = min;
                            anychange = true;
                        }
                    }
                }

                paint2(totalrisk, gif, anychange);
            }

            Program.saveAnimatedImage("day15", gif);
        }

        void paint2(int[,] array, ImageMagick.MagickImageCollection gif,bool notend)
        {
            int min = 0;
            int max = 0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    max = Math.Max(max, array[x, y]);
                }
            }
            max++;

            ImageMagick.IMagickColor<byte>[] colors = new ImageMagick.IMagickColor<byte>[max];
            int stepsize = 500;
            int steps = (max / stepsize) + 1;
            double sat = 0.05;
            double satstep = 0.75 / steps;
            for (int c = min; c < max; c++)
            {
                double hue = (c % stepsize) / (double)stepsize;
                double saturation = sat + (satstep * (c / stepsize));
                colors[c] = (new ImageMagick.ColorHSL(hue, saturation, 0.5)).ToMagickColor();
            }

            ImageMagick.MagickImage img = new ImageMagick.MagickImage(ImageMagick.MagickColors.Black, w, h);
            ImageMagick.Drawables draw = new ImageMagick.Drawables();
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    draw.FillColor(colors[array[x, y]]).Rectangle(x, y, x+1, y+1);
                }
            }
            draw.Draw(img);

            Program.addAnimatedImageFrame(gif, img, notend ? 10 : 50);
        }
    }
}
