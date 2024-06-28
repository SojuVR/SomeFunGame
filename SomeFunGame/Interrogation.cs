using System.Text.Json;

class Interrogation
{
    private Interrogated victim;
    private Player player;
    private Kelly kelly;
    private List<string> threaten { get; set; }
    private List<string> bribe { get; set; }
    private List<string> bribeSuccess { get; set; }
    private List<string> bribeFail { get; set; }
    private List<string> talk { get; set; }
    private List<string> talkSuccess { get; set; }
    private List<string> talkFail { get; set; }
    private string bossSuccess { get; set; }
    private string bossFail { get; set; }

    public Interrogation(Interrogated interrogated, Player player, Kelly kelly)
    {
        this.victim = interrogated;
        this.player = player;
        this.kelly = kelly;
        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\InterrogationLines\Lines.json");
        var jsonDocument = JsonDocument.Parse(jsonString);

        this.threaten = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("threaten").GetRawText())!;
        this.bribe = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("bribe").GetRawText())!;
        this.bribeSuccess = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("bribeSuccess").GetRawText())!;
        this.bribeFail = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("bribeFail").GetRawText())!;
        this.talk = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("talk").GetRawText())!;
        this.talkSuccess = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("talkSuccess").GetRawText())!;
        this.talkFail = JsonSerializer.Deserialize<List<string>>(jsonDocument.RootElement.GetProperty("talkFail").GetRawText())!;
    }

    public void Interrogate(int type)
    {
        double fatigue = this.player.getFatigue();
        bool shake = false;
        bool monster = false;
        Console.WriteLine("[Kelly brings in the prisoner and briefs you.]");
        this.victim.describeInterrogated();
        while (true)
        {
            Console.WriteLine("\n[Choose from the following options.]\nUse force     Talk to captive     Threaten" +
            "\nBribe         Status              Powerups\nGive up\n");
            string choice = Console.ReadLine()!;
            choice = choice.ToLower();
            if (choice == "threaten")
            {
                Console.WriteLine(this.player.playerName + this.victim.GetRandomAttributeString(this.threaten) + "\n" +
                    " [You threaten the captive.]");
                Console.ReadKey(true);
                Random random = new Random();
                if (monster == true) 
                {
                    this.victim.increaseFear(random.Next(5) * 2);
                }
                else if (monster == false)
                {
                    this.victim.increaseFear(random.Next(5));
                }
                this.kelly.addRep(-1);
                continue;
            }
            if (choice == "powerups")
            {
                try
                {
                    this.player.getPowerups();
                    Console.WriteLine("[Which would you like to use?]");
                    string drink = Console.ReadLine()!;
                    drink = drink.ToLower();
                    string drink2 = char.ToUpper(drink[0]) + drink.Substring(1);
                    if (!this.player.powerups.Contains(drink2))
                    {
                        throw new Exception();
                    }
                    if (drink2  == "Coffee")
                    {
                        this.player.setFatigue(0);
                        Console.WriteLine("\n[You drank a coffee. The effects will last this interrogation.]");
                        Console.ReadKey(true);
                        continue;
                    }
                    else if (drink2 == "Shake")
                    {
                        shake = true;
                        Console.WriteLine("\n[You drank a protein shake. The effects will last this interrogation.]");
                        Console.ReadKey(true);
                        continue;
                    }
                    else if (drink2 == "Monster")
                    {
                        monster = true;
                        Console.WriteLine("\n[You drank a Monster. The effects will last this interrogation.]");
                        Console.ReadKey(true);
                        continue;
                    }
                    this.player.removePowerups(drink2);
                    continue;
                }
                catch
                {
                    Console.WriteLine("[Your choice was invalid.]\n");
                    Console.ReadKey(true);
                    continue;
                }
            }
            else if (choice.Contains("give"))
            {
                Console.WriteLine("[You gave up and called it quits. You lost some respect from Kelly.]\n");
                Console.ReadKey(true);
                this.kelly.addRep(-20);
                this.player.setFatigue(fatigue);
                shake = false;
                monster = false;
                return;
            }
            else if (choice == "status")
            {
                this.victim.getStatus();
                continue;
            }
            else if (choice == "bribe")
            {
                Console.WriteLine(this.player.playerName + this.victim.GetRandomAttributeString(this.bribe) +
                    "\n" +
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
                            Console.WriteLine(this.victim.GetRandomAttributeString(this.bribeSuccess));
                            Console.ReadKey(true);
                            if (type == 1)
                            {
                                this.player.addMoney(10);
                                this.kelly.addRep(15);
                                this.player.levelUp(1);
                                Console.WriteLine("[You received 10 dollars and quite a bit of respect from Kelly.]\n");
                                Console.WriteLine("[You levelled up.]\n");
                                Console.ReadKey(true);
                                this.player.setFatigue(fatigue);
                                shake = false;
                                monster = false;
                                return;
                            }
                            else if (type == 2) 
                            {
                                this.player.addMoney(30);
                                this.kelly.addRep(20);
                                string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Bosses\Boss" + this.player.getLevel() + ".json");
                                var jsonDocument = JsonDocument.Parse(jsonString);

                                this.bossSuccess = JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("bossSuccess").GetRawText())!;
                                Console.WriteLine(this.bossSuccess);
                                Console.ReadKey(true);
                                Console.WriteLine("[You received 30 dollars and a lot of respect from Kelly.]\n");
                                Console.ReadKey(true);
                                this.player.addBossName();
                                this.player.addBossEvidence();
                                this.player.setFatigue(fatigue);
                                shake = false;
                                monster = false;
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine(this.victim.GetRandomAttributeString(this.bribeFail));
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
                try
                {
                    this.player.getInventory();
                    Console.WriteLine("[Which would you like to use?]");
                    string force = Console.ReadLine()!;
                    force = force.ToLower();
                    string force2 = char.ToUpper(force[0]) + force.Substring(1);
                    if (!this.player.inventory.Contains(force2))
                    {
                        throw new Exception();
                    }
                    Console.WriteLine("\nHead        Chest        Stomach\n" +
                        "Genitals     Arms         Legs");
                    Console.WriteLine("[Where would you like to use it?]");
                    string spot = Console.ReadLine()!;
                    spot = spot.ToLower();
                    int wound = this.player.inflictWound(force2, spot, shake);
                    this.victim.decreaseHealth(wound);
                    if (this.victim.getHealth() == 0)
                    {
                        if (type == 1)
                        {
                            Console.WriteLine("[The captive stopped breathing. You seemed to have killed the captive.]\n" +
                            "[You return to your workstation defeated. You get no money and lose much respect from Kelly.]\n");
                            this.kelly.addRep(-15);
                            Console.ReadKey(true);
                            this.player.setFatigue(fatigue);
                            shake = false;
                            monster = false;
                            return;
                        }
                        else if (type == 2)
                        {
                            Console.WriteLine("[The captive stopped breathing. You seemed to have killed the captive.]\n" +
                            "[You return to your workstation defeated. You get no money and lose much respect from Kelly.]\n");
                            Console.ReadKey(true);
                            string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Bosses\Boss" + this.player.getLevel() + ".json");
                            var jsonDocument = JsonDocument.Parse(jsonString);

                            this.bossFail = JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("bossFail").GetRawText())!;
                            Console.WriteLine(this.bossFail);
                            Console.ReadKey(true);
                            this.kelly.addRep(-12);
                            this.player.setFatigue(fatigue);
                            shake = false;
                            monster = false;
                            return;
                        }
                    }
                    int fear = this.player.inflictFear(force2, spot, monster);
                    this.victim.increaseFear(fear);
                    if (this.victim.getFear() == this.victim.getMaxFear())
                    {
                        if (type == 1)
                        {
                            Console.WriteLine("[The captive passed out. You won't be able to continue the interrogation.]\n" +
                            "[You return to your workstation defeated. You get no money and lose some respect from Kelly.]\n");
                            this.kelly.addRep(-10);
                            Console.ReadKey(true);
                            this.player.setFatigue(fatigue);
                            shake = false;
                            monster = false;
                            return;
                        }
                        else if (type == 2)
                        {
                            Console.WriteLine("[The captive passed out. You won't be able to continue the interrogation.]\n" +
                            "[You return to your workstation defeated. You get no money and lose some respect from Kelly.]\n");
                            this.kelly.addRep(-10);
                            Console.ReadKey(true);
                            string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Bosses\Boss" + this.player.getLevel() + ".json");
                            var jsonDocument = JsonDocument.Parse(jsonString);

                            this.bossFail = JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("bossFail").GetRawText())!;
                            Console.WriteLine(this.bossFail);
                            Console.ReadKey(true);
                            this.player.setFatigue(fatigue);
                            shake = false;
                            monster = false;
                            return;
                        }
                    }
                    int rep = this.player.affectKelly(force2);
                    this.kelly.addRep(rep);
                    continue;
                }
                catch
                {
                    Console.WriteLine("[Your choice of equipment and/or location was invalid.]\n");
                    Console.ReadKey(true);
                    continue;
                }
            }
            else if (choice.Contains("talk") || choice.Contains("captive"))
            {
                Console.WriteLine(this.player.playerName + this.victim.GetRandomAttributeString(this.talk));
                Console.ReadKey(true);
                bool result = this.victim.infoAttempt();
                if (result == true)
                {
                    Console.WriteLine(this.victim.GetRandomAttributeString(this.talkSuccess));
                    Console.ReadKey(true);
                    if (type == 1)
                    {
                        this.player.addMoney(10);
                        this.kelly.addRep(10);
                        this.player.levelUp(1);
                        Console.WriteLine("[You received 10 dollars and some respect from Kelly.]\n");
                        Console.WriteLine("[You levelled up.]\n");
                        Console.ReadKey(true);
                        this.player.setFatigue(fatigue);
                        shake = false;
                        monster = false;
                        return;
                    }
                    else if (type == 2)
                    {
                        this.player.addMoney(30);
                        this.kelly.addRep(15);
                        string jsonString = File.ReadAllText(@"C:\Users\emman\source\repos\SomeFunGame\SomeFunGame\Bosses\Boss" + this.player.getLevel() + ".json");
                        var jsonDocument = JsonDocument.Parse(jsonString);

                        this.bossSuccess = JsonSerializer.Deserialize<string>(jsonDocument.RootElement.GetProperty("bossSuccess").GetRawText())!;
                        Console.WriteLine(this.bossSuccess);
                        Console.ReadKey(true);
                        Console.WriteLine("[You received 30 dollars and quite a bit of respect from Kelly.]\n");
                        Console.ReadKey(true);
                        this.player.addBossName();
                        this.player.addBossEvidence();
                        this.player.setFatigue(fatigue);
                        shake = false;
                        monster = false;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(this.victim.GetRandomAttributeString(this.talkFail));
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