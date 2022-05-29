using System;
using System.Drawing;

namespace RogueLIKE
{
  class Character
  {
    public int x;
    public int y;
    public char sign;
    // public bool transparent;
    public bool IsHero;
  }

  class Hero : Character
  {
    public int Damage;
    public int Health;
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

    static char[,] MotionHero(int sizeMap , Hero mainHero,ref Hero[] monsters, char[,] map)
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
          map = Collision(mainHero,ref monsters, map);
          break;
          
      }
      return map;
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

    static char[,] Collision(Hero mainHero,ref Hero[] monsters, char[,] map)
    {
      for (int i = 0; i < monsters.Length; i++)
      {
        bool collis = ((Math.Abs(mainHero.x - monsters[i].x) == 1 || Math.Abs(mainHero.x - monsters[i].x) == 0) && 
                       (Math.Abs(mainHero.y - monsters[i].y) == 1 || Math.Abs(mainHero.y - monsters[i].y) == 0));

        if (collis)
        {
          monsters[i].Health -= mainHero.Damage;
          // if (mainHero.Health > 0) mainHero.Health -= monster.Damage;
          
          if (monsters[i].Health <= 0)
          {
            Console.SetCursorPosition(monsters[i].y,monsters[i].x);
            Console.Write('.');
            map[monsters[i].x, monsters[i].y] = '.';
            RemoveElem(ref monsters, i);
          }
        }

      }
      
      return map;
    }

    static Hero PlayField(int sizeMap, int numberMonst, Hero mainHero)
    {
      Hero[] monsters = new Hero[numberMonst];
      char[,] map = CreatMap(sizeMap);
      for(int i = 0; i < numberMonst; i++)
      {
        monsters[i] = CreateMonsterCharacter(sizeMap);
        map[monsters[i].x, monsters[i].y] = monsters[i].sign;
      }
      map[mainHero.x, mainHero.y] = mainHero.sign;
      PrintMap(sizeMap, map);
      map[mainHero.x, mainHero.y] = '.';
      Console.WriteLine($"Количество XP: {mainHero.Health}");
      
      while (true)
      {
        map = MotionHero(sizeMap, mainHero,ref monsters, map);
        Console.SetCursorPosition(0, sizeMap);
        Console.WriteLine($"Количество XP: {mainHero.Health}");
        if (mainHero.Health == 0)
        {
          Console.SetCursorPosition(mainHero.y, mainHero.x);
          Console.Write('X'); 
          Console.SetCursorPosition(0, sizeMap);
          Console.WriteLine("Количество XP: 0 - Вы проиграли!");
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
        int numberMonst = rand.Next(sizeMap/4, sizeMap/2);
        Hero mainHero = CreateHeroCharacter(sizeMap);
        
        if (PlayField(sizeMap, numberMonst, mainHero).Health == 0) break;
      }
    }
  }
}
