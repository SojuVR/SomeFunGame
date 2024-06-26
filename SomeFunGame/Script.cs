using Newtonsoft.Json;

public class Choice
{
    public string Text { get; set; }
    public int NextId { get; set; }
    public int Kelly { get; set; }
}

public class Text
{
    public string Desc { get; set; }
}

public class Section
{
    public int Id { get; set; }
    public List<Text> Texts { get; set; }
    public List<Choice> Choices { get; set; }
}

[Serializable]
class Script
{
    private Kelly kelly;
    private string player;
    public HashSet<int> finishedLevels = new HashSet<int> { 0 };
    private List<Section> Sections { get; set; }
    public Script(Kelly kelly, string playerName)
    {
        this.kelly = kelly;
        this.player = playerName;
    }

    public bool loadScene(int level)
    {
        try
        {
            string json = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Scripts\Scene" + level + ".json");
            Sections = JsonConvert.DeserializeObject<List<Section>>(json)!;
            if (!finishedLevels.Contains(level))
            {
                return playScene(level);
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private bool playScene(int level)
    {
        int currentId = 1;
        while (true)
        {
            try
            {
                Section currentSection = Sections.Find(section => section.Id == currentId)!;
                for (int i = 0; i < currentSection.Texts.Count; i++)
                {
                    string sentence = currentSection.Texts[i].Desc;
                    sentence = sentence.Replace("playerName", this.player);
                    Console.WriteLine(sentence);
                    Console.ReadKey(true);
                }
                Console.WriteLine("");
                if (currentSection.Choices.Count > 1)
                {
                    for (int i = 0; i < currentSection.Choices.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {currentSection.Choices[i].Text}");
                    }
                    int choice = 0;
                    while (true)
                    {
                        try
                        {
                            choice = int.Parse(Console.ReadLine()!) - 1;
                            currentId = currentSection.Choices[choice].NextId;
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("[Please choose one of the valid numbers.]");
                        }
                    }
                    this.kelly.addRep(currentSection.Choices[choice].Kelly);
                }
                else
                {
                    break;
                }
            }
            catch
            {
                break;
            }
        }
        finishedLevels.Add(level);
        return true;
    }
}