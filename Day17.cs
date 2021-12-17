using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day17 : Day
    {

        int xmin, xmax, ymin, ymax;

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/

            string[] lines = { "target area: x=20..30, y=-10..-5" };

            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();


            
            string str = data[0].Split(":")[1];
            string[] xy = str.Split(",");
            string xx = xy[0].Split("=")[1];
            string yy = xy[1].Split("=")[1];
            string[] xxx = xx.Split("..");
            string[] yyy = yy.Split("..");
            xmin = int.Parse(xxx[0]);
            xmax = int.Parse(xxx[1]);
            ymin = int.Parse(yyy[0]);
            ymax = int.Parse(yyy[1]);


            List<Point> points = new List<Point>();
            int maxheight = int.MinValue;
            int bestx = 0;
            int besty = 0;
            for(int x = 0; x < 300; x++)
            {
                for(int y = -120; y < 1000; y++)
                {
                    int height = 0;
                    bool hit = testPath(x, y, ref height);
                    if (hit)
                    {
                        points.Add(new Point(x, y));
                        if (height > maxheight)
                        {
                            maxheight = height;
                            bestx = x;
                            besty = y;
                        }
                    }
                }
            }

            Console.WriteLine("Highest y = " + maxheight+"  for x = "+bestx+"  y = "+besty);

            Console.WriteLine();
            Console.WriteLine("Count of possible: " + points.Count);
            for(int i = 0; i < points.Count; i++)
            {
                Console.Write(String.Format("{0,4},{1,4} ", points[i].x,points[i].y));
                if (i % 8 == 7)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("\t");
                }
            }
        }

        bool testPath(int xv, int yv, ref int height)
        {
            int x = 0;
            int y = 0;
            int maxhigh = 0;

            while( x < xmax && y > ymin)
            {
                x = x + xv;
                y = y + yv;
                if(xv > 0)
                {
                    xv--;
                }
                else if(xv < 0)
                {
                    xv++;
                }
                yv--;

                if (y > maxhigh)
                    maxhigh = y;
                if(x>=xmin && x<=xmax && y>=ymin && y <= ymax)
                {
                    //target hit
                    height = maxhigh;
                    return true;
                }
            }

            //miss
            height = maxhigh;
            return false;
        }

        public class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

    }
}
