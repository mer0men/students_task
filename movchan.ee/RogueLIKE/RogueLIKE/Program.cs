using System;
using System.Drawing;

namespace RogueLIKE
{
  class Character
  {
    public int x;
    public int y;
    public char sign;
    public bool IsHero;
  }

  class Hero : Character
  {
    public int Damage;
    public int Health;
  }

  class Bullet : Character
  {
    public char Direction;
    public double Speed;
    public int Damage;
  }

  static class Program
  {
    static char[,] CreatMap(int sizeMap)
    {
      char[,] map = new char[sizeMap, sizeMap];
      for (int i = 0; i < sizeMap; i++)
      {
        map[0, i] = '#';
        map[i, 0] = '#';
        map[i, sizeMap - 1] = '#';
        map[sizeMap - 1, i] = '#';
        if (i > 0 && i < sizeMap - 1)
        {
          for (int j = 1; j < sizeMap - 1; j++)
          {
            map[i, j] = '.';
          }
        }

        map[sizeMap - 2, sizeMap - 2] = '<';
      }

      return map;
    }

    static void PrintMap(int sizeMap, char[,] map)
    {
      for (int i = 0; i < sizeMap; i++)
      {
        for (int j = 0; j < sizeMap; j++)
        {
          Console.Write(map[i, j]);
        }

        Console.WriteLine();
      }
    }

    static Hero CreateHeroCharacter(int sizeMap)
    {
      Hero mainHero = new Hero();
      mainHero.x = 1;
      mainHero.y = 1;
      mainHero.Health = 20;
      mainHero.Damage = 3;
      mainHero.sign = '@';
      mainHero.IsHero = true;
      return mainHero;
    }

    static Hero CreateMonsterCharacter(int sizeMap)
    {
      var rand = new Random();
      Hero monster = new Hero();
      monster.x = rand.Next(2, sizeMap - 2);
      monster.y = rand.Next(2, sizeMap - 2);
      monster.Health = rand.Next(6, 16);
      monster.Damage = rand.Next(1, 5);
      monster.sign = 'M';
      monster.IsHero = false;
      return monster;
    }

    static Bullet CreateBullet(char direct, Hero mainHero)
    {
      Bullet bullet = new Bullet();
      bullet.x = mainHero.x;
      bullet.y = mainHero.y;
      bullet.Damage = 6;
      bullet.sign = '*';
      bullet.IsHero = false;
      bullet.Direction = direct;
      bullet.Speed = 1;
      return bullet;
    }

    static void RemoveElem(ref Hero[] array, int index)
    {
      Hero[] newArray = new Hero[array.Length - 1];

      for (int i = 0; i < index; i++)
        newArray[i] = array[i];

      for (int i = index + 1; i < array.Length; i++)
        newArray[i - 1] = array[i];

      array = newArray;

    }

    static char[,] Collision(ref Hero mainHero, ref Hero[] monsters, ref char[,] map, Bullet bullet)
    {
      for (int i = 0; i < monsters.Length; i++)
      {
        bool collisHero = ((Math.Abs(mainHero.x - monsters[i].x) == 1 || Math.Abs(mainHero.x - monsters[i].x) == 0) &&
                           (Math.Abs(mainHero.y - monsters[i].y) == 1 || Math.Abs(mainHero.y - monsters[i].y) == 0));

        bool collisBull = bullet.x == monsters[i].x && bullet.y == monsters[i].y;

        if (collisHero) monsters[i].Health -= mainHero.Damage;

        if (collisBull) monsters[i].Health -= bullet.Damage;

        if (monsters[i].Health <= 0)
        {
          Console.SetCursorPosition(monsters[i].y, monsters[i].x);
          Console.Write('.');
          map[monsters[i].x, monsters[i].y] = '.';
          RemoveElem(ref monsters, i);
        }

      }

      return map;
    }

