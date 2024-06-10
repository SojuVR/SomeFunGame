
class Kelly
{
    private int level;
    public Kelly()
    {
        this.level = 0;
    }

    public void speakToKelly()
    {
        Console.WriteLine("[She seems busy. Your relationship with her is " + this.level + ".]\n");
        Console.ReadKey(true);
    }
}