using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day3 : Day
    {
        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = { "00100",
"11110",
"10110",
"10111",
"10101",
"01111",
"00111",
"11100",
"10000",
"11001",
"00010",
"01010" };
            //*/
            
            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();


            int len = data[0].Length;
            int[] bits = new int[len];

            for(int i = 0; i < data.Length; i++)
            {
                char[] chars = data[i].ToCharArray();
                for(int j = 0; j < len; j++)
                {
                    if (chars[j] == '1')
                        bits[j]++;
                }
            }

            string result = "";
            for(int i = 0; i < len; i++)
            {
                if(bits[i] > data.Length / 2)
                {
                    result += '1';
                }
                else
                {
                    result += '0';
                }
            }
            int gamma = Convert.ToInt32(result, 2);
            string er = result.Replace('1', 'a').Replace('0', '1').Replace('a', '0');
            int epsilon = Convert.ToInt32(er, 2);


            Console.WriteLine("Value: " + (gamma * epsilon));

            Console.WriteLine();
            Console.WriteLine();

            bool[] okoxy = new bool[data.Length];
            bool[] okco2 = new bool[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                okoxy[i] = okco2[i] = true;
            }

            //oxy
            for (int i = 0; i < bits.Length; i++)
            {
                int bitson = 0;
                int strcount = count(okoxy);
                if (strcount > 1)
                {
                    
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (okoxy[j] && data[j].ToCharArray()[i] == '1')
                            bitson++;
                    }

                    for (int j = 0; j < data.Length; j++)
                    {
                        if (okoxy[j])
                        {
                            if (bitson < ((strcount+1) / 2) )
                            {
                                if (data[j].ToCharArray()[i] != '0')
                                    okoxy[j] = false;
                            }
                            else
                            {
                                if (data[j].ToCharArray()[i] != '1')
                                    okoxy[j] = false;
                            }
                        }
                    }
                }
            }

            //co2
            for (int i = 0; i < bits.Length; i++)
            {
                int bitson = 0;
                int strcount = count(okco2);
                if (strcount > 1)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (okco2[j] && data[j].ToCharArray()[i] == '1')
                            bitson++;
                    }

                    for (int j = 0; j < data.Length; j++)
                    {

                        if (okco2[j] && count(okco2) > 1)
                        {
                            if (bitson < ((strcount+1) / 2) )
                            {
                                if (data[j].ToCharArray()[i] != '1')
                                    okco2[j] = false;
                            }
                            else
                            {
                                if (data[j].ToCharArray()[i] != '0')
                                    okco2[j] = false;
                            }
                        }
                    }
                }
            }

            int oxy = 0;
            int co2 = 0;
            for(int i = 0; i < okoxy.Length; i++)
            {
                if (okoxy[i])
                {
                    oxy = Convert.ToInt32(data[i], 2);
                }
            }
            for (int i = 0; i < okco2.Length; i++)
            {
                if (okco2[i])
                {
                    co2 = Convert.ToInt32(data[i], 2);
                }
            }

            Console.WriteLine("Value: " + (oxy * co2));
        }

        int count(bool[] arr)
        {
            int count = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                count += arr[i] ? 1 : 0;
            }
            return count;
        }

    }
}
