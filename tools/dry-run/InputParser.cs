public static class InputParser
{
    public static string ReadString(string prompt = "Enter string: ")
    {
        Console.Write(prompt);
        return Console.ReadLine()!;
    }

    public static int ReadInt(string prompt = "Enter value: ")
    {
        Console.Write(prompt);
        return int.Parse(Console.ReadLine()!);
    }

    public static int[] ReadIntArray(string prompt = "Enter nums (space-separated): ")
    {
        Console.Write(prompt);
        return Console.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
    }

    public static char[] ReadCharArray(string prompt = "Enter chars: ")
    {
        Console.Write(prompt);
        return Console.ReadLine()!.ToCharArray();
    }
}
