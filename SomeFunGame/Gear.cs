
class Gear
{
    private Dictionary<string, int> shop;
    public Gear()
    {
        this.shop = new Dictionary<string, int>() 
        {
          { "Knife", 10 },
        };
    }
    public string buyGear()
    {
        if (this.shop.Count == 0)
        {
            Console.WriteLine("[There are no items to buy.]\n");
            Console.ReadKey(true);
            return "";
        }
        Console.WriteLine("[Choose an item to buy.]");
        foreach (KeyValuePair<string, int> g in shop)
        {
            Console.WriteLine(string.Format("{0,-5}", g.Key) + "     $" + g.Value);
        }
        while(true)
        {
            Console.WriteLine("[What would you like to order?]");
            string order = Console.ReadLine();
            string upper = char.ToUpper(order[0]) + order.Substring(1);
            if (shop.ContainsKey(upper))
            {
                Console.WriteLine("[You ordered: " + upper + "]\n");
                shop.Remove(upper);
                return upper;
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