// See https://aka.ms/new-console-template for more information

class SomeFunGame
{
    static void Main()
    {
        new FunGame();
        string answer = "";
        while (true)
        {
            Console.WriteLine("Play Again? [Yes/No]");
            answer = Console.ReadLine()!;
            answer = answer.ToLower();
            if (answer == "yes")
            {
                new FunGame();
            }
            else if (answer == "no")
            {
                System.Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("That answer was not valid.\n");
                continue;
            }
        }
    }
}
