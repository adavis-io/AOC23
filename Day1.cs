using System;
using System.Collections.Generic;
using System.Text;

namespace AOC22
{
    public class Day1 : Day
    {
        public Day1(bool test) : base(1, test)
        {
        }

        private int get_cal_1(IEnumerable<char> s)
        {
            foreach (char ch in s)
            {
                if (ch >= '0' & ch <= '9')
                {
                    return (int)ch - '0';
                }
            }

            return 0;
        }

        private int get_cal_2(IEnumerable<char> s, bool reversed)
        {
            if (reversed)
            { 
                s = s.Reverse(); 
            }
            string line = new string(s.ToArray());

            var number_strings = new List<string>() {
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine"
            };

            //forward
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] >= '0' & line[i] <= '9')
                {
                    return (int)line[i] - '0';
                }

                foreach (var n in number_strings)
                {
                    var num = n;
                    if (reversed)
                    {
                        num = new string(n.Reverse().ToArray());

                    }
                    

                    if (i + n.Length < line.Length)
                    {
                        if (line.Substring(i, n.Length) == num)
                        {
                           return number_strings.IndexOf(n) + 1;
                        }
                    }
                }
            }
            return 0;
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            List<int> cal_values = new();

            //scan f
            foreach (var line in lines)
            {
                int cur_cal = 10 * get_cal_1(line);

                cur_cal += get_cal_1(line.Reverse());

                cal_values.Add(cur_cal);

            }

            Console.WriteLine(cal_values.Sum());

        }

        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            

            List<int> cal_values = new();

            //scan f
            foreach (var line in lines)
            {
                int cur_cal = 10 * get_cal_2(line, false);
                cur_cal += get_cal_2(line, true);

                if (this.test)
                {
                    Console.WriteLine(cur_cal);
                }
                cal_values.Add(cur_cal);

            }

            Console.WriteLine(cal_values.Sum());

        }
    }
}
