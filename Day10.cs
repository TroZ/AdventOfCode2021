using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day10 : Day
    {

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"[({(<(())[]>[[{[]{<()<>>",
"[(()[<>])]({[<{<<[]>>(",
"{([(<{}[<>[]}>{[]{[(<()>",
"(((({<>}<{<{<>}{[]{[]{}",
"[[<[([]))<([[{}[[()]]]",
"[{[{({}]{}}([{[{{{}}([]",
"{<[[]]>}<{[{[{[]{()[[[]",
"[<(<(<(<{}))><([]([]()",
"<{([([[(<>()){}]>(<<{{",
"<{([{{}}[<[[[<>{}]]]>[]]"
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            int totalerr = 0;
            List<Int64> fixScores = new List<Int64>();
            foreach(string str in data)
            {
                
                parseResult r = getScore(str);
                while(r.value==0 && r.remain.Length > 0)
                {
                    r = getScore(r.remain);
                }
                totalerr += r.value;

                Int64 val = 0;
                if (r.finish.Length > 0)
                {
                    
                    foreach (char c in r.finish)
                    {
                        val *= 5;
                        val += getFinishPoints(c);
                    }
                    fixScores.Add(val);
                }

                Console.WriteLine("" + str + " : " + r.value +"  -  "+r.finish+" : "+val);
            }

            Console.WriteLine("Total error score: " + totalerr);

            Int64[] fixScoreArray = fixScores.ToArray();
            Array.Sort(fixScoreArray);
            Console.WriteLine("Fix score: " + fixScoreArray[(fixScoreArray.Length-1)/2]);
        }

        /* takes string and attempts to find match for first character */
        public parseResult getScore(string str)
        {
            parseResult r = new parseResult();
            char c = str.ToCharArray()[0];
            string remain = str.Substring(1);

            bool done = false;
            while (!done && remain.Length > 0)
            {
                char next = remain.ToCharArray()[0];
                if ("(<[{".Contains(next))
                {
                    parseResult res = getScore(remain);
                    if (res.value > 0)
                    {
                        done = true;
                        r.value = res.value;
                    }
                    remain = res.remain;
                    r.finish = res.finish;
                }
                else
                {
                    remain = remain.Substring(1);
                    done = true;
                    switch (c)
                    {
                        case '(':
                            if (next != ')')
                            {
                                r.value = getPoints(next);
                            }
                            break;
                        case '[':
                            if (next != ']')
                            {
                                r.value = getPoints(next);
                            }
                            break;
                        case '{':
                            if (next != '}')
                            {
                                r.value = getPoints(next);
                            }
                            break;
                        case '<':
                            if (next != '>')
                            {
                                r.value = getPoints(next);
                            }
                            break;
                    }
                }
            }

            r.remain = remain;

            if (!done)
            {
                r.finish += getFinish(c);
            }

            return r;
        }

        int getPoints(char c)
        {
            switch (c)
            {
                case ')': return 3;
                case ']': return 57;
                case '}': return 1197;
                case '>': return 25137;
            }
            return 0;
        }

        char getFinish(char c)
        {
            switch (c)
            {
                case '(': return ')';
                case '[': return ']';
                case '{': return '}';
                case '<': return '>';
            }
            return ' ';
        }

        int getFinishPoints(char c)
        {
            switch (c)
            {
                case ')': return 1;
                case ']': return 2;
                case '}': return 3;
                case '>': return 4;
            }
            return 0;
        }


        public class parseResult
        {
            public string remain = "";
            public int value = 0;
            public string finish = "";
        }
    }
}
