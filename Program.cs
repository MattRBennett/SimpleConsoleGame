using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using static ConsoleGame;

class ConsoleGame
{
    #region Player
    public enum Speciality
    {
        Unspecified = 0,
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
        public Speciality PlayersSpeciality { get; set; } = Speciality.Unspecified;
        public List<PlayerInventory> playerInventory { get; set; } = new List<PlayerInventory>();

        public void SetPlayerName(string name)
        {
            Name = name;
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

        public void SetSpeciality(Speciality speciality)
        {
            Console.Clear();

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

    #endregion

    #region Monsters 

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
            Random rand = new Random();
            Array monsterArray = Enum.GetValues(typeof(MonsterType));

            monster.monstertype = (MonsterType)monsterArray.GetValue(rand.Next(monsterArray.Length));
            monster.Health = rand.Next(12, 20);
            monster.Defense = rand.Next(1, 10);
            monster.Strength = rand.Next(1, 10);

            Console.WriteLine("A monster appears!");
            Thread.Sleep(1000);

            return monster;
        }

        public void MonsterStats()
        {
            Console.WriteLine($"Monsters is of type {monstertype}, health is {Health}, strength is {Strength} and a defense of {Defense}.");
            Thread.Sleep(2000);
        }

        public void MonsterAttack(Monster monster, Player player)
        {
            if (monster.Health > 0)
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
    }

    #endregion

    #region Potions
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

            if (WillPotionDrop > 55)
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
            if (player.playerInventory.Count > 0)
            {
                foreach (var item in player.playerInventory)
                {
                    Console.WriteLine($"{item.Id} - {item.ItemName}");
                }

                Console.WriteLine("Which potion do you want to use?");
                int ChosenItem = int.Parse(Console.ReadLine());

                while (ChosenItem > 0 && ChosenItem <= player.playerInventory.Count)
                {
                    string NameOfItem = player.playerInventory.FirstOrDefault(x => x.Id == ChosenItem).ItemName;
                    Console.WriteLine($"You have chosen to use the {NameOfItem} potion");
                    Thread.Sleep(1000);
                    player.UseItem(ChosenItem);
                    break;
                }
            }
            else
            {
                Console.WriteLine("Your inventory is empty!");
            }
        }
    }

    #endregion

    #region Controller
    public class GameController
    {
        private readonly Player _player;
        private readonly Monster _monster;
        private readonly Potions _potions;

        private int Rounds = 1;

        private bool ExitTheGame = false;

        public GameController(Monster monster, Player player, Potions potions)
        {
            _monster = monster;
            _player = player;
            _potions = potions;
        }

        public void StartGame()
        {
            while (true)
            {
                SetPlayerName();
                SetPlayerClass();
                GameRounds();

                Thread.Sleep(3000);
                break;
            }
        }

        public void SetPlayerName()
        {
            string playersname = string.Empty;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the game, what is your name?");
                playersname = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(playersname))
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid name!");
                    Thread.Sleep(1000);
                }
            }

            _player.Name = playersname;
        }

        public void SetPlayerClass()
        {
            string[] ClassArray = { "rogue", "knight", "archer" };

            while (true)
            {
                Console.WriteLine($"{_player.Name}, What class would you like to play? Type 'Rogue', 'Knight', or 'Archer' to continue");

                string playersclass = Console.ReadLine().ToLower();

                if (ClassArray.Contains(playersclass))
                {
                    switch (playersclass)
                    {
                        case "rogue":
                            _player.SetSpeciality(Speciality.Rogue);
                            break;
                        case "knight":
                            _player.SetSpeciality(Speciality.Knight);
                            break;
                        case "archer":
                            _player.SetSpeciality(Speciality.Archer);
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again!");
                            break;
                    }

                    Console.WriteLine($"You have chosen {_player.PlayersSpeciality}!");
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }

        public void GameRounds()
        {
            while (Rounds < 11)
            {
                Console.WriteLine($"GET READY! - Round {Rounds}");
                Thread.Sleep(1000);

                _monster.GenerateMonster(_monster);
                _monster.MonsterStats();

                while (_monster.Health > 0 && _player.Health > 0)
                {
                    GameCommands();
                    _monster.MonsterAttack(_monster, _player);
                }
                EndRoundOptions();

                ExitGame();
            }
        }

        public void EndRoundOptions()
        {
            string[] EndOptionsArray = { "restart", "exit", "continue" };

            while (true)
            {
                if (Rounds == 10 && _player.Health > 0)
                {
                    GameWon();
                }
                else
                {
                    if (Rounds == 10 && _player.Health < 1)
                    {
                        GameDeath();
                    }
                    else
                    {
                        ContinueGame();
                    }
                }

                string PlayerInput = Console.ReadLine().ToLower();

                if (EndOptionsArray.Contains(PlayerInput))
                {
                    if (PlayerInput == "restart")
                    {
                        RestartGame();
                        break;
                    }
                    else if (PlayerInput == "exit")
                    {
                        ExitTheGame = true;
                        break;
                    }
                    else if (PlayerInput == "continue")
                    {
                        if (Rounds == 10)
                        {
                            Console.WriteLine("The game has been completed, please choose another option.");
                        }
                        else
                        {
                            _potions.PotionsDrop(_player, _monster);
                            Rounds++;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again!");
                }
            }
        }

        public void GameWon()
        {
            Console.WriteLine($"Congratulations {_player.Name}, you have won! What would you like to do next champion? Type 'Exit' or 'Restart'.");
        }

        public void GameDeath()
        {
            Console.WriteLine("YOU DIED!!");
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("What would you like to do next? Type 'Exit' or 'Restart'.");
        }

        public void RestartGame()
        {
            _player.Health = 30;
            _player.Strength = 0;
            _player.Defense = 0;
            _player.PlayersSpeciality = Speciality.Unspecified;
            _player.playerInventory.Clear();

            Rounds = 1;

            Console.WriteLine("The game has been restarted!");
            Thread.Sleep(2000);
            Console.Clear();
        }

        public void ExitGame()
        {
            if (ExitTheGame)
            {
                Console.WriteLine("Goodbye!");
                Thread.Sleep(2000);
                Rounds = 10;
            }
        }

        public void ContinueGame()
        {
            Console.WriteLine("You have defeated the monster! Ready for the next round? Type 'Continue', 'Restart' or 'Exit'.");
        }

        public void GameCommands()
        {

            string[] CommandsArray = { "run", "attack", "usepotion", "health", "monster" };

            while (true)
            {
                Console.WriteLine("What do you do? 'Run', 'Attack', or 'UsePotion'?");

                var InputtedCommand = Console.ReadLine().ToLower();

                if (CommandsArray.Contains(InputtedCommand))
                {
                    switch (InputtedCommand)
                    {
                        case "run":
                            _player.PlayerRun(_player, _monster);
                            break;
                        case "attack":
                            _player.PlayerAttack(_player, _monster);
                            break;
                        case "usepotion":
                            _potions.UsePotion(_player);
                            break;
                        case "health":
                            _player.CheckHealth();
                            GameCommands();
                            break;
                        case "monster":
                            _monster.MonsterStats();
                            GameCommands();
                            break;
                        default:
                            Console.Write("Command not recognised!");
                            break;
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("Inputted command does not exist!");
                }
            }
        }

    }
    #endregion

    static void Main(string[] args)
    {
        Monster monster = new();
        Player player = new();
        Random random = new();
        Potions potions = new();
        GameController controller = new GameController(monster, player, potions);

        controller.StartGame();
        
    }
}