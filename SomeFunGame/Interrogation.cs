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
        Console.WriteLine("[Kelly brings in the prisoner and briefs you.]");
        this.victim.describeInterrogated();
        Console.ReadKey(true);
    }
}