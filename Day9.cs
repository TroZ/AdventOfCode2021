using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day9 : Day
    {

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"2199943210",
"3987894921",
"9856789892",
"8767896789",
"9899965678"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            int totalRisk = 0;
            List<Point> riskpoints = new List<Point>();

            for(int y = 0; y < data.Length; y++)
            {
                for(int x=0; x< data[0].Length; x++)
                {
                    int height = getData(data, x, y);
                    if(height < getData(data,x-1,y) && height < getData(data, x + 1, y) &&
                        height < getData(data, x, y-1) &&height < getData(data, x, y + 1))
                    {
                        totalRisk += 1 + height;
                        riskpoints.Add(new Point(x, y, height));
                    }
                }
            }

            Console.WriteLine("Total Risk: " + totalRisk);


            //part 2
            int[] basinSizes = new int[riskpoints.Count];
            int bcount = 0;
            HashSet<Point> allBasin = new HashSet<Point>(new PointEqualityComparer());
            foreach (Point p in riskpoints)
            {
                HashSet<Point> basinPoints = new HashSet<Point>(new PointEqualityComparer());
                Stack<Point> checkPoints = new Stack<Point>();
                basinPoints.Add(p);
                checkPoints.Push(p);

                while(checkPoints.Count > 0)
                {
                    Point c = checkPoints.Pop();

                    if (c.height == 8)
                        continue;
                    /*
                     * Misenterpertation of 'evenly flow' to mean that the basins only connect to one number higher than the current number
                    int hh = getData(data, c.x - 1, c.y);
                    if (hh == c.height + 1)
                    {
                        Point np = new Point(c.x - 1, c.y, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }

                    hh = getData(data, c.x + 1, c.y);
                    if (hh == c.height + 1)
                    {
                        Point np = new Point(c.x + 1, c.y, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }

                    hh = getData(data, c.x, c.y - 1);
                    if (hh == c.height + 1)
                    {
                        Point np = new Point(c.x , c.y - 1, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }

                    hh = getData(data, c.x , c.y + 1);
                    if (hh == c.height + 1)
                    {
                        Point np = new Point(c.x , c.y + 1, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }
                    */
                    int hh = getData(data, c.x - 1, c.y);
                    if (hh != 9)
                    {
                        Point np = new Point(c.x - 1, c.y, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }

                    hh = getData(data, c.x + 1, c.y);
                    if (hh != 9)
                    {
                        Point np = new Point(c.x + 1, c.y, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }

                    hh = getData(data, c.x, c.y - 1);
                    if (hh != 9)
                    {
                        Point np = new Point(c.x, c.y - 1, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }

                    hh = getData(data, c.x, c.y + 1);
                    if (hh != 9)
                    {
                        Point np = new Point(c.x, c.y + 1, hh);
                        if (basinPoints.Add(np))
                        {
                            //if not already part of the basin
                            checkPoints.Push(np);
                        }
                    }
                }

                //found full basin
                basinSizes[bcount] = basinPoints.Count;
                bcount++;

                allBasin.UnionWith(basinPoints);
            }

            Console.WriteLine();
            ConsoleColor def = Console.ForegroundColor;
            ConsoleColor backdef = Console.BackgroundColor;
            ConsoleColor basinBack = ConsoleColor.DarkBlue;//ConsoleColor.DarkGray;
            ConsoleColor[] colors = new ConsoleColor[ 10];
            /*
            colors[0] = ConsoleColor.DarkBlue;
            colors[1] = ConsoleColor.Blue;
            colors[2] = ConsoleColor.DarkCyan;
            colors[3] = ConsoleColor.Cyan;
            colors[4] = ConsoleColor.DarkGreen;
            colors[5] = ConsoleColor.Green;
            colors[6] = ConsoleColor.DarkYellow;
            colors[7] = ConsoleColor.Yellow;
            colors[8] = ConsoleColor.DarkRed;
            colors[9] = ConsoleColor.Red;
            */
            colors[0] = ConsoleColor.Blue;
            colors[1] = ConsoleColor.DarkCyan;
            colors[2] = ConsoleColor.Cyan;
            colors[3] = ConsoleColor.DarkGreen;
            colors[4] = ConsoleColor.Green;
            colors[5] = ConsoleColor.DarkYellow;
            colors[6] = ConsoleColor.Yellow;
            colors[7] = ConsoleColor.DarkRed;
            colors[8] = ConsoleColor.Red;
            colors[9] = ConsoleColor.Magenta;

            for (int y = 0; y < data.Length; y++)
            {
                Console.Write(y);
                Console.Write('\t');
                for (int x = 0; x < data[0].Length; x++)
                {
                    int h = getData(data, x, y);
                    Console.ForegroundColor = colors[h];
                    if (allBasin.Contains(new Point(x, y, 0)))
                    {
                         Console.BackgroundColor = basinBack;
                    }
                    else
                    {
                        Console.BackgroundColor = backdef;
                    }
                    Console.Write(h);

                }
                Console.ForegroundColor = def;
                Console.BackgroundColor = backdef;
                Console.WriteLine();

            }


            for(int i = 0; i < basinSizes.Length; i++)
            {
                Point bp = riskpoints[i];
                Console.WriteLine("Basin "+i+" has size "+ basinSizes[i]+" and starts at x: "+bp.x+"  y: "+bp.y);
            }

            Array.Sort(basinSizes);

            int result = basinSizes[basinSizes.Length - 1] * basinSizes[basinSizes.Length - 2] * basinSizes[basinSizes.Length - 3];

            Console.WriteLine("Result: " + result);
        }

        public int getData(string[] data, int x, int y)
        {
            if(y<0 || y>= data.Length || x<0 || x >= data[0].Length)
            {
                return 9;
            }
            return int.Parse("" + data[y].ToCharArray()[x]);
        }

        public class Point
        {
            public int x;
            public int y;
            public int height;

            public Point(int xx, int yy, int h)
            {
                x = xx;
                y = yy;
                height = h;
            }

            public bool Equals(Point other)
            {
                if(other.x == x && other.y == y)
                {
                    return true;
                }
                return false;
            }
        }

        class PointEqualityComparer : IEqualityComparer<Point>
        {
            public bool Equals(Point p1, Point p2) {
                if (p1 == null && p2 == null)
                    return true;
                else if (p1 == null || p2 == null)
                    return false;
                else if (p1.x == p2.x && p1.y == p2.y)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Point p)
            {
                int hCode = p.x * 1000 + p.y;
                return hCode.GetHashCode();
            }
        }
    }
}
