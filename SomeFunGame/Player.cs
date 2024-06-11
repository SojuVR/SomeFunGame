using System.Text.RegularExpressions;
class Player
{
    public string playerName;
    private int level;
    private int money;
    private List<string> inventory;
    public Player()
    {
        this.playerName = "IDFK";
        this.level = 1;
        this.money = 0;
        this.inventory = new List<string>();
    }
    public void nextDialogue()
    {
        Console.ReadKey(true);
    }

    public void setName()
    {
        Console.Write("Enter your desired name: ");
        string pattern = @"^.{1,14}$"; // Pattern to match one or more digits
        string input = Console.ReadLine();

        Match match = Regex.Match(input, pattern);

        while (!match.Success)
        {
            Console.WriteLine("14 characters max. Cannot be blank.");
            input = Console.ReadLine();
            match = Regex.Match(input, pattern);
        }
        this.playerName = input;
    }

    public int getLevel()
    {
        return this.level;
    }

    public int getMoney()
    {
        return this.money;
    }

    public void getInventory()
    {
        Console.WriteLine("Your backpack contains:");
        for (int i = 0; i < inventory.Count; i+=2)
        {
            if (i + 1 < inventory.Count)
            {
                Console.WriteLine(string.Format("{0,-5}", inventory[i]) + "     " + inventory[i + 1]);
            }
            else
            {
                Console.Write(inventory[i]);
            }
        }
        Console.WriteLine();
    }

    public void addToInventory(string gear)
    {
        if(gear == "")
        {
            return;
        }
        this.inventory.Add(gear);
    }
}
