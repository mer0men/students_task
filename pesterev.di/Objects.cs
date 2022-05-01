using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeGame
{
    internal class GameObject
    {
        private int x;

        public int XP
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

        public int YP
        {
            get { return y; }
            set { y = value; }
        }

        protected char Symbol;
        public char SP
        {
            get { return Symbol; }
            set { Symbol = value; }
        }
        public GameObject(int x, int y, char Symbol)
        {
            this.x = x;
            this.y = y;
            this.Symbol = Symbol;

        }
        public static List<List<GameObject>> Seter(List<List<GameObject>> Floor, GameObject gameObject)
        {
            Floor[gameObject.x][gameObject.y] = gameObject;
            return Floor;
        }
        public static GameObject CoordinateRandomizer(List<List<GameObject>> Floor, GameObject gameObject)
        {
            var rand = new Random();
            while (true)
            {
                gameObject.x = rand.Next(2, Floor.Count - 2);
                gameObject.y = rand.Next(1, Floor[1].Count - 1);
                if (Floor[gameObject.x][gameObject.y].SP == '.') break;
            }
            return gameObject;
        }
    }
    internal class Character : GameObject
    {
        protected int Damage;
        protected int Health; 
        protected int Level;
        protected int MoveX;
        protected int MoveY;
        protected int UnderObjectIndex;
        protected bool IsHero;
        public int LP
        {
            get { return Level; }
            set { Level = value; }
        }

        public int HP
        {
            get { return Health; }
            set { Health = value; }
        }


        public Character(int x, int y, char Symbol, int Health, int Damage, int Level, int MoveX, int MoveY, int UnderObjectIndex, bool  IsHero) 
            : base(x, y, Symbol)
        {
            this.Health = Health;
            this.Damage = Damage;
            this.Level = Level;
            this.MoveX = MoveX;
            this.MoveY = MoveY;
            this.UnderObjectIndex = UnderObjectIndex;
            this.IsHero = IsHero;
        }
        public static Character CoordinateRandomizer(List<List<GameObject>> Floor, Character character)
        {
            var rand = new Random();
            while (true)
            {
                character.XP = rand.Next(2, Floor.Count - 2);
                character.YP = rand.Next(1, Floor[0].Count - 1);
                if (Floor[character.XP][character.YP].SP == '.') break;
            }
            return character;
        }
        protected static bool IsMove (List<List<GameObject>> Floor, Character character, int BaisX, int BaisY)
        {
            bool temp = true;
            if (Floor[character.XP + BaisX][character.YP + BaisY].SP == '#' ||
                Floor[character.XP + BaisX][character.YP + BaisY].SP == 'M' ||
                Floor[character.XP + BaisX][character.YP + BaisY].SP == '@')
            {
                temp = false;
            }
            return temp;
        }
        public static List<List<GameObject>> Mover(List<List<GameObject>> Floor, Character character)
        {
            if (character.MoveX == 0 & character.MoveY == 0)
            {
                return Floor;
            }
            else
            {
                char temp = Floor[character.XP][character.YP].SP;
                Floor[0][character.UnderObjectIndex].XP = character.XP - character.MoveX;
                Floor[0][character.UnderObjectIndex].YP = character.YP - character.MoveY;
                Floor[character.XP][character.YP] = character;
                Floor = Seter(Floor, Floor[0][character.UnderObjectIndex]);
                character.MoveX = 0;
                character.MoveY = 0;
                Floor[0][character.UnderObjectIndex] = new GameObject(0, 0, temp);
            }            
            return Floor;
        }
        public static Character HeroControl(List<List<GameObject>> Floor, Character Hero, Character Monster)
        {
            ConsoleKey consoleKey = Console.ReadKey().Key;
            if (Hero.IsHero == true)
            {
                if (Math.Abs((Hero.XP - Monster.XP) + (Hero.YP - Monster.YP)) == 1)
                {
                    Monster.Health -= (Hero.Damage * Hero.Level);
                    if (Monster.Health <= 0)
                    {
                        Hero.Level++;
                    }
                }
                switch (consoleKey)
                {

                    case ConsoleKey.UpArrow:
                        if(IsMove(Floor, Hero, -1, 0))
                        {
                            Hero.MoveX = -1;
                            Hero.XP--;
                            break;
                        }
                        else break;                       
                    case ConsoleKey.DownArrow:
                        if (IsMove(Floor, Hero, 1, 0))
                        {
                            Hero.MoveX = 1;
                            Hero.XP++;
                            break;
                        }
                        else break;
                    case ConsoleKey.LeftArrow:
                        if (IsMove(Floor, Hero, 0, -1))
                        {
                            Hero.MoveY = -1;
                            Hero.YP--;
                            break;
                        }
                        else break;
                    case ConsoleKey.RightArrow:
                        if (IsMove(Floor, Hero, 0, 1))
                        {
                            Hero.MoveY = 1;
                            Hero.YP++;
                            break;
                        }
                        else break;
                    default:
                        break;
                }   
            }            
            return Hero;
        }
        public static Character MonsterControl (List<List<GameObject>> Floor, Character Hero, Character Monster)
        {
            var rand = new Random();
            int X = Hero.XP - Monster.XP;
            int Y = Hero.YP - Monster.YP;
            if (X < 0 & IsMove(Floor, Monster, -1, 0))
            {
                Monster.MoveX = -1;
                Monster.XP--;
            }
            else if (X > 0 & IsMove(Floor, Monster, 1, 0))
            {
                Monster.MoveX = 1;
                Monster.XP++;
            }
            else if (Y < 0 & IsMove(Floor, Monster, 0, -1))
            {
                Monster.MoveY = -1;
                Monster.YP--;
            }
            else if (Y > 0 & IsMove(Floor, Monster, 0, 1))
            {
                Monster.MoveY = 1;
                Monster.YP++;
            }
            if (Math.Abs((Hero.XP - Monster.XP) + (Hero.YP - Monster.YP)) == 1)
            {
                Hero.Health -= Monster.Damage;
            }
            return Monster;
        }
        public static int[] Collision (Character Hero, Character Monster)
        {
            int[] HelthChange = new int[2];
            if (Math.Abs((Hero.XP - Monster.XP) + (Hero.YP - Monster.YP)) == 1)
            {
                Hero.Health -= (Monster.Damage * Monster.Level);
            }
            return HelthChange;
        }
    }

}
