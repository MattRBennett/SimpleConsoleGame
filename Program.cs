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
        //public int Range { get; set; } = 0;
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
                    //Range = 1;
                    break;
                case Speciality.Knight:
                    Strength = 10;
                    Defense = 8;
                    //Range = 2;
                    break;
                case Speciality.Archer:
                    Strength = 7;
                    Defense = 4;
                    //Range = 3;
                    break;
                default:
                    throw new Exception("Speciality unknown.");
            }

            
            Console.WriteLine($"{speciality} Selected!");
            Thread.Sleep(1000);
            Console.WriteLine($"Your health is {Health}");
            Thread.Sleep(1000);
            Console.WriteLine($"Your attack is {Strength}");
            Thread.Sleep(1000);
            Console.WriteLine($"Your defense is {Defense}");
            //Thread.Sleep(1000);
            //Console.WriteLine($"Your range is {Range}");
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
        //public int Range { get; set; } = 0;

        public void GenerateMonster()
        {
            Monster monster = new Monster();

            Random rand = new Random();

        }

    }

    static void Main(string[] args)
    {
        Player player = new Player();

        while (true)
        {
            Console.WriteLine("Welcome to the game, what is your name?");
            var playersname = Console.ReadLine();

            if (playersname != null)
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
                                Console.WriteLine("Unknown class!");
                                break;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid class!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Name!");

            }
        }
    }
}