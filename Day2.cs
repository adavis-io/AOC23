using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace AOC23
{
    public class Game
    {
        public int id = -1;
        public List<int[]> draws = new();

        public Game(string s)
        {
            this.id = int.Parse(s.Split(":")[0].Split(" ")[1]);

            var draw_str = s.Split(":")[1].Split(";");

            var rx_red = new Regex(@"(\d+) red");
            var rx_green = new Regex(@"(\d+) green");
            var rx_blue = new Regex(@"(\d+) blue");


            foreach (var draw in draw_str)
            {
                var rrm = rx_red.Match(draw);
                var grm = rx_green.Match(draw);
                var brm = rx_blue.Match(draw);
                var cubes = new int[3] { 0, 0, 0 };

                if (rrm.Success)
                    cubes[0] = int.Parse(rrm.Groups[1].Value);

                if (grm.Success)
                    cubes[1] = int.Parse(grm.Groups[1].Value);

                if (brm.Success)
                    cubes[2] = int.Parse(brm.Groups[1].Value);

                this.draws.Add(cubes);
             
            }
        }

        public bool isValid(int[] limits)
        {
            foreach (var draw in this.draws)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (draw[i] > limits[i])
                        return false;
                }
            }
            return true;
        }

        public int Power()
        {
            int[] max_cubes = new int[3] { -1, -1, -1 };

            foreach (var draw in this.draws)
            {
                for (int i=0; i<3; i++)
                {
                    if (draw[i] > max_cubes[i])
                    {
                        max_cubes[i] = draw[i];
                    }
                }
            }
            return max_cubes[0] * max_cubes[1] * max_cubes[2];

        }
    }
    public class Day2 : Day
    {
        public Day2(bool test) : base(2, test)
        {
        }
        public override void Part1()
        {
            Console.Write("\tPart 1: ");

            var limits = new int[3] { 12, 13, 14 };

            var lines = this.Load();

            int sum = 0;

            foreach (var line in lines)
            {
                var g = new Game(line);
                if (g.isValid(limits))
                {
                    sum += g.id;
                }
            }

            Console.WriteLine(sum);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            var limits = new int[3] { 12, 13, 14 };

            int sum = 0;

            foreach (var line in lines)
            {
                var g = new Game(line);
                sum += g.Power();
            }

            Console.WriteLine(sum);
        }
    }
}
