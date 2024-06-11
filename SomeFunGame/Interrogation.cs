class Interrogation
{
    private Interrogated victim;
    private Player player;

    public Interrogation(Interrogated interrogated, Player player)
    {
        this.victim = interrogated;
        this.player = player;
    }

    public void Interrogate()
    {
        Console.WriteLine("[You cannot interrogate right now.]\n");
        Console.ReadKey(true);
    }
}