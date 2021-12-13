using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day13 : Day
    {
        Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/

            string[] lines = {
"6,10",
"0,14",
"9,10",
"0,3",
"10,4",
"4,11",
"6,0",
"6,12",
"4,1",
"0,13",
"10,12",
"3,4",
"3,0",
"8,4",
"1,10",
"2,14",
"8,10",
"9,0",
"",
"fold along y=7",
"fold along x=5"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            int maxx = 2000;
            int maxy = 2000;
            char[,] paper = new char[maxx, maxy];
            for(int i = 0; i < maxx; i++)
            {
                for(int j = 0; j < maxy; j++)
                {
                    paper[i, j] = ' ';
                }
            }

            bool firstfold = true;
            foreach(string line in data)
            {
                if (!line.StartsWith("fold along") && line.Length > 0) 
                {
                    //add dot
                    string[] pos = line.Split(",");
                    int x = int.Parse(pos[0]);
                    int y = int.Parse(pos[1]);
                    paper[x, y] = '#';
                }

                if(line.StartsWith("fold along"))
                {
                    string[] str = line.Split(" ");
                    if (str[2].StartsWith("y"))
                    {
                        //flip up
                        int pos = int.Parse(str[2].Split("=")[1]);
                        for(int y = pos + 1; y < maxy; y++)
                        {
                            for(int x = 0; x < maxx; x++)
                            {
                                int newy = pos - (y - pos);
                                if (newy >= 0)
                                {
                                    if(paper[x, y] != ' ')
                                        paper[x, newy] = '#';
                                }
                            }
                        }
                        maxy = pos;
                    }
                    else
                    {
                        //flip to side
                        int pos = int.Parse(str[2].Split("=")[1]);
                        for(int x = pos + 1;x< maxx; x++)
                        {
                            for(int y = 0; y < maxy; y++)
                            {
                                int newx = pos - (x - pos);
                                if (newx >= 0)
                                {
                                    if(paper[x, y] != ' ')
                                        paper[newx, y] = '#';
                                }
                            }
                        }
                        maxx = pos;
                    }

                    if (firstfold)
                    {
                        //count number of dots
                        firstfold = false;
                        int count = 0;
                        for(int x = 0; x < maxx; x++)
                        {
                            for(int y = 0; y < maxy; y++)
                            {
                                if (paper[x, y] != ' ')
                                    count++;
                            }
                        }
                        Console.WriteLine("Count after first fold " + count);
                    }
                }
            }

            //print
            Console.WriteLine("width: " + maxx + "  height: " + maxy);
            for (int y = 0; y < maxy; y++)
            {
                for (int x = 0; x < maxx; x++)
                {
                    Console.Write(paper[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
