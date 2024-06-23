using Newtonsoft.Json;

public class Choice
{
    public string Text { get; set; }
    public int NextId { get; set; }
    public int Kelly { get; set; }
}

public class Section
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<Choice> Choices { get; set; }
}

class Script
{
    private int level;
    private Kelly kelly;
    private HashSet<int> finishedLevels = new HashSet<int> { 0 };
    private List<Section> Sections { get; set; }
    public Script(int playerLevel, Kelly kelly)
    {
        this.kelly = kelly;
        this.level = playerLevel;
        this.finishedLevels.Add(playerLevel);
    }

    public void loadScene(int level)
    {
        try
        {
            this.level = level;
            string json = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Scripts\Scene" + this.level + ".json");
            Sections = JsonConvert.DeserializeObject<List<Section>>(json)!;
            if (!finishedLevels.Contains(this.level))
            {
                playScene();
            }
            else
            {
                return;
            }
        }
        catch
        {
            return;
        }
    }

    private void playScene()
    {
        int currentId = 1;
        while (true)
        {
            try
            {
                Section currentSection = Sections.Find(section => section.Id == currentId)!;
                Console.WriteLine(currentSection.Text);
                for (int i = 0; i < currentSection.Choices.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentSection.Choices[i].Text}");
                }
                int choice = int.Parse(Console.ReadLine()!) - 1;
                currentId = currentSection.Choices[choice].NextId;
                this.kelly.addRep(currentSection.Choices[choice].Kelly);
            }
            catch
            {
                break;
            }
        }
        finishedLevels.Add(this.level);
    }
}