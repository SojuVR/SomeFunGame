using System.Text.RegularExpressions;
using System.Text.Json;
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
        this.inventory.Add("Fists");
    }

    public void setName()
    {
        Console.Write("Enter your desired name: ");
        string pattern = @"^.{1,14}$"; // Pattern to match one or more digits
        string input = Console.ReadLine()!;

        Match match = Regex.Match(input, pattern);

        while (!match.Success)
        {
            Console.WriteLine("14 characters max. Cannot be blank.");
            input = Console.ReadLine()!;
            match = Regex.Match(input, pattern);
        }
        this.playerName = input;
    }
    
    public void levelUp(int xp)
    {
        this.level += xp;
    }

    public int inflictWound(string force, string spot)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        int min = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("minDmg").GetRawText())!;
        int max = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("maxDmg").GetRawText())!;
        int mult = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty(spot).GetRawText())!;
        Random random = new Random();
        return (random.Next(min, max)) * mult;
    }

    public int inflictFear(string force, string spot)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        int fear = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("fear").GetRawText())!;
        int mult = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty(spot).GetRawText())!;
        return fear * mult;
    }

    public int affectKelly(string force)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        int rep = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("kelly").GetRawText())!;
        return rep;
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
        Console.WriteLine("Your arsenal consists of:");
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
        this.inventory.Add(gear);
    }

    public void addMoney(int money)
    {
        this.money += money;
    }

    public void subtractMoney(int money)
    {
        if (this.money > 0)
        {
            this.money -= money;
        }
        if (this.money < 0)
        {
            this.money = 0;
        }
    }
}
