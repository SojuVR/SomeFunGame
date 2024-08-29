using System.Text.RegularExpressions;
using System.Text.Json;
using Newtonsoft.Json;

[Serializable]
class Player
{
    public string playerName;
    [JsonProperty] private int level;
    [JsonProperty] private int money;
    [JsonProperty] private double fatigue;
    public List<string> inventory;
    public List<string> powerups;
    [JsonProperty] private List<string> bossNames;
    [JsonProperty] private List<string> bossEvidence;
    
    public Player()
    {
        this.playerName = "IDFK";
        this.level = 1;
        this.money = 10;
        this.fatigue = 0;
        this.inventory = new List<string>();
        this.powerups = new List<string>();
        this.bossNames = new List<string>();
        this.bossEvidence = new List<string>();
    }

    public void setName()
    {
        Console.Write("\nEnter your desired name: ");
        string pattern = @"^.{1,14}$"; // Pattern to match one or more digits
        string input = Console.ReadLine()!;

        Match match = Regex.Match(input, pattern);

        while (!match.Success)
        {
            Console.WriteLine("\n14 characters max. Cannot be blank.");
            input = Console.ReadLine()!;
            match = Regex.Match(input, pattern);
        }
        this.playerName = input;
    }

    public void levelUp(int xp)
    {
        this.level += xp;
    }

    public int inflictWound(string force, string spot, bool shake)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        int min = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("minDmg").GetRawText())!;
        int max = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("maxDmg").GetRawText())!;
        int mult = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty(spot).GetRawText())!;
        Random random = new Random();
        double dmg = ((random.Next(min, max + 1)) * mult) * (1 - fatigue);
        if (shake == true)
        {
            dmg *= 2;
        }
        string desc = System.Text.Json.JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty(spot + "Desc").GetRawText())!;
        Console.WriteLine("\n" + desc);
        return (int)dmg;
    }

    public int inflictFear(string force, string spot, bool monster)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Gear\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        int min = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("minFear").GetRawText())!;
        int max = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("maxFear").GetRawText())!;
        int mult = System.Text.Json.JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty(spot).GetRawText())!;
        Console.ReadKey(true);
        Random random = new Random();
        double fear = ((random.Next(min, max + 1)) * mult) * (1 - fatigue);
        if (monster == true)
        {
            fear *= 2;
        }
        return (int)fear;
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
        Console.WriteLine("\n" + desc + "\n");
    }

    public void houseDescription(string force)
    {
        string fileName = force + ".json";
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Homes\" + fileName);
        var jsonDocument = JsonDocument.Parse(jsonString);
        string desc = System.Text.Json.JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("desc").GetRawText())!;
        Console.WriteLine("\n" + desc + "\n");
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
        Console.WriteLine("\nYour arsenal consists of:");
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

    public void getPowerups()
    {
        Console.WriteLine("\nYour drinks:");
        for (int i = 0; i < powerups.Count; i += 1)
        {
            Console.WriteLine(powerups[i]);
        }
        Console.WriteLine();
    }

    public void addToInventory(string gear)
    {
        this.inventory.Add(gear);
    }

    public void addToPowerups(string drink)
    {
        if (!this.powerups.Contains(drink))
        {
            this.powerups.Add(drink);
        }
        else
        {
            throw new Exception();
        }
    }

    public void removePowerups(string drink)
    {
        this.powerups.Remove(drink);
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
        Console.WriteLine("\nCurrent Terrorist Boss Detainees:\n");
        foreach (string name in this.bossNames)
        {
            Console.WriteLine(name);
        }
        Console.ReadKey(true);
    }

    public void displayEvidence()
    {
        Console.WriteLine("\nThe evidence so far:\n");
        foreach (string evidence in this.bossEvidence)
        {
            Console.WriteLine(evidence + "\n");
            Console.ReadKey(true);
        }
    }

    public bool addFatigue(double buff = 0)
    {
        fatigue += 0.34;
        fatigue -= (buff/100);
        if (fatigue >= 1.00)
        {
            return true;
        }
        return false;
    }

    public void setFatigue(double num)
    {
        fatigue = num;
    }

    public void decreaseFatigue(double buff)
    {
        fatigue -= buff;
        if (fatigue < 0)
        {
            setFatigue(0);
        }
    }

    public double getFatigue()
    {
        return fatigue;
    }

    public void status()
    {
        if (fatigue >= 0 && fatigue < 0.20)
        {
            Console.WriteLine("[You are feeling fantastic and ready to go.]\n");
        }
        else if (fatigue >= 0.20 && fatigue < 0.68)
        {
            Console.WriteLine("[You are feeling a little worn out.]\n");
        }
        else if (fatigue >= 0.68 && fatigue < 1.00)
        {
            Console.WriteLine("[You are feeling incredibly exhausted.]\n");
        }
    }
}
