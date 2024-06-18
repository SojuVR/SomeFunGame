using System.Text.RegularExpressions;
using Newtonsoft.Json;

class FunGame
{
    private Player player;
    private Kelly kelly;
    private Shop gear;
    private Interrogation interrogation;
    private string savePath = @"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Save\savePlayer.json";
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public FunGame()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        initializeGame();
        computer();
    }
    void initializeGame()
    {
        while (true)
        {
            Console.WriteLine("[Choose from the following options.]\nNew Game" +
            "\nLoad Game\n");
            string choice = Console.ReadLine()!;
            choice = choice.ToLower();
            if (choice.Contains("new"))
            {
                this.player = new Player();
                this.gear = new Shop(this.player);
                this.kelly = new Kelly();
                this.player.setName();
                intro();
                tutorial();
                break;
            }
            else if (choice.Contains("load"))
            {
                string json = File.ReadAllText(savePath);
                var data = JsonConvert.DeserializeObject<dynamic>(json);
                this.player = JsonConvert.DeserializeObject<Player>(Convert.ToString(data.Player));
                this.kelly = JsonConvert.DeserializeObject<Kelly>(Convert.ToString(data.Kelly));              
                this.gear = new Shop(this.player);
                break;
            }
            else
            {
                Console.WriteLine("[That was not an option.]\n");
                continue;
            }
        }
    }
    void intro()
    {
        Console.WriteLine("[You are an agent of a secret agency of your national government. You got in claiming" +
            " to be an expert in interrogations, but you don't actually have any experience. Your long-term assignment" +
            " will be to interrogate numerous members of a terrorist organization to obtain information" +
            " that will provide the whereabouts of the organization. Failure to do so will end in your...termination.]\n");
        Console.ReadKey(true);

        Console.WriteLine("[You walk into your new office at your agency. It is pretty barebones; there are two deskspaces" +
            " with a lamp and laptop on each desk. One desk is empty, but the other is occupied by a woman in standard agency" +
            " uniform.]\n");
        Console.ReadKey(true);

        if (Regex.IsMatch(this.player.playerName, @"[^a-zA-Z\s]"))
        {
            Console.WriteLine("[She stands up.]\nKelly: \"Ah! You must be the newcomer! I am Lieutenant Kelly Zhao.\"\n [She shakes your" +
                " hand.]\nKelly: \"Lieutenant " + this.player.playerName + ", right? I gotta say, having something other than just letters" +
                " for a name is strange.\"\n");
        }
        else
        {
            Console.WriteLine("[She stands up.]\nKelly: \"Ah! You must be the newcomer! I am Lieutenant Kelly Zhao.\"\n[She shakes your" +
            " hand.]\nKelly: \"Lieutenant " + this.player.playerName + ", right? Nice to meet you.\"\n");
        }
        Console.ReadKey(true);

        Console.WriteLine("Kelly: \"Although we are of the same rank, I will be your supervisor. It's only natural" +
            " considering I have done this for several years. I'll need to show you the ropes. Take a seat at the other desk.\"\n");
        Console.ReadKey(true);

        Console.WriteLine("[You sit down at your desk.]\n");
        Console.ReadKey(true);
    }

    void tutorial()
    {
        Console.WriteLine("[Your job will be to interrogate prisoners for information. Kelly was told you convince captives quickly. " +
            "The longer you take and the more painful you make it, the less she will like you. She's not much for blood. If she dislikes you enough, " +
            "you'll lose your job. As you become more successful, more tools will be available to you. Make interrogations fast. Good luck.]\n");
        Console.ReadKey(true);
    }

    void computer()
    {
        while (true)
        {
            Console.WriteLine("[Choose from the following options.]\nInterrogate     Talk to Kelly     Your Stats" +
            "\nBuy Gear        Inventory         Exit Game\n");
            string choice = Console.ReadLine()!;
            choice = choice.ToLower();
            if(choice == "interrogate")
            {
                interrogation = new Interrogation(new Interrogated(), this.player, this.kelly);
                interrogation.Interrogate();
                continue;
            }
            else if (choice.Contains("stats"))
            {
                Console.WriteLine("[You are level " + player.getLevel() + " and have " + this.player.getMoney() + " dollars.]\n");
                Console.ReadKey(true);
                continue;
            }
            else if (choice == "inventory")
            {
                while (true)
                {
                    try
                    {
                        this.player.getInventory();
                        Console.WriteLine("\n[select an item to read about, or type exit.]");
                        string selection = Console.ReadLine()!;
                        selection = selection.ToLower();
                        if (selection.Contains("exit"))
                        {
                            break;
                        }
                        else
                        {
                            string upper = char.ToUpper(selection[0]) + selection.Substring(1);
                            this.player.gearDescription(upper);
                            Console.ReadKey(true);
                            continue;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("[That wasn't a valid selection.]\n");
                        continue;
                    }
                }
            }
            else if(choice.Contains("talk") || choice.Contains("kelly")) 
            {
                this.kelly.speakToKelly();
                continue;
            }
            else if (choice.Contains("buy") || choice.Contains("gear")) 
            {
                this.gear.addToShop();
                this.gear.buyGear();
                continue;
            }
            else if (choice.Contains("exit")) 
            {
                var data = new
                {
                    Player = this.player,
                    Kelly = this.kelly
                };
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(savePath, json);
                Console.WriteLine("Goodbye.");
                Console.ReadKey(true);
                System.Environment.Exit(0);
            }
            else 
            {
                Console.WriteLine("[That was not an option.]");
                continue;
            }
        }
    }
}