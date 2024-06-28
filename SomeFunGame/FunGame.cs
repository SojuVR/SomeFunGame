using System.Text.RegularExpressions;
using Newtonsoft.Json;

class FunGame
{
    private Player player;
    private Kelly kelly;
    private Shop gear;
    private Interrogation interrogation;
    private Script script;
    private Home home;
    private Cafe cafe;
    private bool boss;
    private int time = 1;
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
                File.WriteAllText(savePath, "{}");
                this.player = new Player();
                this.gear = new Shop(this.player);
                this.kelly = new Kelly();
                this.player.setName();
                this.script = new Script(this.kelly, this.player.playerName);
                this.home = new Home(this.player);
                this.cafe = new Cafe(this.player);
                intro();
                tutorial();
                break;
            }
            else if (choice.Contains("load"))
            {
                if (File.ReadAllText(savePath) == "{}")
                {
                    Console.WriteLine("[There is no game data to load.]\n");
                    continue;
                }
                string json = File.ReadAllText(savePath);
                var data = JsonConvert.DeserializeObject<dynamic>(json)!;
                this.player = JsonConvert.DeserializeObject<Player>(Convert.ToString(data.Player));
                this.kelly = JsonConvert.DeserializeObject<Kelly>(Convert.ToString(data.Kelly));
                this.gear = new Shop(this.player);
                this.script = new Script(this.kelly, this.player.playerName);
                this.script.finishedLevels = JsonConvert.DeserializeObject<HashSet<int>>(Convert.ToString(data.Script));
                this.gear.shop = JsonConvert.DeserializeObject<Dictionary<string, int>>(Convert.ToString(data.Shop));
                this.home = new Home(this.player);
                this.cafe = new Cafe(this.player);
                this.home.utility = JsonConvert.DeserializeObject<int>(Convert.ToString(data.Home));
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
        Console.WriteLine("[You are a newly-hired member of the Central Intelligence Agency of New America. You got in claiming" +
            " to be an expert in interrogations, but you don't actually have any experience....you just really needed a job." +
            " For some reason, you thought this would be an easy job to fake. Your long-term assignment" +
            " will be to interrogate numerous members of a terrorist organization called Antimerica to obtain information" +
            " that will provide the whereabouts of this organization. Failure to do so will end in your...termination.]\n");
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
            if (this.kelly.checkFail() == true)
            {
                File.WriteAllText(savePath, "{}");
                break;
            }
            boss = this.script.loadScene(this.player.getLevel());
            if (boss == true)
            {
                interrogation = new Interrogation(new Interrogated(2, this.player.getLevel()), this.player, this.kelly);
                interrogation.Interrogate(2);
                boss = false;
                time += 1;
            }
            if (time > 5)
            {
                time = 1;
                bool result = this.player.addFatigue();
                if (result == true)
                {
                    float lostMoney = (this.player.getMoney() * 3) / 4;
                    int lostMoneyInt = (int)lostMoney;
                    Console.WriteLine("[You collapse from exhaustion. A hospital bills you " + lostMoneyInt + " dollars.]");
                    this.player.subtractMoney(lostMoneyInt);
                    this.player.setFatigue(0);
                }
                this.player.subtractMoney(this.home.utility);
            }
            currentTime();
            Console.WriteLine("[Choose from the following options.]\nInterrogate     Talk to Kelly     Your Stats" +
            "\nBuy Gear        Inventory         Hallway\nExit Game\n");
            string choice = Console.ReadLine()!;
            choice = choice.ToLower();
            if(choice == "interrogate")
            {
                interrogation = new Interrogation(new Interrogated(1), this.player, this.kelly);
                interrogation.Interrogate(1);
                time += 1;
                continue;
            }
            else if (choice.Contains("stats"))
            {
                Console.WriteLine("[You are level " + player.getLevel() + " and have " + this.player.getMoney() + " dollars.]");
                this.player.status();
                Console.ReadKey(true);
                continue;
            }
            else if (choice == "hallway")
            {
                while (true)
                {
                    currentTime();
                    Console.WriteLine("[Choose from the following options.]\nCellblock     Evidence Room     Cafe" +
            "\nYour Office   Go Home");
                    string room = Console.ReadLine()!;
                    room = room.ToLower();
                    if (room.Contains("office") || room.Contains("your"))
                    {
                        break;
                    }
                    else if (room == "cellblock")
                    {
                        this.player.displayNames();
                        continue;
                    }
                    else if (room.Contains("evidence"))
                    {
                        this.player.displayEvidence();
                        continue;
                    }
                    else if (room == "cafe")
                    {
                        this.cafe.buyDrink();
                        Console.WriteLine("[You spend some time relaxing at the cafe.]\n");
                        time += 1;
                        continue;
                    }
                    else if (room.Contains("go") || room.Contains("home"))
                    {
                        this.home.sleep();
                        time = 1;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n[That was not an option.]");
                        continue;
                    }
                }
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
                int rel = this.kelly.speakToKelly();
                this.script.loadScene(rel);
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
                    Kelly = this.kelly,
                    Script = this.script.finishedLevels,
                    Shop = this.gear.shop,
                    Home = this.home.utility,
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

    public void currentTime()
    {
        switch (time)
        {
            case 1:
                Console.WriteLine("Time: Morning");
                break;
            case 2:
                Console.WriteLine("Time: Noon");
                break;
            case 3:
                Console.WriteLine("Time: Afternoon");
                break;
            case 4:
                Console.WriteLine("Time: Evening");
                break;
            case 5:
                Console.WriteLine("Time: Night");
                break;
        }
    }
}