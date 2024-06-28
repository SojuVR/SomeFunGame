

class Cafe
{
    private Player player;
    public Dictionary<string, int> shop;

    public Cafe(Player player)
    {
        this.player = player;
        this.shop = new Dictionary<string, int>();
        this.shop.Add("Coffee", 20);
        this.shop.Add("Shake", 20);
        this.shop.Add("Monster", 20);
    }

    public void buyDrink()
    {
        while (true)
        {
            Console.WriteLine("\n[Choose an item to buy.]");
            foreach (KeyValuePair<string, int> g in shop)
            {
                Console.WriteLine(string.Format("{0,-7}", g.Key) + "     $" + g.Value);
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
                            try
                            {
                                Console.WriteLine("\n[You ordered: " + upper + "]\n");
                                this.player.subtractMoney(shop[upper]);
                                this.player.addToPowerups(upper);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("\n[You can only hold one drink of each at a time.]");
                                Console.ReadKey(true);
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n[That was not a valid response].\n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n[You don't have enough money.]");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("\n[That is not an available item to order.]");
                continue;
            }
        }
    }
}