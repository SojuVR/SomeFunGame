using Newtonsoft.Json;

[Serializable]
class Kelly
{
    [JsonProperty] private int level;
    public Kelly()
    {
        this.level = 0;
    }

    public void speakToKelly()
    {
        if (this.level >= -100 && this.level < 0)
        {
            Console.WriteLine("Kelly: \"I don't like the way you are doing things...\"" +
                "\n[She seems busy. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
        }
        if (this.level >= 0 && this.level < 10)
        {
            Console.WriteLine("[She seems busy. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
        }
        if (this.level >= 10 && this.level < 20)
        {
            Console.WriteLine("Kelly: \"Hey, You're doing alright. I'm expecting more, though.\"" +
                "\n[She seems a bit genuine. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
        }
    }

    public void addRep(int rep)
    {
        this.level += rep;
    }
}