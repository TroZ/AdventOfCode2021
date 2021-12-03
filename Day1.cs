using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day1 : Day
    {
        int[] getData()
        {
            //*
            string[] lines = Program.readFile(1);
            /*/
            string[] lines = { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" };
            //*/
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

            int prev = data[0];
            //Console.WriteLine("val: " + data[0]);
            int incCount = 0;
            for (int i = 1; i < data.Length; i++)
            {
                //Console.Write("val: " + data[i]);
                if (data[i] > prev)
                {
                    incCount++;
                    //Console.Write("    total: " + incCount+"   increased!");
                }
                else
                {
                    //Console.Write("    total: " + incCount);
                }
                //Console.WriteLine();
                prev = data[i];
            }

            Console.WriteLine(" increased " + incCount + " times");

            Console.WriteLine();
            Console.WriteLine();

            incCount = 0;
            for (int i = 0; i < data.Length - 2; i++)
            {
                int val = data[i] + data[i + 1] + data[i + 2];
                if (i > 0)
                {
                    if (val > prev)
                    {
                        incCount++;
                    }
                }
                prev = val;

            }
            Console.WriteLine("window increased " + incCount + " times");
        }

    }
}
