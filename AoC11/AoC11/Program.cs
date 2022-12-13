using System.Numerics;


namespace AoC11;
class Program
{
    static void Main(string[] args)
    {
        Monkey[] monkeys = CreateMonkeys();

        uint rounds = 10000;
        for (uint i = 0; i < rounds; i++)
        {

            Console.WriteLine(i);
            foreach (Monkey monkey in monkeys)
            {
                while (monkey.items.Count != 0)
                {
                    monkey.inspectionCount++;
                    BigInteger oldWorry = BigInteger.Parse(monkey.items.Dequeue());
                    BigInteger worry = CalculateNewWorryLevel(oldWorry, monkey.operation, monkey.opVal);

                    if (worry % monkey.testDiv == 0)
                        monkeys[monkey.ifTrue].items.Enqueue(worry.ToString());
                    else
                        monkeys[monkey.ifFalse].items.Enqueue(worry.ToString());
                }
            }
        }

        Console.WriteLine(CalculateMonkeyBusiness(monkeys));
    }

    static BigInteger CalculateMonkeyBusiness(Monkey[] monkeys)
    {
        monkeys = (from monkey in monkeys orderby monkey.inspectionCount select monkey).ToArray();
        BigInteger a = monkeys[^1].inspectionCount;
        BigInteger b = monkeys[^2].inspectionCount;

        //Console.WriteLine(a);
        //Console.WriteLine(b);

        return BigInteger.Multiply(a, b);
    }

    static void PrintMonkeys(Monkey[] monkeys)
    {
        foreach (Monkey monkey in monkeys)
        {
            Console.Write("Monkey " + monkey.num + ": ");
            foreach (string item in monkey.items)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");
        }
    }

    static BigInteger CalculateNewWorryLevel(BigInteger old, Monkey.MonkeyOperation op, int opValue)
    {
        BigInteger newValue = op.Invoke(old, opValue);
        //newValue = BigInteger.Divide(newValue, 3);
        return newValue;
    }

    static Monkey[] CreateMonkeys()
    {
        string input = File.ReadAllText("input.txt");
        string[] monki = input.Split("\n\n");
        List<Monkey> monkeys = new List<Monkey>();

        int i = 0;
        foreach (string line in monki)
        {
            string[] data = line.Split("\n");
            string[] items = data[1].Trim().Split(": ")[1].Split(", ");
            string op = data[2].Trim().Split(' ')[^2];
            string val = data[2].Trim().Split(' ')[^1];
            int testDiv = int.Parse(data[3].Trim().Split(' ')[^1]);
            int ifTrue = int.Parse(data[4].Trim().Split(' ')[^1]);
            int ifFalse = int.Parse(data[5].Trim().Split(' ')[^1]);

            Monkey monkey = new Monkey()
            {
                num = i,
                items = new Queue<string>(items),
                operation = op == "+" ? AddVal : val == "old" ? MultSelf : MultVal,
                opVal = int.TryParse(val, out int res) ? res : 0,
                testDiv = testDiv,
                ifTrue = ifTrue,
                ifFalse = ifFalse
            };

            monkeys.Add(monkey);
            i++;
        }

        return monkeys.ToArray();
    }

    static BigInteger MultVal(BigInteger old, int val)
    {
        if((val & (val - 1)) == 0)
        {
            int shift = (int)(MathF.Log((float)val) / Math.Log((float)2));
            return (old << shift);
            //return old;
        }
        else
        {
            // TODO
            return old;
        }
    }

    static BigInteger AddVal(BigInteger old, int val) => BigInteger.Add(old, val);
    static BigInteger MultSelf(BigInteger old, int dummy)
    {
        if (old == 0) return 0;

        if(old % 2 == 0)
        {
            BigInteger half = (old >> 1);
            return 4 * (half * half);
            //return old;
        }

        // TODO
        return old;

    }

    class Monkey
    {
        public delegate BigInteger MonkeyOperation(BigInteger old, int val);

        public int num;
        public Queue<string> items;
        public MonkeyOperation operation;
        public int opVal;
        public int testDiv;
        public int ifTrue;
        public int ifFalse;
        public int inspectionCount;
    }
}

