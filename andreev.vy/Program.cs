using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

class GameObject
{
    private int x;
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    private int y;
    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    protected char Draw;
    public char draw
    {
        get { return Draw; }
        set { Draw = value; }
    }

    public GameObject(int x, int y, char Draw)
    {
        this.X = x;
        this.Y = y;
        this.Draw = Draw;
    }

    public static List<List<GameObject>> Seter(List<List<GameObject>> GameMap, GameObject gameObject)
    {
        GameMap[gameObject.x][gameObject.y] = gameObject;
        return GameMap;
    }
    public static GameObject CoordinateRandomizer(List<List<GameObject>> GameMap, GameObject gameObject)
    {
        Random random = new Random();
        while (true)
        {
            gameObject.x = random.Next(2, GameMap.Count - 2);
            gameObject.y = random.Next(1, GameMap[1].Count - 1);
            if (GameMap[gameObject.x][gameObject.y].draw == '.') break;
        }
        return gameObject;
    }
}
internal class Characters : GameObject
{
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    private int lavel;
    public int Lavel
    {
        get { return lavel; }
        set { lavel = value; }
    }
    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    protected int Width;
    protected int Height;
    protected int ObjectСoordinates;
    protected bool Hero;

    public Characters(int x, int y, char Draw, int Health, int Damage, int Level, int Width, int Height, int ObjectСoordinates, bool Hero) : base(x, y, Draw)
    {
        this.Health = Health;
        this.Damage = Damage;
        this.Lavel = Level;
        this.Width = Width;
        this.Height = Height;
        this.ObjectСoordinates = ObjectСoordinates;
        this.Hero = Hero;
    }
    public static Characters CoordinateRandomizer(List<List<GameObject>> GameMap, Characters characters)
    {
        Random random = new Random();
        while (true)
        {
            characters.X = random.Next(2, GameMap.Count - 2);
            characters.Y = random.Next(1, GameMap[0].Count - 1);
            if (GameMap[characters.X][characters.Y].draw == '.') break;
        }
        return characters;
    }
    protected static bool IsMove(List<List<GameObject>> GameMap, Characters characters, int Xx, int Yy)
    {
        bool temp = true;
        if (GameMap[characters.X + Xx][characters.Y + Yy].draw == '#' || GameMap[characters.X + Xx][characters.Y + Yy].draw == 'M' || GameMap[characters.X + Xx][characters.Y + Yy].draw == '@')
            temp = false;
        return temp;
    }
    public static List<List<GameObject>> Mover(List<List<GameObject>> GameMap, Characters characters)
    {
        if (characters.Width == 0 & characters.Height == 0)
            return GameMap;
        else
        {
            char temp = GameMap[characters.X][characters.Y].draw;
            GameMap[0][characters.ObjectСoordinates].X = characters.X - characters.Width;
            GameMap[0][characters.ObjectСoordinates].Y = characters.Y - characters.Height;
            GameMap[characters.X][characters.Y] = characters;
            GameMap = Seter(GameMap, GameMap[0][characters.ObjectСoordinates]);
            characters.Width = 0;
            characters.Height = 0;
            GameMap[0][characters.ObjectСoordinates] = new GameObject(0, 0, temp);
        }
        return GameMap;
    }
    public static Characters HeroControl(List<List<GameObject>> GameMap, Characters Hero, Characters Monster)
    {

        if (Hero.Hero == true)
        {
            if (Math.Abs((Hero.X - Monster.X) + (Hero.Y - Monster.Y)) == 1)
            {
                Monster.Health = Monster.Health - (Hero.Damage * Hero.Lavel);
                if (Monster.Health <= 0)
                    Hero.Lavel++;
            }
            ConsoleKey consoleKey = Console.ReadKey().Key;
            switch (consoleKey)
            {

                case ConsoleKey.UpArrow:
                    if (IsMove(GameMap, Hero, -1, 0))
                    {
                        Hero.Width = -1;
                        Hero.X--;
                        break;
                    }
                    else break;
                case ConsoleKey.DownArrow:
                    if (IsMove(GameMap, Hero, 1, 0))
                    {
                        Hero.Width = 1;
                        Hero.X++;
                        break;
                    }
                    else break;
                case ConsoleKey.LeftArrow:
                    if (IsMove(GameMap, Hero, 0, -1))
                    {
                        Hero.Height = -1;
                        Hero.Y--;
                        break;
                    }
                    else break;
                case ConsoleKey.RightArrow:
                    if (IsMove(GameMap, Hero, 0, 1))
                    {
                        Hero.Height = 1;
                        Hero.Y++;
                        break;
                    }
                    else break;
                default:
                    break;
            }
        }
        return Hero;
    }
    public static Characters MonsterControl(List<List<GameObject>> GameMap, Characters Hero, Characters Monster)
    {
        int X = Hero.X - Monster.X;
        int Y = Hero.Y - Monster.Y;
        if (X < 0 & IsMove(GameMap, Monster, -1, 0))
        {
            Monster.Width = -1;
            Monster.X--;
        }
        else if (X > 0 & IsMove(GameMap, Monster, 1, 0))
        {
            Monster.Width = 1;
            Monster.X++;
        }
        else if (Y < 0 & IsMove(GameMap, Monster, 0, -1))
        {
            Monster.Height = -1;
            Monster.Y--;
        }
        else if (Y > 0 & IsMove(GameMap, Monster, 0, 1))
        {
            Monster.Height = 1;
            Monster.Y++;
        }
        if (Math.Abs((Hero.X - Monster.X) + (Hero.Y - Monster.Y)) == 1)
        {
            Hero.Health -= Monster.Damage;
        }
        return Monster;
    }
    public static int[] Collision(Characters Hero, Characters Monster)
    {
        int[] HelthChange = new int[2];
        if (Math.Abs((Hero.X - Monster.X) + (Hero.Y - Monster.Y)) == 1)
            Hero.Health = Hero.Health - (Monster.Damage * Monster.Lavel);
        return HelthChange;
    }
}

