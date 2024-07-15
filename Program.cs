using System;
using System.ComponentModel;
using static Program;

class Program
{
    public enum Speciality
    {
        Rogue = 1,
        Knight = 2,
        Archer = 3
    }

    public class PlayerInventory
    {
        public int Id { get; set; } = 0;
        public string ItemName { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
    }
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; } = 30;
        public int Strength { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public Speciality PlayersSpeciality { get; set; } = Speciality.Rogue;
        public List<PlayerInventory> playerInventory { get; set; } = new List<PlayerInventory>();

        public void SetPlayerName(string name)
        {
            Name = name;
        }

        public void SetSpeciality(Speciality speciality)
        {
            PlayersSpeciality = speciality;

            switch (speciality)
            {
                case Speciality.Rogue:
                    Strength = 7;
                    Defense = 4;
                    break;
                case Speciality.Knight:
                    Strength = 10;
                    Defense = 8;
                    break;
                case Speciality.Archer:
                    Strength = 8;
                    Defense = 2;
                    break;
                default:
                    throw new Exception("Speciality unknown.");
            }
        }

        public void AddItem(string ItemName, string ItemType)
        {
            int TotalItems = playerInventory.Count + 1;

            PlayerInventory NewItem = new PlayerInventory
            {
                Id = TotalItems,
                ItemName = ItemName,
                ItemType = ItemType
            };

            playerInventory.Add(NewItem);

            Console.WriteLine($"{ItemName} has been added to your inventory!");
        }

        public void UseItem(int Id)
        {
            var ItemInInventory = playerInventory.FirstOrDefault(x => x.Id == Id);

            if (ItemInInventory is not null)
            {
                if (ItemInInventory.ItemType == "Potion")
                {
                    switch (ItemInInventory.ItemName)
                    {
                        case "Health":
                            Health = 30;
                            Console.WriteLine("Health has been restored!");
                            Thread.Sleep(3000);
                            break;
                        case "Strength":
                            Strength += 5;
                            Console.WriteLine("Strengh has been inreased by 5!");
                            Thread.Sleep(3000);
                            break;
                        case "Defense":
                            Defense += 5;
                            Console.WriteLine("Defense has been increased by 5!");
                            Thread.Sleep(3000);
                            break;
                        default:
                            Console.WriteLine("Cannot use item!");
                            Thread.Sleep(3000);
                            break;
                    }

                    playerInventory.Remove(ItemInInventory);
                }
                else
                {
                    Console.WriteLine("Unknown item!");
                }
            }
            else
            {
                Console.WriteLine("Cannot find item in inventory!");
            }
        }

        public bool EscapeChance()
        {
            Random random = new Random();
            int CanPlayerEscape = random.Next(1, 100);

            if (CanPlayerEscape > 75)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AttackChance()
        {
            Random random = new Random();
            int DidPlayerDealDamage = random.Next(1, 100);

            if (DidPlayerDealDamage > 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayPlayerStats()
        {
            Console.WriteLine($"Name: {Name}");
            Thread.Sleep(1000);
            Console.WriteLine($"Class: {PlayersSpeciality}");
            Thread.Sleep(1000);
            Console.WriteLine($"Health: {Health}");
            Thread.Sleep(1000);
            Console.WriteLine($"Strength: {Strength}");
            Thread.Sleep(1000);
            Console.WriteLine($"Defense: {Defense}");
        }

        public void CheckHealth()
        {
            Console.WriteLine($"Health: {Health}");
        }

        public void ChooseClass(Player player, string ClassName)
        {
            switch (ClassName)
            {
                case "rogue":
                    player.SetSpeciality(Speciality.Rogue);
                    break;
                case "knight":
                    player.SetSpeciality(Speciality.Knight);
                    break;
                case "archer":
                    player.SetSpeciality(Speciality.Archer);
                    break;
                default:
                    Console.WriteLine("Invalid input, please try again!");
                    break;
            }

            Console.WriteLine($"You have chosen {player.PlayersSpeciality}!");
            Thread.Sleep(2000);
        }

        public void PlayerAttack(Player player, Monster monster)
        {
            int MonstersOriginalHealth = monster.Health;

            bool DidPlayerDamageTarget = player.AttackChance();

            if (DidPlayerDamageTarget == true)
            {
                int DamageGiven = monster.Defense - player.Strength;

                if (DamageGiven < 0)
                {
                    monster.Health += DamageGiven;

                    int FinalCalculation = MonstersOriginalHealth -= monster.Health;

                    Console.WriteLine($"You dealt the {monster.monstertype} {FinalCalculation} damage and is now at {monster.Health} health.");

                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("The attack was ineffective, and no damage was inflicted.");
                }
            }
            else
            {
                Console.WriteLine("You missed!");
            }
        }

        public bool PlayerRun(Player player, Monster monster)
        {
            Random random = new Random();

            int PlayersOriginalHealth = player.Health;
            bool WillPlayerEscape = player.EscapeChance();

            if (WillPlayerEscape == true)
            {
                Console.WriteLine("You have escaped!");
                monster.Health = 0;
                Thread.Sleep(2000);
                return true;
            }
            else
            {
                int PenaltyDamage = random.Next(1, monster.Strength);
                player.Health -= PenaltyDamage;
                int FinalCalculation = PlayersOriginalHealth -= player.Health;
                Console.WriteLine($"The {monster.monstertype} managed to stop you from escaping and dealt {FinalCalculation} damage to you! You now have {player.Health} health left.");
                return false;
            }
        }
    }

    public enum MonsterType
    {
        Bat = 1,
        Werewolf = 2,
        Goblin = 3,
        Vampire = 4,
        Ghoul = 5,
        Troll = 6
    }

    public class Monster
    {
        public MonsterType monstertype { get; set; } = MonsterType.Bat;
        public int Health { get; set; } = 0;
        public int Strength { get; set; } = 0;
        public int Defense { get; set; } = 0;

        public Monster GenerateMonster(Monster monster)
        {
            //Monster monster = new Monster();
            Random rand = new Random();
            Array monsterArray = Enum.GetValues(typeof(MonsterType));

            monster.monstertype = (MonsterType)monsterArray.GetValue(rand.Next(monsterArray.Length));
            monster.Health = rand.Next(12, 20);
            monster.Defense = rand.Next(1, 10);
            monster.Strength = rand.Next(1, 10);

            return monster;
        }

        public void MonsterStats()
        {
            Console.WriteLine($"Monsters type is {monstertype}");
            Thread.Sleep(1000);
            Console.WriteLine($"Monsters health is {Health}");
            Thread.Sleep(1000);
            Console.WriteLine($"Monsters strength is {Strength}");
            Thread.Sleep(1000);
            Console.WriteLine($"Monsters defense is {Defense}");
        }

        public void MonsterAttack(Monster monster, Player player)
        {
            int PlayersOriginalHealth = player.Health;

            int DamageTaken = player.Defense - monster.Strength;

            if (DamageTaken < 0)
            {
                player.Health += DamageTaken;
                int FinalCalculation = PlayersOriginalHealth -= player.Health;

                Console.WriteLine($"The {monster.monstertype} has chosen to attack and has dealt {FinalCalculation} damage to you.");
                Console.WriteLine($"Your health is at {player.Health} points!");
                Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine($"The {monster.monstertype} tried to attack you but it was ineffective, and you took no damage.");
            }
        }
    }

    public enum PotionType
    {
        Health = 1,
        Strength = 2,
        Defense = 3
    }

    public class Potions
    {
        public PotionType potionType { get; set; } = PotionType.Health;

        public PotionType GeneratePotion()
        {
            Random rand = new Random();

            Array PotionArray = Enum.GetValues(typeof(PotionType));

            return (PotionType)PotionArray.GetValue(rand.Next(PotionArray.Length));
        }

        public bool PotionDropChance()
        {
            Random random = new Random();
            int WillPotionDrop = random.Next(1, 100);

            if (WillPotionDrop > 65)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PotionsDrop(Player player, Monster monster)
        {
            Potions potion = new Potions();
            bool DoesPotionDrop = potion.PotionDropChance();
            if (DoesPotionDrop == true)
            {
                var DroppedPotion = potion.GeneratePotion();
                player.AddItem(DroppedPotion.ToString(), "Potion");
                Console.WriteLine($"The {monster.monstertype} dropped a potion if {potion.potionType}! It has been added to your inventory.");
            }
            else
            {
                Console.WriteLine($"The {monster.monstertype} did not drop anything.");
            }
        }

        public void UsePotion(Player player)
        {
            if (player.playerInventory != null)
            {
                foreach (var item in player.playerInventory)
                {
                    Console.WriteLine($"{item.Id} - {item.ItemName}");
                }

                Console.WriteLine("Which potion do you want to use?");
                int ChosenItem = 0;
                ChosenItem = int.Parse(Console.ReadLine());

                while (ChosenItem > 0 && ChosenItem > player.playerInventory.Count)
                {
                    string NameOfItem = player.playerInventory.FirstOrDefault(x => x.Id == ChosenItem).ItemName;
                    Console.WriteLine($"You have chosen to use the {NameOfItem} potion");
                    Thread.Sleep(1000);
                    player.UseItem(ChosenItem);
                }
            }
            else
            {
                Console.WriteLine("Your inventory is empty!");
            }
        }
    }

    static void Main(string[] args)
    {
        Player player = new();
        Random random = new();
        Potions potions = new();
        Monster monster = new();


        int Rounds = 1;

        while (true)
        {
            Console.WriteLine("Welcome to the game, what is your name?");
            var playersname = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(playersname))
            {
                player.SetPlayerName(playersname);

                while (true)
                {
                    Console.WriteLine($"{player.Name}, What class would you like to play? Type 'Rogue', 'Knight', or 'Archer' to continue");

                    var playersclass = Console.ReadLine().ToLower();

                    if (playersclass == "rogue" || playersclass == "knight" || playersclass == "archer")
                    {
                        player.ChooseClass(player, playersclass);

                        while (Rounds < 11)
                        {
                            Console.Clear();

                            Console.WriteLine($"GET READY! - Round {Rounds}");
                            Thread.Sleep(1000);
                            monster.GenerateMonster(monster);

                            Console.WriteLine("A monster appears!");
                            Thread.Sleep(1000);

                            monster.MonsterStats();

                            while (monster.Health > 0 && player.Health > 0)
                            {
                                Console.WriteLine("What do you do? 'Run', 'Attack', or 'UsePotion'?");
                                var FightInput = Console.ReadLine().ToLower();

                                if (FightInput == "run" || FightInput == "usepotion" || FightInput == "attack")
                                {
                                    if (FightInput == "run")
                                    {
                                        bool Outcome = player.PlayerRun(player, monster);

                                        if (Outcome)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else if (FightInput == "attack")
                                    {
                                        player.PlayerAttack(player, monster);
                                    }
                                    else if (FightInput == "usepotion")
                                    {
                                        potions.UsePotion(player);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unknown command! Please try again!");
                                    }


                                    if (monster.Health > 0)
                                    {
                                        monster.MonsterAttack(monster, player);
                                    }
                                    else
                                    {
                                        if (monster.Health < 1)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"The {monster.monstertype} was slain!!!");
                                            Thread.Sleep(2000);
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }

                                    if (player.Health < 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("YOU DIED!");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }


                                }
                            }

                            Console.Clear();
                            //Thread.Sleep(1000);

                            if (Rounds < 10)
                            {
                                if (player.Health < 1)
                                {
                                    break;
                                }
                                else if (monster.Health < 1)
                                {
                                    Console.WriteLine($"Ready for round {Rounds + 1}? Type 'yes' to continue.");

                                    var input = Console.ReadLine().ToLower();
                                    if (input == "yes")
                                    {
                                        potions.PotionsDrop(player, monster);
                                        Rounds++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Goodbye!");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Congratulations! You have completed the game, until we meet again warrior!");
                                    Thread.Sleep(2000);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please try again!");
                    }
                }
                break;
            }
            else
            {
                Console.WriteLine("Invalid Name!");
            }
        }
    }
}