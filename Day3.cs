using System.ComponentModel;
using System.Text;

namespace AOC23
{
    public class Part(int num, int[] pos, char symbol)
    {
        public readonly int num = num; 
        public readonly int[] position = pos; 
        public readonly char symbol = symbol;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Part ");
            sb.Append(this.num);
            sb.Append(" at [");
            sb.Append(this.position[0]);
            sb.Append(", ");
            sb.Append(this.position[1]);
            sb.Append("] - symbol \"");
            sb.Append(this.symbol);
            sb.Append("\"");

            return sb.ToString();
        }
        public bool LocEquals(Part other)
        {
            return this.position[0] == other.position[0] && this.position[1] == other.position[1];
        }
    }


    public class Day3(bool test) : Day(3, test)
    {
        public bool IsNum(char c)
        {
            return c >= '0' & c <= '9';
        }
        public bool IsPart(List<string> input, int[] pos)
        {
            List<int> x_offs = [0], y_offs = [0];

            char current = input[pos[1]][pos[0]];
            if (IsNum(current))
            {
                if (pos[0] > 0)
                {
                    x_offs.Add(-1);
                }
                if (pos[0] < input[0].Trim().Length - 1)
                {
                    x_offs.Add(1);
                }
                if (pos[1] > 0)
                {
                    y_offs.Add(-1);
                }
                if (pos[1] < input.Count - 1)
                {
                    y_offs.Add(1);
                }

                foreach(var x_os in x_offs)
                {
                    foreach(var y_os in y_offs)
                    {
                        if (x_os == 0 & y_os == 0)
                        { continue; }

                        var c = input[pos[1] + y_os][pos[0] + x_os];
                        if (!IsNum(c))
                        {
                            if (c != '.')
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public string ExtractPN(List<string> input, int[] pos)
        {
            int start = pos[0];
            int end = pos[0] + 1;

            int len = input[0].Trim().Length - 1;

            while(end <= len && IsNum(input[pos[1]][end]))
            {
                end++;
            }

            while(start > 0 && IsNum(input[pos[1]][start - 1]))
            {
                start--;
            }
            
            return input[pos[1]].Substring(start, end - start);
        }

        public int[] GetSymbolLoc(List<string> input, int[] pos)
        {
            int[] offs = [-1, 0, 1];

            foreach (var x_off in offs)
            {
                if((pos[0] + x_off > input[0].Trim().Length - 1) || (pos[0] + x_off < 0))
                {
                    continue;
                }
                foreach(var y_off in offs)
                {
                    if ((pos[1] + y_off >= input.Count) || (pos[1] + y_off < 0))
                    {
                        continue;
                    }
                    if (x_off == 0 && y_off == 0)
                    {
                        continue;
                    }

                    char curr = input[pos[1] + y_off][pos[0] + x_off];
                    if (!IsNum(curr) && curr != '.')
                    {
                        return [pos[0] + x_off, pos[1] + y_off];
                    }
                }
            }

            return [-1, -1];
        }
        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            int len = lines[0].Trim().Length;

            List<Part> parts = [];
            for (int j = 0; j < lines.Count; j++)
            {
                for (int i = 0; i < len; i++)
                {
                    if (IsPart(lines, [i, j]))
                    {
                        var pn = ExtractPN(lines, [i, j]);
                        
                        var loc = GetSymbolLoc(lines, [i, j]);
                        parts.Add(new Part(int.Parse(pn), loc, lines[loc[1]][loc[0]]));

                        //Console.WriteLine(parts.Last());
                        while (i + 1 < len && IsNum(lines[j][i + 1]))
                        {
                            i++;
                        }
                    }           
                }
            }

            int psum = 0;
            foreach (var part in parts)
            {
                psum += part.num;
            }
            Console.WriteLine(psum);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            int len = lines[0].Trim().Length;

            List<Part> parts = [];
            for (int j = 0; j < lines.Count; j++)
            {
                for (int i = 0; i < len; i++)
                {
                    if (IsPart(lines, [i, j]))
                    {
                        var pn = ExtractPN(lines, [i, j]);

                        var loc = GetSymbolLoc(lines, [i, j]);
                        parts.Add(new Part(int.Parse(pn), loc, lines[loc[1]][loc[0]]));

                        //Console.WriteLine(parts.Last());
                        while (i + 1 < len && IsNum(lines[j][i + 1]))
                        {
                            i++;
                        }
                    }
                }
            }

            int gear_ratio = 0;
            for (int i = 0; i < parts.Count; i++)
            {
                for (int j = i; j < parts.Count; j++)
                {
                    if (i == j) { continue; }

                    if (parts[i].symbol == '*' && parts[i].LocEquals(parts[j]))
                    {
                        gear_ratio += (parts[i].num * parts[j].num);
                    }
                }
            }

            Console.WriteLine(gear_ratio);

        }
    }
}
