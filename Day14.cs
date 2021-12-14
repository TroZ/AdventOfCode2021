using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day14 : Day
    {
        Dictionary<string, string> mapping = new Dictionary<string, string>();

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/

            string[] lines = {
"NNCB",
"",
"CH -> B",
"HH -> N",
"CB -> H",
"NH -> C",
"HB -> C",
"HC -> B",
"HN -> C",
"NN -> C",
"BH -> H",
"NC -> B",
"NB -> B",
"BN -> B",
"BB -> N",
"BC -> B",
"CC -> N",
"CN -> C"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            string template = data[0];
            
            for (int i = 2; i < data.Length; i++)
            {
                string[] str = data[i].Split(" -> ");
                mapping.Add(str[0], str[1]);
            }


            Console.WriteLine("Template: " + template);

            //polymerize 10 times
            string polymer = template;
            for (int i = 0; i < 10; i++)
            {
                //do replacement
                StringBuilder sb = new StringBuilder();
                char[] chars = polymer.ToCharArray();
                sb.Append(chars[0]);
                for (int pos = 1; pos < polymer.Length; pos++)
                {
                    string pair = polymer.Substring(pos - 1, 2);
                    sb.Append(mapping[pair]);
                    sb.Append(chars[pos]);
                }

                polymer = sb.ToString();
                Console.WriteLine("After " + (i + 1) + ": " + polymer);
            }


            //count chars
            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach (char c in polymer)
            {
                if (counts.ContainsKey(c))
                {
                    counts[c]++;
                }
                else
                {
                    counts[c] = 1;
                }
            }
            char min = ' ', max = ' ';
            int mincount = int.MaxValue;
            int maxcount = 0;
            foreach (char key in counts.Keys)
            {
                int val = counts[key];
                if (val < mincount)
                {
                    mincount = val;
                    min = key;
                }
                if (val > maxcount)
                {
                    maxcount = val;
                    max = key;
                }
            }

            Console.WriteLine("Value: " + (maxcount - mincount));
            Console.WriteLine(" MAX: " + max + " - " + maxcount);
            Console.WriteLine(" MIN: " + min + " - " + mincount);
            Console.WriteLine();
            Console.WriteLine();



            //part 2;
            Dictionary<string, long> pairs = new Dictionary<string, long>();

            //initialize pairs
            for (int pos = 1; pos < template.Length; pos++)
            {
                string pair = template.Substring(pos - 1, 2);
                if (pairs.ContainsKey(pair))
                {
                    pairs[pair]++;
                }
                else
                {
                    pairs[pair] = 1;
                }
            }

            //polymerize 40 times
            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> newpairs = new Dictionary<string, long>();

                foreach (string key in pairs.Keys)
                {
                    long count = pairs[key];

                    string c = mapping[key];

                    string newpair1 = key.Substring(0, 1) + c;
                    string newpair2 = c + key.Substring(1, 1);

                    if (newpairs.ContainsKey(newpair1))
                    {
                        newpairs[newpair1] += count; ;
                    }
                    else
                    {
                        newpairs[newpair1] = count;
                    }
                    if (newpairs.ContainsKey(newpair2))
                    {
                        newpairs[newpair2] += count; ;
                    }
                    else
                    {
                        newpairs[newpair2] = count;
                    }

                }

                pairs = newpairs;
            }


            //count
            Dictionary<char, long> counts2 = new Dictionary<char, long>();
            foreach (string key in pairs.Keys)
            {
                long val = pairs[key];
                foreach (char c in key)
                {
                    if (counts2.ContainsKey(c))
                    {
                        counts2[c] += val;
                    }
                    else
                    {
                        counts2[c] = val;
                    }
                }
            }
            //the above double counts each letter except the first and last, which is one less
            counts2[template.ToCharArray()[0]]++;
            counts2[template.ToCharArray()[template.Length-1]]++;
            //now everything is double counted

            long mincountl = long.MaxValue;
            long maxcountl = 0;
            foreach (char key in counts2.Keys)
            {
                long val = (counts2[key])/2;
                if (val < mincountl)
                {
                    mincountl = val;
                    min = key;
                }
                if (val > maxcountl)
                {
                    maxcountl = val;
                    max = key;
                }
            }

            Console.WriteLine("Value: " + (maxcountl - mincountl));
            Console.WriteLine(" MAX: " + max + " - " + maxcountl);
            Console.WriteLine(" MIN: " + min + " - " + mincountl);
        }
    }
}
