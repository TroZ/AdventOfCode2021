﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace Advent_of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //Day20 prog = new Day20();
            //Day19 prog = new Day19();
            Day18 prog = new Day18();
            //Day17 prog = new Day17();
            //Day16 prog = new Day16();
            //Day15 prog = new Day15();
            //Day14 prog = new Day14();
            //Day13 prog = new Day13();
            //Day12 prog = new Day12();
            //Day11 prog = new Day11();
            //Day10 prog = new Day10();
            //Day9 prog = new Day9();
            //Day8 prog = new Day8();
            //Day7 prog = new Day7();
            //Day6 prog = new Day6();
            //Day5 prog = new Day5();
            //Day4 prog = new Day4();
            //Day3 prog = new Day3();
            //Day2 prog = new Day2();
            //Day1 prog = new Day1();
            prog.Run();


            Console.WriteLine("\n\n\nPress Enter");
            Console.In.ReadLine();


            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            ConsoleColor back = Console.BackgroundColor;
            ConsoleColor fore = Console.ForegroundColor;
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach( var t in types)
            {
                if (t.Namespace.StartsWith("Advent_of_Code_2021") && t.Name.StartsWith("Day") && !t.IsAbstract )
                {
                    ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
                    if (ci != null)
                    {
                        object dayInstance = ci.Invoke(null);

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(t.FullName);
                        Console.BackgroundColor = back;
                        Console.ForegroundColor = fore;
                        Console.WriteLine();

                        t.GetMethod("Run").Invoke(dayInstance, null);
                    }

                }
            }

            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Elapsed time: "+(end-start)+" ms");
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
            Console.WriteLine();
            Console.WriteLine();

        }


        public static string[] readFile(int num)
        {
            /*
            int counter = 0;
            string line;
            Dictionary<int, string> lines = new Dictionary<int, string>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\TroZ\Projects\AdventOfCode\AdventOfCode\AdventOfCode\day"+num+".txt");
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(counter, line);
                System.Console.WriteLine(line);
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);

            string[] ret = new string[lines.Count];
            for(int i = 0; i < ret.Length; i++)
            {
                ret[i] = lines[i];
            }
            /*/
            return System.IO.File.ReadAllLines(@"C:\Users\TroZ\source\repos\Advent of Code 2021\Advent of Code 2021\day" + num + ".txt");
            //*/
        }

        internal static void saveImg(Bitmap pic, int day, int frame = -1)
        {
            String name = "" + day;
            if (frame > -1)
            {
                name += "-" + frame;
            }
            pic.Save(@"C:\Users\TroZ\source\repos\Advent of Code 2021\Advent of Code 2021\day" + name + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }

        internal static ImageMagick.MagickImageCollection getAnimatedImage()
        {
            return new ImageMagick.MagickImageCollection();
        }

        internal static void addAnimatedImageFrame(ImageMagick.MagickImageCollection col, Bitmap img, int delay=100)
        {
            ImageMagick.IMagickImage<byte> image = null;
            //ImageMagick.MagickFactory f = new ImageMagick.MagickFactory();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                image = new ImageMagick.MagickImage(ms);
            }
            col.Add(image);
            col[col.Count - 1].AnimationDelay = delay;
        }

        internal static void addAnimatedImageFrame(ImageMagick.MagickImageCollection col, ImageMagick.IMagickImage<byte> img, int delay = 10)
        {
            col.Add(img);
            col[col.Count - 1].AnimationDelay = delay;
        }

        internal static void saveAnimatedImage(String name, ImageMagick.MagickImageCollection col, int colors = 256)
        {
            ImageMagick.QuantizeSettings settings = new ImageMagick.QuantizeSettings();
            settings.Colors = colors;
            col.Quantize(settings);

            // Optionally optimize the images (images should have the same size).
            col.Optimize();

            //save gif
            col.Write(@"C:\Users\TroZ\source\repos\Advent of Code 2021\Advent of Code 2021\" + name + ".gif");
        }

        internal static void saveImageMagickImage(string name, ImageMagick.MagickImage img)
        {
            img.Write(@"C:\Users\TroZ\source\repos\Advent of Code 2021\Advent of Code 2021\" + name + ".png");
        }
    }

}
