using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day12 : Day
    {
        Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            /*
            string[] lines = {
"start-A",
"start-b",
"A-c",
"A-b",
"b-d",
"A-end",
"b-end"
            };
            /*/
            /*
            string[] lines = { 
"dc-end",
"HN-start",
"start-kj",
"dc-start",
"dc-HN",
"LN-dc",
"HN-end",
"kj-sa",
"kj-HN",
"kj-dc"
                };
            /*/
            /*
            string[] lines = {
"fs-end",
"he-DX",
"fs-he",
"start-DX",
"pj-DX",
"end-zg",
"zg-sl",
"zg-pj",
"pj-he",
"RW-he",
"fs-DX",
"pj-RW",
"zg-RW",
"start-pj",
"he-WI",
"zg-he",
"pj-fs",
"start-RW"
                };
            //*/
            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();


            


            foreach(string str in data)
            {
                string[] caves = str.Split("-");
                //add a->b
                if (connections.ContainsKey(caves[0]))
                {
                    connections[caves[0]].Add(caves[1]);
                }
                else
                {
                    List<String> dests = new List<string>();
                    dests.Add(caves[1]);
                    connections.Add(caves[0], dests);
                }
                //add b->a
                if (connections.ContainsKey(caves[1]))
                {
                    connections[caves[1]].Add(caves[0]);
                }
                else
                {
                    List<String> dests = new List<string>();
                    dests.Add(caves[0]);
                    connections.Add(caves[1], dests);
                }
            }


            int paths = findPaths("start");

            Console.WriteLine("Total Paths no revisit small cave: " + paths);



            int paths2 = findPaths2("start",false);

            Console.WriteLine("Total Paths allow small cave revisit: " + paths2);

        }


        int findPaths(string path)
        {
            int paths = 0;

            string[] pathParts = path.Split(",");

            List<string> dests = connections[pathParts[pathParts.Length - 1]];

            foreach(string str in dests)
            {
                if (str.Equals("end"))
                {
                    paths++;
                    Console.WriteLine(path + ",end");
                }else if (str.ToLowerInvariant().Equals(str))
                {
                    //small cave, can only visit once, check we haven't visited before
                    bool visit = false;
                    foreach(string visited in pathParts)
                    {
                        if (str.Equals(visited))
                        {
                            visit = true;
                        }
                    }
                    if (!visit)
                    {
                        paths += findPaths(path + "," + str);
                    }
                }
                else
                {
                    //big cave
                    paths += findPaths(path + "," + str);
                }
            }

            return paths;
        }


        int findPaths2(string path,bool smallRevisitHappened)
        {
            int paths = 0;

            string[] pathParts = path.Split(",");

            List<string> dests = connections[pathParts[pathParts.Length - 1]];

            foreach (string str in dests)
            {
                if (str.Equals("end"))
                {
                    paths++;
                    Console.WriteLine(path + ",end");
                }
                else if (str.ToLowerInvariant().Equals(str))
                {
                    //small cave, can only visit once, check we haven't visited before
                    bool visit = false;
                    foreach (string visited in pathParts)
                    {
                        if (str.Equals(visited))
                        {
                            visit = true;
                        }
                    }
                    if (!visit)
                    {
                        paths += findPaths2(path + "," + str, smallRevisitHappened);
                    }
                    else if(!str.Equals("start") && !smallRevisitHappened)
                    {
                        paths += findPaths2(path + "," + str, true);
                    }

                }
                else
                {
                    //big cave
                    paths += findPaths2(path + "," + str, smallRevisitHappened);
                }
            }

            return paths;
        }

    }
}
