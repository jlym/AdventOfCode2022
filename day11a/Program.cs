using System.Text;
using System.Linq;
using System.Collections.Generic;

class Monkey
{
    public List<int> Items { get; set; }
    public Func<int, int> Operation { get; set; }
    public int TestDivisor { get; set; }
    public int DestinationIfDivisible { get; set; }
    public int DestinationIfNotDivisible { get; set; }
    public int NumInspections { get; set; }
}

class Program
{
    public static void Main(string[] args)
    {
        var monkeys = new List<Monkey>()
        {
            new Monkey
            {
                Items = new List<int>() { 52, 60, 85, 69, 75, 75 },
                Operation = (old) => old * 17,
                TestDivisor = 13,
                DestinationIfDivisible = 6,
                DestinationIfNotDivisible = 7,
            },
            new Monkey
            {
                Items = new List<int>() { 96, 82, 61, 99, 82, 84, 85 },
                Operation = (old) => old + 8,
                TestDivisor = 7,
                DestinationIfDivisible = 0,
                DestinationIfNotDivisible = 7,
            },
            new Monkey
            {
                Items = new List<int>() { 95, 79 },
                Operation = (old) => old + 6,
                TestDivisor = 19,
                DestinationIfDivisible = 5,
                DestinationIfNotDivisible = 3,
            },
            new Monkey
            {
                Items = new List<int>() { 88, 50, 82, 65, 77 },
                Operation = (old) => old * 19,
                TestDivisor = 2,
                DestinationIfDivisible = 4,
                DestinationIfNotDivisible = 1,
            },
            new Monkey
            {
                Items = new List<int>() { 66, 90, 59, 90, 87, 63, 53, 88 },
                Operation = (old) => old + 7,
                TestDivisor = 5,
                DestinationIfDivisible = 1,
                DestinationIfNotDivisible = 0,
            },
            new Monkey
            {
                Items = new List<int>() { 92, 75, 62 },
                Operation = (old) => old * old,
                TestDivisor = 3,
                DestinationIfDivisible = 3,
                DestinationIfNotDivisible = 4,
            },
            new Monkey
            {
                Items = new List<int>() { 94, 86, 76, 67 },
                Operation = (old) => old + 1,
                TestDivisor = 11,
                DestinationIfDivisible = 5,
                DestinationIfNotDivisible = 2,
            },
            new Monkey
            {
                Items = new List<int>() { 57 },
                Operation = (old) => old + 2,
                TestDivisor = 17,
                DestinationIfDivisible = 6,
                DestinationIfNotDivisible = 2,
            },                            
        };


        for (int round = 0; round < 20; round++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    var worryLevel = monkey.Operation(item);
                    worryLevel /= 3;
                    var isDivisible = worryLevel % monkey.TestDivisor == 0;
                    var destination = 
                        isDivisible ?
                        monkey.DestinationIfDivisible :
                        monkey.DestinationIfNotDivisible;

                    monkeys[destination].Items.Add(worryLevel);
                }

                monkey.NumInspections += monkey.Items.Count;
                monkey.Items = new List<int>();                
            }
        }        

        var numInspections = monkeys.Select(monkey => monkey.NumInspections).ToList();
        numInspections.Sort();
        numInspections.Reverse();

        var monkeyBusiness = numInspections[0] * numInspections[1];
        Console.Out.WriteLine($"{monkeyBusiness}");
    }    
}
