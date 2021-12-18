using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day18 : Day
    {

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/

            //string[] lines = { "[[[[[9,8],1],2],3],4]" };
            //string[] lines = { "[7,[6,[5,[4,[3,2]]]]]" };
            //string[] lines = { "[[6,[5,[4,[3,2]]]],1]" };
            //string[] lines = { "[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]" };
            //string[] lines = { "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]" };

//            string[] lines = {
//"[[[[4,3],4],4],[7,[[8,4],9]]]",
//"[1,1]"
//            };

//              string[] lines = {
//"[1,1]",
//"[2,2]",
//"[3,3]",
//"[4,4]",
//"[5,5]",
//"[6,6]"
//              };

//            string[] lines = {
//"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
//"[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
//"[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
//"[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
//"[7,[5,[[3,8],[1,4]]]]",
//"[[2,[2,2]],[8,[8,1]]]",
//"[2,9]",
//"[1,[[[9,3],9],[[9,0],[0,7]]]]",
//"[[[5,[7,4]],7],1]",
//"[[[[4,2],2],6],[8,7]]"
//                };
             string[] lines = {
"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
"[[[5,[2,8]],4],[5,[[9,9],0]]]",
"[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
"[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
"[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
"[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
"[[[[5,4],[7,7]],8],[[8,3],8]]",
"[[9,3],[[9,9],[6,[4,9]]]]",
"[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
"[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"
             };

             //*/

             return lines;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            string[] data = getData();

            /*
            Pair p = new Pair(data[0]);
            string str = Pair.reduce(p).print();
            */

            /*
            Pair p1 = new Pair(data[0]);
            Pair p2 = new Pair(data[1]);
            Pair p3 = Pair.add(p1, p2);
            string str = p3.print();
            */

            /*
            Pair p1 = new Pair("[[[[1,1],[2,2]],[3,3]],[4,4]]");
            Pair p2 = new Pair("[5,5]");
            Pair p3 = Pair.add(p1, p2);
            string str1 = p3.print();
            //*/

            /*
            Pair mag = new Pair("[[9,1],[1,9]]");
            int m = mag.magnitude();
            */


            //part1
            Pair acc = new Pair(data[0]);
            for(int i = 1; i < data.Length; i++)
            {
                Pair p = new Pair(data[i]);
                acc = Pair.add(acc, p);
            }
            string str = acc.print();
            Console.WriteLine(str);
            Console.WriteLine(acc.magnitude());
            Console.WriteLine();
            Console.WriteLine();

            //part 2
            int bigmag = 0;
            Pair num1 = null, num2 = null;
            for(int i = 0; i < data.Length - 1;i++)
            {
                Pair p1 = new Pair(data[i]);
                for (int j = i + 1; j < data.Length; j++)
                {
                    Pair p2 = new Pair(data[j]);

                    //a+b
                    Pair res = Pair.add(p1, p2);
                    if (res.magnitude() > bigmag)
                    {
                        bigmag = res.magnitude();
                        num1 = p1;
                        num2 = p2;
                    }

                    //b+a
                    res = Pair.add(p2, p1);
                    if (res.magnitude() > bigmag)
                    {
                        bigmag = res.magnitude();
                        num1 = p2;
                        num2 = p1;
                    }
                }
            }
            Console.WriteLine("Magnitude " + bigmag + " is made by adding the following two:");
            Console.WriteLine(num1.print());
            Console.WriteLine(num2.print());

        }


        public class Pair
        {
            int l = 0;
            int r = 0;
            Pair lp = null;
            Pair rp = null;

            public Pair(int l, int r)
            {
                this.l = l;
                this.r = r;
            }

            public Pair(Pair l, Pair r)
            {
                this.lp = l;
                this.rp = r;
            }

            public Pair(Pair l, int r)
            {
                this.lp = l;
                this.r = r;
            }

            public Pair(int l, Pair r)
            {
                this.l = l;
                this.rp = r;
            }

            public Pair(string p)
            {
                StringBuilder sb = new StringBuilder(p);
                Pair temp = parsePair(sb);
                l = temp.l;
                r = temp.r;
                lp = temp.lp;
                rp = temp.rp;
            }
            Pair(){}

            public Pair clone()
            {
                Pair p = new Pair();
                p.l = l;
                p.r = r;
                if (lp != null)
                {
                    p.lp = lp.clone();
                }
                if (rp != null)
                {
                    p.rp = rp.clone();
                }
                return p;
            }

            Pair parsePair(StringBuilder sb)
            {
                Pair p = new Pair(0, 0);
                //open
                if (sb[0] != '[')
                {
                    throw new ArgumentException("Pair not starting with '['");
                }
                sb.Remove(0, 1);
                //left
                if (sb[0] == '[')
                {
                    p.lp = parsePair(sb);
                }
                else
                {
                    string l = sb.ToString();
                    int pos = l.IndexOf(',');
                    if (pos < 0)
                    {
                        throw new ArgumentException("Pair not containing ','");
                    }
                    p.l = int.Parse(l.Substring(0, pos));
                    sb.Remove(0, pos);
                }
                //comma
                if (sb[0] != ',')
                {
                    throw new ArgumentException("Pair not containing ','");
                }
                sb.Remove(0, 1);
                //right
                if (sb[0] == '[')
                {
                    p.rp = parsePair(sb);
                }
                else
                {
                    string r = sb.ToString();
                    int pos = r.IndexOf(']');
                    if (pos < 0)
                    {
                        throw new ArgumentException("Pair not ending with ']'");
                    }
                    p.r = int.Parse(r.Substring(0, pos));
                    sb.Remove(0, pos);
                }
                //close
                if (sb[0] != ']')
                {
                    throw new ArgumentException("Pair not ending with ']'");
                }
                sb.Remove(0, 1);
                return p;
            }

            public string print()
            {
                StringBuilder sb = new StringBuilder();
                print(sb);
                return sb.ToString();
            }

            void print(StringBuilder sb)
            {
                sb.Append('[');
                if (lp != null)
                {
                    lp.print(sb);
                }
                else
                {
                    sb.Append(l);
                }
                sb.Append(',');
                if (rp != null)
                {
                    rp.print(sb);
                }
                else
                {
                    sb.Append(r);
                }
                sb.Append(']');
            }

            public static Pair add(Pair l, Pair r)
            {

                return reduce(new Pair(l.clone(), r.clone()));
            }

            public static Pair reduce(Pair p)
            {
                bool action = false;
                do
                {
                    action = false;
                    if (Pair.tryExplode(p))
                    {
                        action = true;
                        continue;
                    }
                    if (Pair.trySplit(p))
                    {
                        action = true;
                        continue;
                    }

                } while (action);
                return p;
            }

            static bool tryExplode(Pair p)
            {
                Pair pp = tryExplode(p, 1, null);
                return pp != null;
            }
            static Pair tryExplode(Pair p,int depth, Pair add)
            {
                //add
                if(add != null)
                {
                    if(add.l != 0)
                    {
                        if(p.rp != null)
                        {
                            tryExplode(p.rp, 0, add);
                        }
                        else
                        {
                            p.r += add.l;
                            add.l = 0;
                        }
                    }
                    if(add.r != 0)
                    {
                        if(p.lp != null)
                        {
                            tryExplode(p.lp, 0, add);
                        }
                        else
                        {
                            p.l += add.r;
                            add.r = 0;
                        }
                    }
                    return null;
                }

                //check for explode
                if(depth == 5)
                {
                    //exploding
                    return p;

                }
                //check for exploding children
                if(p.lp != null)
                {
                    Pair result = tryExplode(p.lp, depth + 1, add);
                    if(result != null)
                    {
                        if (depth == 4)
                        {
                            p.lp = null;
                            p.l = 0;
                        }
                        if(p.rp == null)
                        {
                            p.r += result.r;
                            result.r = 0;
                        }
                        else if (result.r != 0)
                        {
                            Pair r = new Pair(0, result.r);
                            tryExplode(p.rp, 0, r);
                            result.r = 0;
                        }
                        return result;
                    }
                }
                if(p.rp != null)
                {
                    Pair result = tryExplode(p.rp, depth + 1, add);
                    if(result != null)
                    {
                        if (depth == 4)
                        {
                            p.rp = null;
                            p.r = 0;
                        }
                        if(p.lp == null)
                        {
                            p.l += result.l;
                            result.l = 0;
                        }
                        else if(result.l != 0)
                        {
                            Pair l = new Pair(result.l,0);
                            tryExplode(p.lp, 0, l);
                            result.l = 0;
                        }
                        return result;
                    }
                }
                return null;
            }

            static bool trySplit(Pair p) {
                bool split = false;
                if(p.lp != null)
                {
                    split = Pair.trySplit(p.lp);
                }
                else
                {
                    if(p.l > 9)
                    {
                        p.lp = Pair.doSplit(p.l);
                        p.l = 0;
                        split = true;
                    }
                }
                if (split)
                    return true;
                if (p.rp != null)
                {
                    split = Pair.trySplit(p.rp);
                }
                else
                {
                    if (p.r > 9)
                    {
                        p.rp = Pair.doSplit(p.r);
                        p.r = 0;
                        split = true;
                    }
                }
                return split;
            }

            static Pair doSplit(int n)
            {
                return new Pair(n / 2, n / 2 + (n % 2));
            }
            
            public int magnitude()
            {
                int m;
                if(lp != null)
                {
                    m = 3 * lp.magnitude();
                }
                else
                {
                    m = 3 * l;
                }
                if (rp != null)
                {
                    m += 2 * rp.magnitude();
                }
                else
                {
                    m += 2 * r;
                }
                return m;
            }
        }

    }
}
