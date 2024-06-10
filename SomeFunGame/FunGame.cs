using System.Text.RegularExpressions;

class FunGame
{
    private Player player;
    private Kelly kelly;
    private Interrogated interrogated;
    private Gear gear;
    private Interrogation interrogation;
    public FunGame()
    {
        gear = new Gear();
        player = new Player();
        kelly = new Kelly();
        runGame();
    }
    void runGame()
    {
        player.setName();
        intro();
        tutorial();
        computer();
    }
    void intro()
    {
        Console.WriteLine("[You are an agent of a secret agency of your national government. You got in claiming" +
            " to be an expert in interrogations. Now it is time to prove your worth. Your long-term assignment" +
            " will be to interrogate numerous members of a terrorist organization to obtain information" +
            " that will provide the whereabouts of the organization. Failure to do so will end in your...termination.]\n");
        player.nextDialogue();

        Console.WriteLine("[You walk into your new office at your agency. It is pretty barebones; there are two deskspaces" +
            " with a lamp and laptop on each desk. One desk is empty, but the other is occupied by a woman in standard agency" +
            " uniform.]\n");
        player.nextDialogue();

        if (Regex.IsMatch(player.playerName, @"[^a-zA-Z\s]"))
        {
            Console.WriteLine("[She stands up.]\nKelly: \"Ah! You must be the newcomer! I am Lieutenant Kelly Zhao.\"\n [She shakes your" +
                " hand.]\nKelly: \"Lieutenant " + player.playerName + ", right? I gotta say, having something other than just letters" +
                " for a name is strange.\"\n");
        }
        else
        {
            Console.WriteLine("[She stands up.]\nKelly: \"Ah! You must be the newcomer! I am Lieutenant Kelly Zhao.\"\n[She shakes your" +
            " hand.]\nKelly: \"Lieutenant " + player.playerName + ", right? Nice to meet you.\"\n");
        }
        player.nextDialogue();
        
        Console.WriteLine("Kelly: \"Although we are of the same rank, I will be your supervisor. It's only natural" +
            " considering I have done this for several years. I'll need to show you the ropes. Take a seat at the other desk.\"\n");
        player.nextDialogue();

        Console.WriteLine("[You sit down at your desk.]\n");
        player.nextDialogue();
    }

    void tutorial()
    {
        Console.WriteLine("[I don't feel like writing the tutorial right now. I will write it when I'm done.]\n");
        player.nextDialogue();
    }

    void computer()
    {
        while (true)
        {
            Console.WriteLine("[Choose from the following options.]\nInterrogate     Talk to Kelly     Your Stats" +
            "\nBuy Gear        Inventory         Exit Game\n");
            string choice = Console.ReadLine();
            choice = choice.ToLower();
            if(choice == "interrogate")
            {
                interrogation = new Interrogation();
                interrogation.Interrogate();
                continue;
            }
            else if (choice.Contains("stats"))
            {
                Console.WriteLine("[You are level " + player.getLevel() + " and have " + player.getMoney() + " dollars.]\n");
                player.nextDialogue();
                continue;
            }
            else if (choice == "inventory")
            {
                player.getInventory();
                player.nextDialogue();
                continue;
            }
            else if(choice.Contains("talk") || choice.Contains("kelly")) 
            {
                kelly.speakToKelly();
                continue;
            }
            else if (choice.Contains("buy") || choice.Contains("gear")) 
            {
                string item = gear.buyGear();
                player.addToInventory(item);
                continue;
            }
            else if (choice.Contains("exit")) 
            {
                Console.WriteLine("Goodbye.");
                player.nextDialogue();
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