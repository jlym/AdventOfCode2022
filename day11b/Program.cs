using System.Numerics;

class Monkey
{
    public List<BigInteger> Items { get; set; } = new List<BigInteger>();
    public Func<BigInteger, BigInteger> Operation { get; set; } = (_) => 0;
    public BigInteger TestDivisor { get; set; }
    public int DestinationIfDivisible { get; set; }
    public int DestinationIfNotDivisible { get; set; }
    public BigInteger NumInspections { get; set; }
}

class Program
{
    static List<Monkey> GetProblemInput()
    {
        return new List<Monkey>()
        {
            new Monkey
            {
                Items = new List<BigInteger>() { 52, 60, 85, 69, 75, 75 },
                Operation = (old) => old * 17,
                TestDivisor = 13,
                DestinationIfDivisible = 6,
                DestinationIfNotDivisible = 7,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 96, 82, 61, 99, 82, 84, 85 },
                Operation = (old) => old + 8,
                TestDivisor = 7,
                DestinationIfDivisible = 0,
                DestinationIfNotDivisible = 7,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 95, 79 },
                Operation = (old) => old + 6,
                TestDivisor = 19,
                DestinationIfDivisible = 5,
                DestinationIfNotDivisible = 3,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 88, 50, 82, 65, 77 },
                Operation = (old) => old * 19,
                TestDivisor = 2,
                DestinationIfDivisible = 4,
                DestinationIfNotDivisible = 1,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 66, 90, 59, 90, 87, 63, 53, 88 },
                Operation = (old) => old + 7,
                TestDivisor = 5,
                DestinationIfDivisible = 1,
                DestinationIfNotDivisible = 0,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 92, 75, 62 },
                Operation = (old) => old * old,
                TestDivisor = 3,
                DestinationIfDivisible = 3,
                DestinationIfNotDivisible = 4,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 94, 86, 76, 67 },
                Operation = (old) => old + 1,
                TestDivisor = 11,
                DestinationIfDivisible = 5,
                DestinationIfNotDivisible = 2,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 57 },
                Operation = (old) => old + 2,
                TestDivisor = 17,
                DestinationIfDivisible = 6,
                DestinationIfNotDivisible = 2,
            },                            
        };
    }

    static List<Monkey> GetSampleInput()
    {
        return new List<Monkey>()
        {
            new Monkey
            {
                Items = new List<BigInteger>() { 79, 98 },
                Operation = (old) => old * 19,
                TestDivisor = 23,
                DestinationIfDivisible = 2,
                DestinationIfNotDivisible = 3,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 54, 65, 75, 74 },
                Operation = (old) => old + 6,
                TestDivisor = 19,
                DestinationIfDivisible = 2,
                DestinationIfNotDivisible = 0,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 79, 60, 97 },
                Operation = (old) => old * old,
                TestDivisor = 13,
                DestinationIfDivisible = 1,
                DestinationIfNotDivisible = 3,
            },
            new Monkey
            {
                Items = new List<BigInteger>() { 74 },
                Operation = (old) => old + 3,
                TestDivisor = 17,
                DestinationIfDivisible = 0,
                DestinationIfNotDivisible = 1,
            },
        };
    }

    public static void Main(string[] args)
    {
        var monkeys = GetProblemInput();

        var divisor = monkeys
            .Select(monkey => monkey.TestDivisor)
            .Aggregate(new BigInteger(1), (prod, next) => prod * next);

        for (int round = 0; round < 10000; round++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    var worryLevel = monkey.Operation(item);

                    // We need a way to keep worryLevel from getting too big. I think the key point is that
                    // we need to reduce worryLevel in a way such that `worryLevel mod testDivisor` stays the
                    // same for all monkeys' testDivisors, so that items get routed to the correct monkeys.
                    //
                    // I think modding worryLevel by the product of all testDivisors should do that.
                    //
                    // Modular arithmetic refresher: https://www.geeksforgeeks.org/modular-arithmetic/
                    worryLevel = worryLevel % divisor;

                    var isDivisible = worryLevel % monkey.TestDivisor == 0;
                    var destination = 
                        isDivisible ?
                        monkey.DestinationIfDivisible :
                        monkey.DestinationIfNotDivisible;

                    monkeys[destination].Items.Add(worryLevel);
                }

                monkey.NumInspections += monkey.Items.Count;
                monkey.Items = new List<BigInteger>();                
            }

            // Help check if we're on the right track.
            if (round == 0 || round == 19 || round == 999 || round == 1999 || round == 10000 - 1)
            {
                Console.Out.WriteLine($"After round {round}");
                for (int i = 0; i < monkeys.Count; i++)
                {
                    Console.Out.WriteLine($"Monkey: {i}, Num: {monkeys[i].NumInspections}");
                }                
                Console.Out.WriteLine();
            }            
        }        

        var numInspections = monkeys.Select(monkey => monkey.NumInspections).ToList();
        numInspections.Sort();
        numInspections.Reverse();

        var monkeyBusiness = numInspections[0] * numInspections[1];
        Console.Out.WriteLine($"{monkeyBusiness}");
    }    
}
