
class Gear
{
    private Player player;
    private Dictionary<string, int> shop;
    public Gear(Player player)
    {
        this.player = player;
        this.shop = new Dictionary<string, int>() 
        {
          { "Knife", 10 },
        };
    }
    public void buyGear()
    {
        if (this.shop.Count == 0)
        {
            Console.WriteLine("[There are no items to buy.]\n");
            Console.ReadKey(true);
            return;
        }
        Console.WriteLine("[Choose an item to buy.]");
        foreach (KeyValuePair<string, int> g in shop)
        {
            Console.WriteLine(string.Format("{0,-5}", g.Key) + "     $" + g.Value);
        }
        Console.WriteLine("\nExit Store");
        while (true)
        {
            Console.WriteLine("[What would you like to order?] [Your Money: $" + this.player.getMoney() + "]");
            string order = Console.ReadLine();
            string upper = char.ToUpper(order[0]) + order.Substring(1);
            if (upper.Contains("Exit"))
            {
                return;
            }
            else if (shop.ContainsKey(upper))
            {
                if (this.player.getMoney() >= shop[upper])
                {
                    Console.WriteLine("[You ordered: " + upper + "]\n");
                    this.player.subtractMoney(shop[upper]);
                    shop.Remove(upper);
                    this.player.addToInventory(upper);
                    return;
                }
                else
                {
                    Console.WriteLine("[You don't have enough money.]");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("[That is not an available item to order.]");
                continue;
            }
        }
    }

    public void addToShop()
    {

    }
}