class Dungeon
{
    public static List<List<GameObject>> Builder()
    {
        Random random = new Random();
        int X = random.Next(5, 15);
        int Y = random.Next(5, 15);
        List<List<GameObject>> GameMap = new List<List<GameObject>>();
        for (int i = 0; i < Y; i++)
        {
            GameMap.Add(new List<GameObject>());
        }
        for (int i = 0; i < Y; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < X; j++)
                {
                    GameMap[i].Add(new GameObject(i, j, '.'));
                }
            }
            else if (i == 1 || i == Y - 1)
            {
                for (int j = 0; j < X; j++)
                {
                    GameMap[i].Add(new GameObject(i, j, '#'));
                }
            }
            else
            {
                for (int j = 0; j < X; j++)
                {
                    if (j == 0 || j == X - 1)
                    {
                        GameMap[i].Add(new GameObject(i, j, '#'));
                    }
                    else
                    {
                        GameMap[i].Add(new GameObject(i, j, '.'));
                    }
                }
            }
        }
        return GameMap;
    }
    public static void Сartographer(List<List<GameObject>> GameMap)
    {
        for (int i = 1; i < GameMap.Count; i++)
        {
            for (int j = 0; j < GameMap[0].Count; j++)
            {
                Console.Write(GameMap[i][j].draw);
            }
            Console.WriteLine();
        }
    }
    class Programm
    {
        public static void Main()
        {
            int Level = 1;
            int HeroDamage = 5;
            int HeroHealth = 100;
            int MonsterDamage = 2;
            int MonsterHealth = 20;
            GameObject NextLevel = new GameObject(0, 0, '<');
            Characters hero = new Characters(0, 0, '@', HeroHealth, HeroDamage, 1, 0, 0, 0, true);
            while (hero.Health > 0)
            {
                List<List<GameObject>> Map = Dungeon.Builder();
                hero.Damage = HeroDamage * Level;
                if (hero.Health < 100)
                    hero.Health = 100;
                MonsterDamage *= Level;
                MonsterHealth *= Level;
                Characters Monster = new Characters(0, 0, 'M', MonsterHealth, MonsterDamage, 0, 0, 0, 1, false);
                hero = Characters.CoordinateRandomizer(Map, hero);
                Monster = Characters.CoordinateRandomizer(Map, Monster);
                NextLevel = GameObject.CoordinateRandomizer(Map, NextLevel);
                Map = GameObject.Seter(Map, hero);
                Map = GameObject.Seter(Map, Monster);
                Map = GameObject.Seter(Map, NextLevel);
                while (Map[0][0].draw != '<' & hero.Health > 0)
                {
                    Dungeon.Сartographer(Map);
                    if (Monster.Health <= 0)
                    {
                        Map[Monster.X][Monster.Y] = Map[0][1];
                        Monster.X = 0;
                        Monster.Y = 1;
                    }
                    else
                    {
                        Monster = Characters.MonsterControl(Map, hero, Monster);
                    }
                    Map = Characters.Mover(Map, Monster);
                    hero = Characters.HeroControl(Map, hero, Monster);
                    Map = Characters.Mover(Map, hero);
                    Console.Clear();
                    Console.WriteLine($"Здоровье игрока={hero.Health} Урон игрока={hero.Damage}  Здоровье монстра={Monster.Health} Урон монстра={Monster.Damage} Уровень={hero.Lavel}");
                }
                Level++;
            }
        }
    }
}