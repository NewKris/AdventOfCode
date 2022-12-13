namespace AoC_1_1;
class Program
{

    static void Main(string[] args)
    {

        string input = File.ReadAllText("../../../input.txt");
        string[] inventory = input.Split("\n");

        List<string> elfPocket = new List<string>();
        List<string[]> pockets = new List<string[]>();

        for (int i = 0; i < inventory.Length; i++)
        {
            if (string.IsNullOrEmpty(inventory[i]))
            {
                pockets.Add(elfPocket.ToArray());
                elfPocket.Clear();
                continue;
            }

            elfPocket.Add(inventory[i]);
        }

        List<int> sums = new List<int>();

        for (int i = 0; i < pockets.Count; i++)
        {
            sums.Add(Sum(pockets[i]));
        }

        sums.Sort();

        Console.WriteLine(sums[^1]);
        Console.WriteLine(sums[^2]);
        Console.WriteLine(sums[^3]);

        Console.WriteLine("\n");

        Console.WriteLine(sums[^1] + sums[^2] + sums[^3]);

    }


    static void PrintArray(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine(arr[i]);
        }
    }

    static int Sum(string[] items)
    {

        int sum = 0;

        foreach(string item in items)
        {
            sum += int.Parse(item);
        }

        return sum;
    }


}

