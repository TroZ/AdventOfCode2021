using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day7 : Day
    {

        int[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"16,1,2,0,4,2,7,1,2,14"
            };
            //*/
            lines = lines[0].Split(",");
            int[] data = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                data[i] = int.Parse(lines[i]);
            }
            return data;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            int[] data = getData();

            int min = data[0];
            int max = data[0];

            for(int i = 0; i < data.Length; i++)
            {
                min = Math.Min(min, data[i]);
                max = Math.Max(max, data[i]);
            }

            int bestcost = int.MaxValue;
            int bestpos = 0;

            for(int i = min; i <= max; i++)
            {
                int cost = 0;
                for(int j = 0; j < data.Length; j++)
                {
                    cost += Math.Abs(i - data[j]);
                }

                if(cost < bestcost)
                {
                    bestcost = cost;
                    bestpos = i;
                }
            }

            Console.WriteLine("Best Cost at " + bestpos + " with a cost of " + bestcost);

            //part2
            bestcost = int.MaxValue;
            bestpos = 0;

            for (int i = min; i <= max; i++)
            {
                int cost = 0;
                int thiscost = 0;
                for (int j = 0; j < data.Length; j++)
                {
                    thiscost = Math.Abs(i - data[j]);
                    for(int k = 0; k < thiscost; k++)
                    {
                        cost += (k+1);
                    }
                }

                if (cost < bestcost)
                {
                    bestcost = cost;
                    bestpos = i;
                }
            }

            Console.WriteLine("Best Cost at " + bestpos + " with a cost of " + bestcost);
        }
    }
}
