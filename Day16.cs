using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day16 : Day
    {
        int totalVersion = 0;
        StringBuilder currentBits = new StringBuilder();
        Dictionary<char, string> Hex2Bin = new Dictionary<char, string>();

        string[] getData()
        {
            //*
            int day = int.Parse(this.GetType().Name.Substring(3));
            string[] lines = Program.readFile(day);
            /*/

            //string[] lines = {"D2FE28"};
            //string[] lines = { "38006F45291200" };
            //string[] lines = { "EE00D40C823060" };
            //string[] lines = { "8A004A801A8002F478" };
            //string[] lines = { "620080001611562C8802118E34" };
            //string[] lines = { "C0015000016115A2E0802F182340" };
            //string[] lines = { "A0016C880162017C3686B18A3D4780" };

            //string[] lines = {"C200B40A82"};
            //string[] lines = { "04005AC33890" };
            //string[] lines = { "880086C3E88112" };
            //string[] lines = { "CE00C43D881120" };
            //string[] lines = { "D8005AC2A8F0" };
            //string[] lines = { "F600BC2D8F" };
            //string[] lines = { "9C005AC2F8F0" };
            //string[] lines = { "9C0141080250320F1802104A08" };
            //*/

            return lines;
        }

        public override void Run()
        {

            Hex2Bin.Add('0', "0000");
            Hex2Bin.Add('1', "0001");
            Hex2Bin.Add('2', "0010");
            Hex2Bin.Add('3', "0011");
            Hex2Bin.Add('4', "0100");
            Hex2Bin.Add('5', "0101");
            Hex2Bin.Add('6', "0110");
            Hex2Bin.Add('7', "0111");
            Hex2Bin.Add('8', "1000");
            Hex2Bin.Add('9', "1001");
            Hex2Bin.Add('A', "1010");
            Hex2Bin.Add('B', "1011");
            Hex2Bin.Add('C', "1100");
            Hex2Bin.Add('D', "1101");
            Hex2Bin.Add('E', "1110");
            Hex2Bin.Add('F', "1111");


            Console.WriteLine("Do Something!");

            string[] data = getData();

            StringBuilder sb = new StringBuilder(data[0]);
            int length = 0;
            long value = readPacket(sb, ref length);

            Console.WriteLine("Version Sum: " + totalVersion);
            Console.WriteLine("Value: " + value);
        }

        long readPacket(StringBuilder sb, ref int length)
        {
            long value = 0;

            string version = getBits(sb,ref length,3);
            
            int ver = Convert.ToInt32(version, 2);
            totalVersion += ver;
            string type = getBits(sb, ref length, 3);
            int t = Convert.ToInt32(type, 2);
            switch (t)
            {
                case 4:
                    //literal value
                    bool cont = true;
                    string valstr = "";
                    while(cont)
                    {
                        string tempstr = getBits(sb, ref length, 5);
                        if (tempstr.StartsWith('0'))
                        {
                            cont = false;
                        }
                        valstr += tempstr.Substring(1);
                    }
                    value = Convert.ToInt64(valstr, 2);
                    break;
                default:
                    //operator
                    List<long> valList = new List<long>();
                    if(getBits(sb, ref length, 1) == "0")
                    {
                        //total length (15 bits)
                        string lenstr = getBits(sb, ref length, 15);
                        int len = Convert.ToInt32(lenstr, 2);

                        while (len > 0)
                        {
                            int l = 0;
                            valList.Add(readPacket(sb, ref l));
                            len -= l;
                            length += l;
                        }
                    }
                    else
                    {
                        //packet count (11 bits)
                        string packetstr = getBits(sb, ref length, 11);
                        int pcount = Convert.ToInt32(packetstr, 2);

                        for(int i = 0; i < pcount; i++)
                        {
                            int l = 0;
                            valList.Add(readPacket(sb,ref l));
                            length += l;
                        }
                    }

                    switch (t)
                    {
                        case 0: //sum
                            foreach(long v in valList)
                            {
                                value += v;
                            }
                            break;
                        case 1: //product
                            value = 1;
                            foreach (long v in valList)
                            {
                                value *= v;
                            }
                            break;
                        case 2: //min
                            value = valList[0];
                            foreach (long v in valList)
                            {
                                value = Math.Min(value,v);
                            }
                            break;
                        case 3: //max
                            value = valList[0];
                            foreach (long v in valList)
                            {
                                value = Math.Max(value, v);
                            }
                            break;
                        case 5: //greater
                            if (valList[0] > valList[1])
                                value = 1;
                            break;
                        case 6: //less
                            if (valList[0] < valList[1])
                                value = 1;
                            break;
                        case 7: //equal
                            if (valList[0] == valList[1])
                                value = 1;
                            break;
                    }
                    break;
            }

            return value;
        }

        string getBits(StringBuilder sb,ref int totallen, int len)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < len; i++)
            {
                if (currentBits.Length == 0)
                {
                    currentBits.Append(Hex2Bin[sb[0]]);
                    sb = sb.Remove(0, 1);
                }
                result.Append(currentBits[0]);
                currentBits = currentBits.Remove(0, 1);
            }
            totallen += len;
            return result.ToString();
        }
    }
}
