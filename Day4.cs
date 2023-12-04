using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace AOC23
{   
    public class Card
    {
        public int id;
        public List<int> have = [];
        public List<int> winning = [];

        public Card(string input)
        {
            var card_id_re = new Regex(@"Card\s+(\d+)");

            this.id = int.Parse(card_id_re.Match(input.Split(':')[0]).Groups[1].Value);

            var rem = input.Split(':')[1].Split('|');
            string have = rem[1].Trim();
            string win = rem[0].Trim();

            foreach(string num in have.Split(' '))
            {
                if (num.Trim().Length > 0)
                    this.have.Add(int.Parse(num));
            }
            foreach(string num in win.Split(' '))
            {
                if (num.Trim().Length > 0)
                    this.winning.Add(int.Parse(num));
            }

        }

        public int Matches 
        { 
            get 
            {
                int count = 0;
                foreach (var num in have) 
                {
                    if (winning.Contains(num))
                    { count++; }
                }  
                if (count == 0) { return 0; }
                return count; 
            } 
        
        }
    }
    public class Day4(bool test) : Day(4, test)
    {
        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            var cards = new List<Card>();

            int score = 0;
            foreach (var line in lines)
            {
                var c = new Card(line);
                score += (int)Math.Pow(2, c.Matches - 1);

                cards.Add(c);

            }

            Console.WriteLine(score);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            var cards = new List<Card>();

            int score = 0;
            foreach (var line in lines)
            {
                var c = new Card(line);
                score += (int)Math.Pow(2, c.Matches - 1);

                cards.Add(c);

            }

        }
    }
}
