using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day5 : Day
    {
        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"0,9 -> 5,9",
"8,0 -> 0,8",
"9,4 -> 3,4",
"2,2 -> 2,1",
"7,0 -> 7,4",
"6,4 -> 2,0",
"0,9 -> 2,9",
"3,4 -> 1,4",
"0,0 -> 8,8",
"5,5 -> 8,2" 
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            int[,] area = new int[1000, 1000];

            foreach(string line in data)
            {
                string[] points = line.Split("->");
                string[] one = points[0].Trim().Split(',');
                string[] two = points[1].Trim().Split(',');

                int x1 = int.Parse(one[0]);
                int y1 = int.Parse(one[1]);
                int x2 = int.Parse(two[0]);
                int y2 = int.Parse(two[1]);

                if (x1 == x2 || y1 == y2)
                {
                    //plot
                    if(x1 == x2)
                    {
                        int yy1 = Math.Min(y1, y2);
                        int yy2 = Math.Max(y1, y2);
                        for(int y = yy1; y <= yy2; y++) {
                            area[x1, y]++;
                        }
                    }
                    else
                    {
                        int xx1 = Math.Min(x1, x2);
                        int xx2 = Math.Max(x1, x2);
                        for (int x = xx1; x <= xx2; x++)
                        {
                            area[x, y1]++;
                        }
                    }
                }
            }

            //count
            int count = 0;
            for(int x = 0; x < 1000; x++)
            {
                for(int y = 0; y < 1000; y++)
                {
                    if(area[x,y] > 1)
                    {
                        count++;
                    }
                }
            }


            Console.WriteLine("Dangerous areas: " + count);


            //part2
            area = new int[1000, 1000];

            foreach (string line in data)
            {
                string[] points = line.Split("->");
                string[] one = points[0].Trim().Split(',');
                string[] two = points[1].Trim().Split(',');

                int x1 = int.Parse(one[0]);
                int y1 = int.Parse(one[1]);
                int x2 = int.Parse(two[0]);
                int y2 = int.Parse(two[1]);

                if (x1 == x2 || y1 == y2)
                {
                    //plot
                    if (x1 == x2)
                    {
                        int yy1 = Math.Min(y1, y2);
                        int yy2 = Math.Max(y1, y2);
                        for (int y = yy1; y <= yy2; y++)
                        {
                            area[x1, y]++;
                        }
                    }
                    else 
                    {
                        int xx1 = Math.Min(x1, x2);
                        int xx2 = Math.Max(x1, x2);
                        for (int x = xx1; x <= xx2; x++)
                        {
                            area[x, y1]++;
                        }
                    }

                }
                else
                {
                    int yy1 = Math.Min(y1, y2);
                    int yy2 = Math.Max(y1, y2);
                    int xstep = 1;
                    int ystep = 1;
                    if (x2 < x1) xstep = -1;
                    if (y2 < y1) ystep = -1;
                    for (int i = 0; i <= (yy2 - yy1); i++)
                    {
                        area[x1 + (i*xstep), y1 + (i*ystep)]++;
                    }

                }
                //print(area);
            }

            


            //count
            count = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (area[x, y] > 1)
                    {
                        count++;
                    }
                }
            }


            Console.WriteLine("Dangerous areas: " + count);
        }

        public void print(int[,] area)
        {
            Console.WriteLine();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write(area[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
