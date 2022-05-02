using System;
namespace RogueLike
{

    class Map
    {
        private int col, row;
        private List<List<GameObject>> world;
        private Hero hero1; 
        public Hero Hero1 { get { return hero1; } set { hero1 = value; } }
        public Map()
        {
            Random rand = new Random();
            col = rand.Next(5, 16);
            row = rand.Next(5, 16);
            world = new List<List<GameObject>>();
            for (int i = 0; i < col; i++)
            {
                world.Add(new List<GameObject>());
                for (int j = 0; j < row; j++)
                {
                    if (i == 0 || i == col - 1 || j == 0 || j == row - 1)
                    {

                        world[i].Add(new Wall(j, i));
                    }
                    else
                    {
                        world[i].Add(new GameObject(j, i));
                    }
                }
            }
            hero1 = new Hero();
            GameObject[] sim = new GameObject[3] { hero1, new Monster(), new Transition() };
            for (int i = 0; i < sim.Length; i++)
            {
                int tcol = rand.Next(1, col - 1);
                int trow = rand.Next(1, row - 1);
                while (world[tcol][trow] is Hero || world[tcol][trow] is Monster || world[tcol][trow] is Transition)
                {
                    tcol = rand.Next(1, col - 1);
                    trow = rand.Next(1, row - 1);

                }
                sim[i].Y = tcol;
                sim[i].X = trow;
                world[tcol][trow] = sim[i];
            }
        }

        public void Move(int x, int y)
        {
            GameObject temp1 = world[hero1.Y + y][hero1.X + x];
            if(temp1 is Monster)
            {

            }
            else if (temp1 is Transition)
            {

            }
            else if(!(temp1 is Wall)) {
                world[hero1.Y + y][hero1.X + x] = hero1;
                world[hero1.Y][hero1.X] = temp1;
                world[hero1.Y][hero1.X].X = hero1.X;
                world[hero1.Y][hero1.X].Y = hero1.Y;
                hero1.Move(x, y);
            }
        }

        public void Print()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    world[i][j].Print();
                }
                Console.WriteLine();
            }
        }

    }
    class GameObject
    {
        private int x;
        private int y;

        protected char symbol;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public GameObject()
        {

        }
        public GameObject(int x, int y)
        {
            this.X = x;
            this.y = y;
            this.symbol = '.';
        }
        public GameObject(int x, int y, char symbol)
        {
            this.X = x;
            this.y = y;
            this.symbol = '.';
        }

        public virtual void Print()
        {
            Console.Write(symbol);
        }

    }

    class Hero : GameObject
    {
        private int level;
        public int Level { get => level; set => level = value; }
        private int damage;
        public int Damage { get => damage; set => damage = value; }
        private int health;
        public int Health { get => health; set => health = value; }

        public Hero()
        {
            symbol = '@';
        }
        public Hero(int x, int y) : base(x, y)
        {
            base.symbol = '@';
        }

        public int Attack()
        {
            return level * damage;
        }
        public void Move(int x, int y)
        {
            X = X + x;
            Y = Y + y;
        }
    }

    class Monster : GameObject
    {
        public Monster()
        {
            symbol = 'M';
        }
        public Monster(int x, int y) : base(x, y)
        {
            base.symbol = 'M';
        }
    }

    class Transition : GameObject
    {
        public Transition()
        {
            symbol = '>';
        }
        public Transition(int x, int y) : base(x, y)
        {
            base.symbol = '>';
        }
    }

    class Wall : GameObject
    {
        public Wall()
        {
            symbol = '#';
        }
        public Wall(int x, int y) : base(x, y)
        {
            symbol = '#';
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Map mp = new Map();
            mp.Print();
            do
            {
                int x = 0, y = 0;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        y--;
                        break;
                    case ConsoleKey.DownArrow:
                        y++;
                        break;
                    case ConsoleKey.RightArrow:
                        x++;
                        break;
                    case ConsoleKey.LeftArrow:
                        x--;
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
                mp.Move(x, y);
                mp.Print();
            } while (true);
        }
    }
}

