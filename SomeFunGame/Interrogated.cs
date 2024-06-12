using System.Text.Json;
class Interrogated
{
    private List<int> health { get; set; }
    private List<int> fear { get; set; }
    private List<int> anxiety { get; set; }
    private List<string> gender { get; set; }
    private List<string> hairColor { get; set; }
    private List<string> hairLength { get; set; }
    private List<string> eyeColor { get; set; }
    private List<string> looks { get; set; }
    private List<string> build { get; set; }
    private List<string> age { get; set; }
    private int healthNum;
    private int fearNum;
    public Interrogated()
    {
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Interrogated\interrogated.json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        this.eyeColor = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("eyeColor").GetRawText());
        this.hairColor = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("hairColor").GetRawText());
        this.hairLength = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("hairLength").GetRawText());
        this.age = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("age").GetRawText());
        this.gender = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("gender").GetRawText());
        this.looks = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("looks").GetRawText());
        this.build = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("build").GetRawText());
        this.health = JsonSerializer.Deserialize<List<int>>(jsonDocument.RootElement.GetProperty("health").GetRawText());
        this.fear = JsonSerializer.Deserialize<List<int>>(jsonDocument.RootElement.GetProperty("fear").GetRawText());
        this.healthNum = GetRandomAttributeInt(this.health);
        this.fearNum = GetRandomAttributeInt(this.fear);
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
        switch (this.healthNum)
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
        switch (this.fearNum)
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

    public void decreaseHealth(int dmg)
    {
        this.healthNum -= dmg;
    }

    public void increaseHealth(int heal)
    {
        this.healthNum += heal;
    }

    public void increaseFear(int fear)
    {
        this.fearNum += fear;
    }

    public void decreaseFear(int assurance)
    {
        this.fearNum -= assurance;
    }
}