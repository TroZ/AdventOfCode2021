using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day2 : Day
    {
        string[] getData()
        {
            //*
            string[] lines = Program.readFile(2);
            /*/
            string[] lines = { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            long x = 0, y = 0;

            for(int i = 0; i < data.Length; i++)
            {
                string[] move = data[i].Split(" ");
                switch (move[0].ToLower().ToCharArray()[0])
                {
                    case 'f':
                        x += int.Parse(move[1]);
                        break;
                    case 'u':
                        y -= int.Parse(move[1]);
                        if (y < 0) Console.WriteLine("issue: y<0");
                        break;
                    case 'd':
                        y += int.Parse(move[1]);
                        break;
                    default:
                        Console.WriteLine("unknown: "+data[i]);
                        break;
                }

            }

            Console.WriteLine("val: " + (x * y) + "   x: " + x + "  y: " + y);



            //part2
            x = 0;
            y = 0;
            int aim = 0;

            for (int i = 0; i < data.Length; i++)
            {
                string[] move = data[i].Split(" ");
                switch (move[0].ToLower().ToCharArray()[0])
                {
                    case 'f':
                        int dist = int.Parse(move[1]);
                        x += dist;
                        y += aim * dist;
                        break;
                    case 'u':
                        aim -= int.Parse(move[1]);
                        break;
                    case 'd':
                        aim += int.Parse(move[1]);
                        break;
                    default:
                        Console.WriteLine("unknown: " + data[i]);
                        break;
                }

            }

            Console.WriteLine("val: " + (x * y) + "   x: " + x + "  y: " + y);

        }
    }
}