    static char[,] Shooting(char direct, ref Hero mainHero, ref Hero[] monsters, int sizeMap, ref char[,] map)
    {
      Bullet bul = CreateBullet(direct, mainHero);
      // MotionMonser(ref monsters, ref mainHero, ref map, sizeMap);
      for (int i = 0; i < 20; i++)
      {
        int x = bul.x, y = bul.y;
        if (bul.Direction == 'u' && ((x - 1 <= (sizeMap - 2)) && x - 2 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.x -= 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'd' && ((x + 1 <= (sizeMap - 2)) && x + 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.x += 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'r' && ((y + 1 <= (sizeMap - 2)) && y + 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.y += 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'l' && ((y - 1 <= (sizeMap - 2)) && y - 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.y -= 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'u' && ((x - 1 <= (sizeMap - 2)) && x - 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.x -= 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'q' && ((x - 1 <= (sizeMap - 2)) && x - 1 >= 1) &&
            ((y - 1 <= (sizeMap - 2)) && y - 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.x -= 1;
          bul.y -= 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'e' && ((x - 1 <= (sizeMap - 2)) && x - 1 >= 1) &&
            ((y - 1 <= (sizeMap - 2)) && y - 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.x -= 1;
          bul.y += 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'z' && ((x - 1 <= (sizeMap - 2)) && x - 1 >= 1) &&
            ((y - 1 <= (sizeMap - 2)) && y - 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.x += 1;
          bul.y -= 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }

        if (bul.Direction == 'x' && ((x - 1 <= (sizeMap - 2)) && x - 1 >= 1) &&
            ((y - 1 <= (sizeMap - 2)) && y - 1 >= 1))
        {
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(map[bul.x, bul.y]);
          bul.x += 1;
          bul.y += 1;
          Console.SetCursorPosition(bul.y, bul.x);
          Console.Write(bul.sign);
          Collision(ref mainHero, ref monsters, ref map, bul);
        }
      }

      return map;
    }

    /*static void MotionMonser(ref Hero[] monsters, ref Hero mainHero, ref char[,] map, int sizeMap)
    {
      var rand = new Random();
      int stepX = rand.Next(-1, 1);
      int stepY = rand.Next(-1, 1);
      for (int i = 0; i < monsters.Length; i++)
      {
        for (int j = 0; j < monsters.Length; j++)
        {
          bool notCollisMonst = (i != j && (monsters[i].x != monsters[j].x && monsters[i].y != monsters[j].y));

          if (((monsters[i].x + stepX <= (sizeMap - 2)) && monsters[i].x + stepX >= 1) && notCollisMonst)
          {
            map[monsters[i].x, monsters[i].y] = '.';
            Console.SetCursorPosition(monsters[i].y, monsters[i].x);
            Console.Write('.');
            monsters[i].x += stepX;
            Console.SetCursorPosition(monsters[i].y, monsters[i].x);
            Console.Write(monsters[i].sign);
            map[monsters[i].x, monsters[i].y] = monsters[i].sign;
          }

          if (((monsters[i].y + stepY <= (sizeMap - 2)) && monsters[i].y + stepY >= 1) && notCollisMonst)
          {
            map[monsters[i].x, monsters[i].y] = '.';
            Console.SetCursorPosition(monsters[i].y, monsters[i].x);
            Console.Write('.');
            monsters[i].y += stepY;
            Console.SetCursorPosition(monsters[i].y, monsters[i].x);
            Console.Write(monsters[i].sign);
            map[monsters[i].x, monsters[i].y] = monsters[i].sign;
          }

          // if (((mainHero.x - monsters[i].x) < -1) && collis)
          // {
          //   Console.SetCursorPosition(monsters[i].y, monsters[i].x);
          //   Console.Write('.');
          //   monsters[i].x -= 1;
          //   Console.SetCursorPosition(monsters[i].y, monsters[i].x);
          //   Console.Write(monsters[i].sign);
          // }
          //
          // if (((mainHero.y - monsters[i].y) > 1) && collis)
          // {
          //   Console.SetCursorPosition(monsters[i].y, monsters[i].x);
          //   Console.Write('.');
          //   monsters[i].y += 1;
          //   Console.SetCursorPosition(monsters[i].y, monsters[i].x);
          //   Console.Write(monsters[i].sign);
          // }
        }
      }
    }*/

    static char[,] MotionHero(int sizeMap, ref Hero mainHero, ref Hero[] monsters, ref char[,] map)
    {
      ConsoleKeyInfo key;
      key = Console.ReadKey(true);
      int y = mainHero.y, x = mainHero.x;

      switch (key.Key)
      {
        case ConsoleKey.DownArrow:
          x += 1;
          if ((x <= (sizeMap - 2)) && x >= 1)
          {
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(map[mainHero.x, mainHero.y]);
            mainHero.x += 1;
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(mainHero.sign);
          }

          break;
        case ConsoleKey.UpArrow:
          x -= 1;
          if ((x <= (sizeMap - 2)) && x >= 1)
          {
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(map[mainHero.x, mainHero.y]);
            mainHero.x -= 1;
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(mainHero.sign);
          }

          break;
        case ConsoleKey.RightArrow:
          y += 1;
          if ((y <= (sizeMap - 2)) && y >= 1)
          {
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(map[mainHero.x, mainHero.y]);
            mainHero.y += 1;
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(mainHero.sign);
          }

          break;
        case ConsoleKey.LeftArrow:
          y -= 1;
          if ((y <= (sizeMap - 2)) && y >= 1)
          {
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(map[mainHero.x, mainHero.y]);
            mainHero.y -= 1;
            Console.SetCursorPosition(mainHero.y, mainHero.x);
            Console.Write(mainHero.sign);
          }

          break;
        case ConsoleKey.F:
          map = Collision(ref mainHero, ref monsters, ref map, CreateBullet('n', mainHero));
          break;
        case ConsoleKey.W:
          map = Shooting('u', ref mainHero, ref monsters, sizeMap, ref map);
          break;
        case ConsoleKey.A:
          map = Shooting('l', ref mainHero, ref monsters, sizeMap, ref map);
          break;
        case ConsoleKey.D:
          map = Shooting('r', ref mainHero, ref monsters, sizeMap, ref map);
          break;
        case ConsoleKey.S:
          map = Shooting('d', ref mainHero, ref monsters, sizeMap, ref map);
          break;

        case ConsoleKey.Q:
          map = Shooting('q', ref mainHero, ref monsters, sizeMap, ref map);
          break;
        case ConsoleKey.E:
          map = Shooting('e', ref mainHero, ref monsters, sizeMap, ref map);
          break;
        case ConsoleKey.Z:
          map = Shooting('z', ref mainHero, ref monsters, sizeMap, ref map);
          break;
        case ConsoleKey.X:
          map = Shooting('x', ref mainHero, ref monsters, sizeMap, ref map);
          break;
      }

      return map;
    }

    static Hero PlayField(int sizeMap, int numberMonst, Hero mainHero)
    {
      Hero[] monsters = new Hero[numberMonst];
      char[,] map = CreatMap(sizeMap);
      for (int i = 0; i < numberMonst; i++)
      {
        monsters[i] = CreateMonsterCharacter(sizeMap);
        map[monsters[i].x, monsters[i].y] = monsters[i].sign;
      }

      map[mainHero.x, mainHero.y] = mainHero.sign;
      PrintMap(sizeMap, map);
      map[mainHero.x, mainHero.y] = '.';
      Console.WriteLine($"Количество HP: {mainHero.Health}");
      while (true)
      {
        map = MotionHero(sizeMap, ref mainHero, ref monsters, ref map);
        Console.SetCursorPosition(0, sizeMap);
        Console.WriteLine($"Количество HP: {mainHero.Health}");
        if (mainHero.Health == 0)
        {
          Console.SetCursorPosition(mainHero.y, mainHero.x);
          Console.Write('X');
          Console.SetCursorPosition(0, sizeMap);
          Console.WriteLine("Количество HP: 0 - Вы проиграли!");
          return mainHero;
        }

        if (mainHero.y == sizeMap - 2 && mainHero.x == sizeMap - 2 && monsters.Length == 0)
        {
          Console.Clear();
          return mainHero;
        }
      }
    }
    
    static void Main()
    {
      while (true)
      {
        var rand = new Random();
        int sizeMap = rand.Next(5, 16);
        int numberMonst = rand.Next(sizeMap / 4, sizeMap / 2);
        Hero mainHero = CreateHeroCharacter(sizeMap);

        if (PlayField(sizeMap, numberMonst, mainHero).Health == 0) break;
      }
    }
  }
}

