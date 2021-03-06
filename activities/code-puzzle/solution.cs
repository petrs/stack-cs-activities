class Program
{
    static void Main()
    {
        var game = new Game();
        game.Start();
    }
}

public class Game
{
    private const int MaxLvl = 10;
    private readonly Hero paladin;
    private Monster actualMonster;

    public Game()
    {
        paladin = new Hero(null);
    }

    public void Start()
    {
        while (paladin.Lvl != MaxLvl && paladin.Health > 0)
        {
            var action = Console.ReadLine();

            switch (action)
            {
                case "heal":
                {
                    paladin.Heal();
                    break;
                }
                case "lvlup":
                {
                    paladin.LevelUp();
                    break;
                }
                case "fight":
                {
                    actualMonster = new Monster(paladin.Lvl);
                    switch (actualMonster.Type)
                    {
                        case MonsterType.Murloc:
                            Console.WriteLine("Mrglglrglglglglglgl!");
                            break;
                        case MonsterType.Kobold:
                            Console.WriteLine("You no take candle!");
                            break;
                        case MonsterType.Ogre:
                            Console.WriteLine("Crush! ...No, smash!");
                            break;
                    }
                    paladin.Health -= actualMonster.Damage;
                    paladin.Exp += actualMonster.Exp;
                    break;
                }
                default:
                {
                    Console.WriteLine("Not a valid action");
                    break;
                }
            }
        }
        Console.WriteLine(paladin.Lvl == MaxLvl ?
            "You have won!" : "You lost!");
    }
}

public enum MonsterType { Murloc, Kobold, Ogre }

public class Monster
{
    public int Damage { get; }
    public int Health { get; }
    public MonsterType Type { get; }
    public int Exp { get;}

    public Monster(int heroLevel)
    {
        Damage = heroLevel;
        Health = heroLevel + 5;
        var generator = new Random();
        var type = generator.Next(0, 4);

        switch (type)
        {
            case 0:
            case 1:
            {
                Type = MonsterType.Murloc;
                Exp = generator.Next(5, 11);
                Health -= generator.Next(2, 5);
                break;
            }
            case 2:
            {
                Type = MonsterType.Kobold;
                Exp = generator.Next(8, 16);
                Damage += generator.Next(2, 5);
                break;
            }
            case 3:
            {
                Type = MonsterType.Ogre;
                Exp = generator.Next(12, 19);
                Damage += generator.Next(3, 8);
                Health += generator.Next(1, 4);
                break;
            }
        }
    }

    public int Attack()
    {
        return new Random().Next(0, Damage);
    }
}

public class Hero
{
    public int Health { get; set; }
    public int Exp { get; set; }
    public int Lvl { get; private set; }
    public int? Age { get; }

    public Hero(int? age)
    {
        Health = 10;
        Exp = 0;
        Lvl = 1;
        Age = age ?? 48;
    }

    public void LevelUp()
    {
        if (Exp >= Lvl * 20)
        {
            Health += 2;
            Lvl++;
            Console.WriteLine("Ding!");
        }
        else
        {
            Console.WriteLine("Nope");
        }
    }

    public void Heal()
    {
        Health = Lvl + 9;
        Console.WriteLine("Healed");
    }
}