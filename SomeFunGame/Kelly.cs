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
        if (this.level >= -20 && this.level < -10)
        {
            Console.WriteLine("Kelly: \"Were you serious when you told us you are one of the best interrogators in the nation?\"" +
                "\n[You definitely aren't convincing anyone that you know what you are doing. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
        }
        if (this.level >= -10 && this.level < 0)
        {
            Console.WriteLine("Kelly: \"I was told you were better at your job...\"" +
                "\n[You need to pick it up. Your relationship with her is " + this.level + ".]\n");
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

    public bool checkFail()
    {
        if (this.level < -20)
        {
            Console.WriteLine("Kelly: \"That's it. I'm done. You suck at your job; you're fired.\"" +
                "\n[Your relationship with her is " + this.level + ". You lost the game.]\n");
            Console.ReadKey(true);
            return true;
        }
        else
        {
            return false;
        }
    }
}