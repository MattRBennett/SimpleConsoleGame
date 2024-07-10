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
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; } = 20;
        public int Strength { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public Speciality PlayersSpeciality {  get; set; } = Speciality.Rogue;

        public void CreatePlayer (string name)
        {
            Name = name;
        }

        public void SetSpeciality (Speciality speciality)
        {
            PlayersSpeciality = speciality;

            switch (speciality)
            {

                case Speciality.Rogue:
                    Strength = 7;
                    Defense = 6;
                    break;
                case Speciality.Knight:
                    Strength = 10;
                    Defense = 8;
                    break;
                case Speciality.Archer:
                    Strength = 7;
                    Defense = 4;
                    break;
                default:
                    throw new Exception("Speciality unknown.");
            }

            Console.WriteLine($"{speciality} Selected!");
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

        public Monster GenerateMonster()
        {
            Monster monster = new Monster();
            Random rand = new Random();
            Array monsterArray = Enum.GetValues(typeof(MonsterType));

            monster.monstertype = (MonsterType)monsterArray.GetValue(rand.Next(monsterArray.Length));
            monster.Health = rand.Next(1, 15);
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

    }

    static void Main(string[] args)
    {
        Player player = new Player();

        while (true)
        {
            Console.WriteLine("Welcome to the game, what is your name?");
            var playersname = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(playersname))
            {
                player.CreatePlayer(playersname);

                while (true)
                {
                    Console.WriteLine($"{player.Name}, What class would you like to play? Type 'Rogue', 'Knight', or 'Archer' to continue");

                    var playersclass = Console.ReadLine().ToLower();

                    if (playersclass == "rogue" || playersclass == "knight" || playersclass == "archer")
                    {

                        switch (playersclass)
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

                        int Rounds = 1;

                        while (Rounds < 11)
                        {
                            Monster monster = new Monster().GenerateMonster();
                            Console.WriteLine($"Game continues - Round {Rounds}");
                            
                                monster.GenerateMonster();
                                Console.WriteLine("A monster appears!");
                                Thread.Sleep(1000);
                                monster.MonsterStats();
                            var input = Console.ReadLine();
                            if (input is not null)
                            {
                                Rounds++;
                            }
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please try again!");
                    }
                }

                Console.WriteLine("Congratulations! You have completed the game, until we meet again warrior!");
                Thread.Sleep(2000);
                break;
            }
            else
            {
                Console.WriteLine("Invalid Name!");
            }
        }
    }
}