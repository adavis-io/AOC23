// See https://aka.ms/new-console-template for more information


class AOC
{
    static void Main(string[] args)
    {
        List<AOC23.Day> days = new();

        bool test = args.Contains("--test");

        if (test)
        {
            Console.WriteLine("Running with test data!");
        }
        else
        {
            Console.WriteLine("Running with full data"); 
        }

        //days.Add(new AOC23.Day1(test));
        days.Add(new AOC23.Day2(test));
        //days.Add(new AOC23.Day3(test));

        foreach (var day in days)
        {
            Console.WriteLine(day.name);
            day.Part1();
            day.Part2();
        }
    }
}