
class Shop
{
    private Player player;
    private Dictionary<string, int> shop;
    private HashSet<string> addedItems;
    public Shop(Player player)
    {
        this.player = player;
        this.shop = new Dictionary<string, int>();
        this.addedItems = new HashSet<string>();
    }
    public void buyGear()
    {
        if (this.shop.Count == 0)
        {
            Console.WriteLine("[There are no items to buy.]\n");
            Console.ReadKey(true);
            return;
        }
        while (true)
        {
            Console.WriteLine("[Choose an item to buy.]");
            foreach (KeyValuePair<string, int> g in shop)
            {
                Console.WriteLine(string.Format("{0,-5}", g.Key) + "     $" + g.Value);
            }
            Console.WriteLine("Exit Store\n");
            Console.WriteLine("[What would you like to order?] [Your Money: $" + this.player.getMoney() + "]");
            string order = Console.ReadLine()!;
            order = order.ToLower();
            if (order.Contains("exit"))
            {
                return;
            }
            if (string.IsNullOrEmpty(order))
            {
                continue;
            }
            string upper = char.ToUpper(order[0]) + order.Substring(1);
            if (shop.ContainsKey(upper))
            {
                if (this.player.getMoney() >= shop[upper])
                {
                    this.player.gearDescription(upper);
                    Console.WriteLine("\n[You'd like to order: " + upper + ", correct?] [Yes/No]\n");
                    string response = "";
                    while (response != "yes" || response != "no")
                    {
                        response = Console.ReadLine()!;
                        response = response.ToLower();
                        if (response == "no")
                        {
                            break;
                        }
                        else if (response == "yes")
                        {
                            Console.WriteLine("[You ordered: " + upper + "]\n");
                            this.player.subtractMoney(shop[upper]);
                            shop.Remove(upper);
                            this.player.addToInventory(upper);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("[That was not a valid response].\n");
                        }
                    }
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
        if (this.player.getLevel() >= 1 && !addedItems.Contains("Knife") && !this.player.inventory.Contains("Knife"))
        {
            this.shop.Add("Knife", 10);
            addedItems.Add("Knife");
        }
        if (this.player.getLevel() >= 2 && !addedItems.Contains("Tazer") && !this.player.inventory.Contains("Tazer"))
        {
            this.shop.Add("Tazer", 20);
            addedItems.Add("Tazer");
        }
        if (this.player.getLevel() >= 3 && !addedItems.Contains("Gun") && !this.player.inventory.Contains("Gun"))
        {
            this.shop.Add("Gun", 30);
            addedItems.Add("Gun");
        }
    }
}