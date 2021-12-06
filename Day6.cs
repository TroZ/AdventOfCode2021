using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day6 : Day
    {
        static int firstTime = 8;
        static int resetTime = 6;

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"3,4,3,1,2"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();
            string[] data2 = data[0].Split(",");

            //int[] times = new int[data2.Length];
            List<LFish> fish = new List<LFish>();
            for(int i = 0; i < data2.Length; i++)
            {
                int time = int.Parse(data2[i]);
                fish.Add(new LFish(time));
            }


            List<LFish> toadd = new List<LFish>();
            for (int day = 0; day < 80; day++) {
                foreach (LFish f in fish)
                {
                    bool n = f.day();
                    if (n)
                    {
                        toadd.Add(new LFish(-1));
                    }
                }
                fish.AddRange(toadd);
                toadd.Clear();

                /*
                Console.Write(" Day " + (day + 1) + " : ");
                foreach (LFish f in fish)
                {
                    Console.Write(f.days);
                    Console.Write(",");
                }
                Console.WriteLine();
                */
            }

            Console.WriteLine("Count at 80: " + fish.Count);


            //part 2

            Int64[] groupcount = new Int64[9];
            for (int i = 0; i < data2.Length; i++)
            {
                int time = int.Parse(data2[i]);
                groupcount[time]++;
            }

            for(int i = 0; i < 256; i++)
            {
                Int64 newfish = groupcount[0];
                for (int j = 0; j < groupcount.Length-1; j++)
                {
                    groupcount[j] = groupcount[j + 1];
                }
                groupcount[6] += newfish;
                groupcount[8] = newfish;

                /*
                Console.Write(" Day " + (i + 1) + " : ");
                for (int j = 0; j < groupcount.Length; j++)
                {
                    Console.Write(j);
                    Console.Write(":");
                    Console.Write(groupcount[j]);
                    Console.Write("  ");
                }
                Console.WriteLine();
                */
            }

            Int64 totalCount = 0;
            for (int j = 0; j < groupcount.Length ; j++)
            {
                totalCount += groupcount[j];
            }

            Console.WriteLine("Count at 256: " + totalCount);

        }



        public class LFish
        {
            public int days;
            public LFish(int day)
            {
                if (day < 0)
                {
                    days = firstTime;
                }
                else
                {
                    days = day;
                }
            }

            public bool day()
            {
                if (days == 0)
                {
                    days = resetTime;
                    return true;
                }
                else 
                {
                    days--;
                }
                return false;
            }
        }

        
    }

}
