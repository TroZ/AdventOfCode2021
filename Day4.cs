using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day4 : Day
    {
        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/
            string[] lines = {
"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
"",
"22 13 17 11  0",
"8  2 23  4 24",
"21  9 14 16  7",
"6 10  3 18  5",
"1 12 20 15 19",
"",
" 3 15  0  2 22",
" 9 18 13 17  5",
"19  8  7 25 23",
"20 11 10 24  4",
"14 21 16 12  6",
"",
"14 21 17 24  4",
"10 16 15  9 19",
"18  8 23 26 20",
"22 11 13  6  5",
" 2  0 12  3  7" 
            };
            //*/

            return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            string[] numbers = data[0].Split(',');
            int[] nums = new int[numbers.Length];
            for(int i = 0; i < numbers.Length; i++)
            {
                nums[i] = int.Parse(numbers[i]);
            }

            List<Bingo> boards = new List<Bingo>();

            for(int i=2; i < data.Length; i+=6)
            {
                Bingo b = new Bingo(data, i);
                boards.Add(b);
            }

            Bingo winner = null;
            int lastcall = 0;
            int firstLastCall = 0;
            List<Bingo> toRemove = new List<Bingo>();
            for (int i = 0;boards.Count > 0 && i < nums.Length; i++)
            {
                lastcall = nums[i];
                
                toRemove.Clear();
                foreach ( Bingo b in boards)
                {
                    b.call(lastcall);
                    if (b.check())
                    {
                        if (winner == null)
                        {
                            winner = b;
                            firstLastCall = lastcall;
                        }
                        toRemove.Add(b);
                    }
                }
                foreach(Bingo b in toRemove)
                {
                    boards.Remove(b);
                }
                
            }

            Console.WriteLine("First Score: " + (winner.uncallSum() * firstLastCall));

            Console.WriteLine();
            Console.WriteLine("Last Score: " + (toRemove[0].uncallSum() * lastcall));
        }

        public class Bingo
        {
            int[,] board = new int[5,5];
            bool[,] called = new bool[5, 5];

            public Bingo(string[] data, int linenum)
            {
                for(int i = 0; i < 5; i++)
                {
                    string[] line = data[linenum + i].Trim().Replace("  "," ").Split(' ');
                    for (int j = 0; j < 5; j++)
                    {
                        board[i, j] = int.Parse(line[j]);
                    }
                }
            }

            public void call(int num)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (board[i, j] == num)
                        {
                            called[i, j] = true;
                        }
                    }
                }
            }

            public bool check()
            {
                bool result = true;
                //across
                for (int i = 0; i < 5; i++)
                {
                    result = true;
                    for (int j = 0; result && j < 5; j++)
                    {
                        result = result && called[i, j];
                    }
                    if (result)
                        return true;
                }
                //down
                for (int j = 0; j < 5; j++)
                {
                    result = true;
                    for (int i = 0; result && i < 5; i++)
                    {
                        result = result && called[i, j];
                    }
                    if (result)
                        return true;
                }
                return false;
            }

            public int uncallSum()
            {
                int sum = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (!called[i, j])
                        {
                            sum += board[i, j];
                        }
                    }
                }
                return sum;
            }
        }

    }
}
