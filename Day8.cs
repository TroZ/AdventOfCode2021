using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day8 : Day
    {

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |fdgacbe cefdb cefbgd gcbe",
"edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |fcgedb cgb dgebacf gc",
"fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |cg cg fdcagb cbg",
"fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |efabcd cedba gadfec cb",
"aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |gecf egdcabf bgf bfgea",
"fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |gebdcfa ecba ca fadegcb",
"dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |cefg dcbef fcge gbcadfe",
"bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |ed bcgafe cdgba cbgef",
"egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |gbdfcae bgc cg cgb",
"gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |fgae cfgab fg bagce"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            //part 1 - count unique
            int count = 0;
            for(int i = 0; i < data.Length; i++)
            {
                string[] output = data[i].Split('|')[1].Split(" ");
                foreach(string s in output)
                {
                    if (s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7)
                        count++;
                }
            }

            Console.WriteLine("1,4,7,8 appear " + count + " times.");


            //part 2 - true decode
            int total = 0;
            foreach (String line in data)
            {
                string[] parts = line.Split('|');

                string[] examples = parts[0].Trim().Split(' ');
                string[] numbers = new string[10];

                //1, 4, 7, 8
                for (int i = 0; i < examples.Length; i++)
                {
                    examples[i] = SortString(examples[i]);
                    if (examples[i].Length == 2)
                    {
                        numbers[1] = new string(examples[i]);
                        examples[i] = "";
                    }
                    else if (examples[i].Length == 3)
                    {
                        numbers[7] = new string(examples[i]);
                        examples[i] = "";
                    }
                    else if (examples[i].Length == 4)
                    {
                        numbers[4] = new string(examples[i]);
                        examples[i] = "";
                    }
                    else if (examples[i].Length == 7)
                    {
                        numbers[8] = new string(examples[i]);
                        examples[i] = "";
                    }
                }


                for (int i = 0; i < examples.Length; i++)
                {
                    if (examples[i].Length == 5)
                    {
                        //2 or 3 or 5
                        if (examples[i].Contains(numbers[1].ToCharArray()[0]) &&
                            examples[i].Contains(numbers[1].ToCharArray()[1]))
                        {
                            //if contains the characters in 1, then it's 3
                            numbers[3] = new string(examples[i]);
                            examples[i] = "";
                        }
                        else
                        {
                            string four = new string(numbers[4]);
                            foreach (char c in examples[i])
                            {
                                four = four.Replace(c, ' ');
                            }
                            four = four.Replace(" ", "");

                            if (four.Length == 1)
                            {
                                //if we remove the elements from 4 and are left with 1, then it was 5
                                numbers[5] = new string(examples[i]);
                                examples[i] = "";
                            }
                            else
                            {
                                //otherwise it was 2
                                numbers[2] = new string(examples[i]);
                                examples[i] = "";
                            }
                        }
                    }

                }

                for (int i = 0; i < examples.Length; i++)
                {
                    if (examples[i].Length == 6)
                    {
                        //0, 6, 9
                        string four = new string(numbers[4]);
                        string seven = new string(numbers[7]);

                        foreach (char c in examples[i])
                        {
                            four = four.Replace(c, ' ');
                            seven = seven.Replace(c, ' ');
                        }
                        four = four.Replace(" ", "");
                        seven = seven.Replace(" ", "");

                        if (four.Length == 0)
                        {
                            //if we find that 4 will fit in the pattern, then it was 9
                            numbers[9] = new string(examples[i]);
                            examples[i] = "";
                        }
                        else if (seven.Length == 0)
                        {
                            //if seven fits in the pattern, it was 0
                            numbers[0] = new string(examples[i]);
                            examples[i] = "";
                        }
                        else
                        {
                            //otherwise, it's 6
                            numbers[6] = new string(examples[i]);
                            examples[i] = "";
                        }

                    }

                }


                //ok, decode digits now
                string[] digits = parts[1].Trim().Split(' ');
                int val = 0;
                for(int i = 0; i < digits.Length; i++)
                {
                    val *= 10;
                    digits[i] = SortString(digits[i]);
                    for(int j = 0; j < 10; j++)
                    {
                        if (numbers[j].Equals(digits[i]))
                        {
                            val += j;
                        }
                    }
                }

                Console.WriteLine(" display: " + val);
                total += val;
            }
            Console.WriteLine("TOTAL: " + total);
        }

        static string SortString(string input)
        {
            char[] characters = input.ToCharArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }
}
