
[Serializable]
class Home
{
    private Player player;
    public int utility;

    public Home(Player player)
    {
        this.player = player;
        this.utility = 20;
    }

    public void sleep()
    {
        this.player.setFatigue(0);
    }
}