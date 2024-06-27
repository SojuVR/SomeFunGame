using System.Text.RegularExpressions;
using System.Text.Json;
using Newtonsoft.Json;

[Serializable]
class Player
{
    public string playerName;
    [JsonProperty] private int level;
    [JsonProperty] private int money;
    public List<string> inventory;
    [JsonProperty] private List<string> bossNames;
    [JsonProperty] private List<string> bossEvidence;
    public Player()
    {
        this.playerName = "IDFK";
        this.level = 1;
        this.money = 10000;
        this.inventory = new List<string>();
        this.bossNames = new List<string>();
        this.bossEvidence = new List<string>();
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
        int min = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("minDmg").GetRawText())!;
        int max = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("maxDmg").GetRawText())!;
        int mult = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty(spot).GetRawText())!;
        Random random = new Random();
        int dmg = (random.Next(min, max + 1)) * mult;
        string desc = System.Text.Json.JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty(spot + "Desc").GetRawText())!;
        Console.WriteLine(desc);
        return dmg;
    }

    public int inflictFear(string force, string spot)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        int min = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("minFear").GetRawText())!;
        int max = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("maxFear").GetRawText())!;
        int mult = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty(spot).GetRawText())!;
        Console.ReadKey(true);
        Random random = new Random();
        int fear = (random.Next(min, max + 1)) * mult;
        return fear;
    }

    public int affectKelly(string force)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        int rep = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("kelly").GetRawText())!;
        return rep;
    }

    public void gearDescription(string force)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        string desc = System.Text.Json.JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("desc").GetRawText())!;
        Console.WriteLine(desc + "\n");
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
        if(!this.inventory.Contains("Fists"))
        {
            this.inventory.Add("Fists");
        }
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

    public void addBossName()
    {
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Bosses\Boss" + this.level + ".json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        string name = System.Text.Json.JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("name").GetRawText())!;
        this.bossNames.Add(name);
    }

    public void addBossEvidence()
    {
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Bosses\Boss" + this.level + ".json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        string evidence = System.Text.Json.JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("evidence").GetRawText())!;
        this.bossEvidence.Add(evidence);
    }

    public void displayNames()
    {
        Console.WriteLine("Current Terrorist Boss Detainees:\n");
        foreach (string name in this.bossNames)
        {
            Console.WriteLine(name);
        }
        Console.ReadKey(true);
    }

    public void displayEvidence()
    {
        Console.WriteLine("The evidence so far:\n");
        foreach (string evidence in this.bossEvidence)
        {
            Console.WriteLine(evidence + "\n");
            Console.ReadKey(true);
        }
    }
}
