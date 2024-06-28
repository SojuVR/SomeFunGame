using Newtonsoft.Json;

[Serializable]
class Kelly
{
    [JsonProperty] private int level;
    public Kelly()
    {
        this.level = 0;
    }

    public int speakToKelly()
    {
        if (this.level >= -20 && this.level < -10)
        {
            Console.WriteLine("\nKelly: \"Were you serious when you told us you are one of the best interrogators in the nation?\"" +
                "\n[You definitely aren't convincing anyone that you know what you are doing. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
            return 0;
        }
        if (this.level >= -10 && this.level < 0)
        {
            Console.WriteLine("\nKelly: \"I was told you were better at your job...\"" +
                "\n[You need to pick it up. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
            return 0;
        }
        if (this.level >= 0 && this.level < 10)
        {
            Console.WriteLine("\n[She seems busy. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
            return 0;
        }
        if (this.level >= 10 && this.level < 20)
        {
            Console.WriteLine("\nKelly: \"Hey, You're doing alright. I'm expecting more, though.\"" +
                "\n[She seems a bit genuine. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
            return 0;
        }
        if (this.level >= 20)
        {
            Console.WriteLine("\nKelly: \"Hey, it's my favorite interrogator!\"" +
                "\n[She actually smiled at your dumb ass. Your relationship with her is " + this.level + ".]\n");
            Console.ReadKey(true);
            return 20;
        }
        return 0;
    }

    public void addRep(int rep)
    {
        this.level += rep;
    }

    public bool checkFail()
    {
        if (this.level < -20)
        {
            Console.WriteLine("\nKelly: \"That's it. I'm done. You clearly lied about your skills. You're fired. You know what termination entails...can't" +
                "let any information get out.\"" +
                "\n[She whips out a pistol and blasts a hole in your head. Your relationship with her was " + this.level + ". You lost the game.]\n");
            Console.ReadKey(true);
            return true;
        }
        else
        {
            return false;
        }
    }
}