
class Gear
{
    private List<string> shop;
    public Gear()
    {
        this.shop = new List<string>();
        this.shop.Add("Knife");
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
        for (int i = 0; i < this.shop.Count; i++)
        {
            Console.WriteLine(shop[i] + "\n");
        }
        while(true)
        {
            Console.WriteLine("[What would you like to order?]");
            string order = Console.ReadLine();
            string upper = char.ToUpper(order[0]) + order.Substring(1);
            if (shop.Contains(upper))
            {
                Console.WriteLine("[You bought a " + upper + ".]\n");
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