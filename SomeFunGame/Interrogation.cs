class Interrogation
{
    private Interrogated victim;
    private Player player;
    private Kelly kelly;

    public Interrogation(Interrogated interrogated, Player player, Kelly kelly)
    {
        this.victim = interrogated;
        this.player = player;
        this.kelly = kelly;
    }

    public void Interrogate()
    {
        Console.WriteLine("[Kelly brings in the prisoner and briefs you.]");
        this.victim.describeInterrogated();
        while (true)
        {
            Console.WriteLine("\n[Choose from the following options.]\nUse force     Talk to captive     Threaten" +
            "\nBribe         Give up\n");
            string choice = Console.ReadLine()!;
            choice = choice.ToLower();
            if (choice == "threaten")
            {
                Console.WriteLine(this.player.playerName + ": \"You like your family? I sure do.\"\n" +
                    " [You threaten the captive.]");
                Console.ReadKey(true);
                Random random = new Random();
                this.victim.increaseFear(random.Next(5));
                this.kelly.addRep(-1);
                continue;
            }
            else if (choice.Contains("give"))
            {
                Console.WriteLine("[You gave up and called it quits. You lost some respect from Kelly.]\n");
                Console.ReadKey(true);
                this.kelly.addRep(-10);
                return;
            }
            else if (choice == "bribe")
            {
                Console.WriteLine(this.player.playerName + ": \"Maybe you need a little convincing for when we set you free...\"\n" +
                    " [How much money will you offer? You have " + this.player.getMoney() + " dollars.]");
                string amnt = Console.ReadLine()!;
                try
                {
                    int give = int.Parse(amnt);
                    if (give <= this.player.getMoney() && give >= 0)
                    {
                        this.player.subtractMoney(give);
                        Random random = new Random();
                        float chance = give / 2;
                        if (chance > 50)
                        {
                            chance = 50;
                        }
                        int num = random.Next(100);
                        if (num < chance)
                        {
                            Console.WriteLine("Captive:\"Well hell yeah I'll tell ya!\"");
                            Console.ReadKey(true);
                            this.player.addMoney(10);
                            this.kelly.addRep(15);
                            Console.WriteLine("[You received 10 dollars and quite a bit of respect from Kelly.]\n");
                            Console.ReadKey(true);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Captive:\"Fuck you, cheap bitch!\"");
                            Console.ReadKey(true);
                        }
                    }
                    else
                    {
                        Console.WriteLine("[You don't have that much money.]");
                        Console.ReadKey(true);
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("[That is not a valid amount.]");
                    continue;
                }
            }
            else if (choice.Contains("use") || choice.Contains("force"))
            {
                this.player.getInventory();
                Console.WriteLine("[Which would you like to use?]");
                string force = Console.ReadLine()!;
                force = force.ToLower();
                string force2 = char.ToUpper(force[0]) + force.Substring(1);
                Console.WriteLine("\nHead        Chest        Stomach\n" +
                    "Genitals     Arms         Legs");
                Console.WriteLine("[Where would you like to use it?]");
                string spot = Console.ReadLine()!;
                spot = spot.ToLower();
                int wound = this.player.inflictWound(force2, spot);
                this.victim.decreaseHealth(wound);
                if (this.victim.getHealth() == 0)
                {
                    Console.WriteLine("[The captive stopped breathing. You seemed to have killed the captive.]\n" +
                        "[You return to your workstation defeated. You get no money and lose much respect from Kelly.]\n");
                    this.kelly.addRep(-15);
                    return;
                }
                int fear = this.player.inflictFear(force2, spot);
                this.victim.increaseFear(fear);
                if (this.victim.getFear() == this.victim.getMaxFear())
                {
                    Console.WriteLine("[The captive passed out. You won't be able to continue the interrogation.]\n" +
                        "[You return to your workstation defeated. You get no money and lose some respect from Kelly.]\n");
                    this.kelly.addRep(-5);
                    return;
                }
                int rep = this.player.affectKelly(force2);
                this.kelly.addRep(rep);
                continue;
            }
            else if (choice.Contains("talk") || choice.Contains("captive"))
            {
                Console.WriteLine(this.player.playerName + ": \"Tell me what I need to know, NOW!\"");
                Console.ReadKey(true);
                bool result = this.victim.infoAttempt();
                if (result == true)
                {
                    Console.WriteLine("Captive:\"OK, OK!! I'LL SPILL!!\"");
                    Console.ReadKey(true);
                    this.player.addMoney(10);
                    this.kelly.addRep(10);
                    Console.WriteLine("[You received 10 dollars and some respect from Kelly.]\n");
                    Console.ReadKey(true);
                    return;
                }
                else
                {
                    Console.WriteLine("Captive:\"Fuck you!\"");
                    Console.ReadKey(true);
                    this.victim.decreaseFear(1);
                    this.victim.increaseHealth(1);
                    continue;
                }
            }
            else
            {
                Console.WriteLine("[That was not an option.]");
                continue;
            }
        }
    }
}