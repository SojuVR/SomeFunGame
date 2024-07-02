using System.Text.Json;

[Serializable]
class Home
{
    private Player player;
    private int utility;
    private bool lastChance = false;
    private Dictionary<string, int> homes;
    public string currentHome;

    public Home(Player player)
    {
        this.player = player;
        this.utility = 30;
        this.homes = new Dictionary<string, int>();
        this.currentHome = "Studio";
    }

    public int goHome()
    {
        setUtility();
        while (true)
        {
            Console.WriteLine("\n[Choose from the following options.]\nSleep               Examine House               Go to Work" +
            "\nShop for New House");
            string choice = Console.ReadLine()!;
            choice = choice.ToLower();
            if (choice == "sleep")
            {
                int answer = sleep();
                return answer;
            }
            else if (choice == "examine house")
            {
                examine();
                continue;
            }
            else if (choice == "go to work")
            {
                break;
            }
            else if (choice == "shop for new house")
            {
                addToHouses();
                buyHome();
                setUtility();
                continue;
            }
            else
            {
                Console.WriteLine("\n[That was not an option.]");
                continue;
            }
        }
        return 0;
    }

    public int sleep()
    {
        if (player.getMoney() >= 0)
        {
            Console.WriteLine("\n[You go to sleep.]");
            Console.ReadKey(true);
            this.player.setFatigue(0);
            Console.WriteLine("[" + utility + " dollars was subtracted for utilities.]");
            Console.ReadKey(true);
            this.player.subtractMoney(utility);
            if (player.getMoney() < 0 && lastChance == false)
            {
                lastChance = true;
            }
            else if (lastChance == true && player.getMoney() > 0)
            {
                lastChance = false;
            }
            return 1;
        }
        else
        {
            Console.WriteLine("\n[You tried to sleep with no money in your bank account. You've been evicted from your home.]");
            Console.WriteLine("[You cannot continue your job being a bum on the streets. You lose!]");
            return 2;
        }
    }

    public void buyHome()
    {
        while (true)
        {
            Console.WriteLine("\n[Choose a house to buy.]");
            foreach (KeyValuePair<string, int> g in homes)
            {
                Console.WriteLine(string.Format("{0,-9}", g.Key) + "     $" + g.Value);
            }
            Console.WriteLine("Exit Store\n");
            Console.WriteLine("[What would you like to buy?] [Your Money: $" + this.player.getMoney() + "]");
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
            if (homes.ContainsKey(upper))
            {
                if (this.player.getMoney() >= homes[upper])
                {
                    this.player.houseDescription(upper);
                    Console.WriteLine("\n[You'd like to buy: " + upper + ", correct?] [Yes/No]\n");
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
                            Console.WriteLine("\n[You bought: " + upper + "]\n");
                            this.player.subtractMoney(homes[upper]);
                            homes.Remove(upper);
                            currentHome = upper;
                            return;
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
                Console.WriteLine("\n[That is not an available house to order.]");
                continue;
            }
        }
    }

    public void addToHouses()
    {
        if (!this.homes.ContainsKey("Studio") && this.currentHome != "Studio")
        {
            this.homes.Add("Studio", 50);
        }
        if (!this.homes.ContainsKey("Apartment") && this.currentHome != "Apartment")
        {
            this.homes.Add("Apartment", 100);
        }
        if (!this.homes.ContainsKey("Townhouse") && this.currentHome != "Townhouse")
        {
            this.homes.Add("Townhouse", 200);
        }
        if (!this.homes.ContainsKey("Farmhouse") && this.currentHome != "Farmhouse")
        {
            this.homes.Add("Farmhouse", 300);
        }
        if (!this.homes.ContainsKey("Modern") && this.currentHome != "Modern")
        {
            this.homes.Add("Modern", 1000);
        }
    }

    public void examine()
    {
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Homes\" + currentHome + ".json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        string effect = JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("homeEffect").GetRawText())!;
        Console.WriteLine("\n" + effect);
        Console.ReadKey(true);
    }

    public void setUtility()
    {
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Homes\" + currentHome + ".json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        utility = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("utility").GetRawText())!;
    }

    public int getBuff()
    {
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Homes\" + currentHome + ".json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        int buff = JsonSerializer.Deserialize<int>(jsonDocument.RootElement.GetProperty("fatigue").GetRawText())!;
        return buff;
    }
}