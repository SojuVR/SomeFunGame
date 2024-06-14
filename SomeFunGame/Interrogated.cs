using System.Text.Json;
class Interrogated
{
    private List<int> health { get; set; }
    private List<int> fear { get; set; }
    private List<string> gender { get; set; }
    private List<string> hairColor { get; set; }
    private List<string> hairLength { get; set; }
    private List<string> eyeColor { get; set; }
    private List<string> looks { get; set; }
    private List<string> build { get; set; }
    private List<string> age { get; set; }
    private int healthMax;
    private int fearMax;
    private int healthNum;
    private int fearNum;
    public Interrogated()
    {
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Interrogated\interrogated.json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        this.eyeColor = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("eyeColor").GetRawText())!;
        this.hairColor = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("hairColor").GetRawText())!;
        this.hairLength = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("hairLength").GetRawText())!;
        this.age = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("age").GetRawText())!;
        this.gender = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("gender").GetRawText())!;
        this.looks = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("looks").GetRawText())!;
        this.build = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("build").GetRawText())!;
        this.health = JsonSerializer.Deserialize<List<int>>(jsonDocument.RootElement.GetProperty("health").GetRawText())!;
        this.fear = JsonSerializer.Deserialize<List<int>>(jsonDocument.RootElement.GetProperty("fear").GetRawText())!;
        this.healthMax = GetRandomAttributeInt(this.health);
        this.fearMax = GetRandomAttributeInt(this.fear);
        this.healthNum = this.healthMax;
        this.fearNum = 0;
    }

    public void describeInterrogated()
    {
        string gender = GetRandomAttributeString(this.gender);
        if (gender == "Female")
        {
            Console.WriteLine("\"The captive is female with a " + GetRandomAttributeString(this.looks) + " face. " +
                "She has a " + GetRandomAttributeString(this.build) + " build and looks to be " + GetRandomAttributeString(this.age) + ". " +
                "She has " + GetRandomAttributeString(this.hairLength) + ", " + GetRandomAttributeString(this.hairColor) + " hair and" +
                " " + GetRandomAttributeString(this.eyeColor) + " eyes.");
        }
        else if (gender == "Male")
        {
            Console.WriteLine("\"The captive is male with a " + GetRandomAttributeString(this.looks) + " face. " +
                "He has a " + GetRandomAttributeString(this.build) + " build and looks to be " + GetRandomAttributeString(this.age) + ". " +
                "He has " + GetRandomAttributeString(this.hairLength) + ", " + GetRandomAttributeString(this.hairColor) + " hair and" +
                " " + GetRandomAttributeString(this.eyeColor) + " eyes.");
        }
        switch (this.healthMax)
        {
            case 10:
                Console.Write("The captive looks to be very frail and weak, ");
                break;
            case 20:
                Console.Write("The captive appears to be decently healthy, maybe a bit weak, ");
                break;
            case 30:
                Console.Write("The captive seems to be of average health, ");
                break;
            case 40:
                Console.Write("The captive doesn't look like someone to brawl with, ");
                break;
            case 50:
                Console.Write("This captive has definitely taken a beating and been fine before, ");
                break;
        }
        switch (this.fearMax)
        {
            case 10:
                Console.Write("and has what looks to be a face about ready to pee oneself.\"");
                break;
            case 20:
                Console.Write("and has a face that looks just to be a bit scared.\"");
                break;
            case 30:
                Console.Write("and has an indifferent face towards you.\"");
                break;
            case 40:
                Console.Write("and has a look that might be taunting the whole situation.\"");
                break;
            case 50:
                Console.Write("and looking into the " + gender.ToLower() + "'s face actually almost puts you in fear.\"");
                break;
        }
    }
    private string GetRandomAttributeString(List<string> attributes)
    {
        Random random = new Random();
        int index = random.Next(attributes.Count);
        return attributes[index];
    }
    private int GetRandomAttributeInt(List<int> attributes)
    {
        Random random = new Random();
        int index = random.Next(attributes.Count);
        return attributes[index];
    }

    public void getStatus()
    {
        float health = ((float)this.healthNum / this.healthMax) * 100;
        float fear = ((float)this.fearNum / this.fearMax) * 100;
        if (health < 25)
        {
            Console.WriteLine("[The captive doesn't look like much more can be taken.]");
        }
        else if (health >= 25 && health < 50)
        {
            Console.WriteLine("[The captive seems to be in pain but remains resilient.]");
        }
        else if (health >= 50 && health < 75)
        {
            Console.WriteLine("[The captive doesn't seem too bothered by this interrogation yet.]");
        }
        else if (health >= 75 && health <= 100)
        {
            Console.WriteLine("[The captive looks ready to burst out of the chair to fight you.]");
        }
        if (fear < 25)
        {
            Console.WriteLine("[The captive looks to be taunting you to try harder.]\n");
            Console.ReadKey(true);
        }
        else if (fear >= 25 && fear < 50)
        {
            Console.WriteLine("[The captive doesn't seem all that afraid of you.]\n");
            Console.ReadKey(true);
        }
        else if (fear >= 50 && fear < 75)
        {
            Console.WriteLine("[The captive is hoping for a prayer to be answered.]\n");
            Console.ReadKey(true);
        }
        else if (fear >= 75 && fear < 100)
        {
            Console.WriteLine("[The captive is trembling in the chair, unable to look at you.]\n");
            Console.ReadKey(true);
        }
    }

    public void increaseHealth(int health)
    {
        if (this.healthNum < this.healthMax)
        {
            this.healthNum += health;
        }
        if (this.healthNum > this.healthMax)
        {
            this.healthNum = this.healthMax;
        }
    }

    public void decreaseHealth(int dmg)
    {
        if (this.healthNum > 0)
        {
            this.healthNum -= dmg;
        }
        if (this.healthNum < 0)
        {
            this.healthNum = 0;
        }
    }

    public void increaseFear(int fear)
    {
        if (this.fearNum < this.fearMax)
        {
            this.fearNum += fear;
        }
        if (this.fearNum > this.fearMax)
        {
            this.fearNum = this.fearMax;
        }
    }

    public void decreaseFear(int fear)
    {
        if (this.fearNum > 0)
        {
            this.fearNum -= fear;
        }
        if (this.fearNum < 0)
        {
            this.fearNum = 0;
        }
    }

    public int getHealth()
    {
        return this.healthNum;
    }

    public int getFear()
    {
        return this.fearNum;
    }

    public int getMaxFear()
    {
        return this.fearMax;
    }

    public bool infoAttempt()
    {
        float chance;
        int num;
        Random random = new Random();
        chance = ((fearNum / fearMax) / 3) * 100;
        num = random.Next(100);
        if (num < chance)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}