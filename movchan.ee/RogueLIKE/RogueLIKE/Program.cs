using System;
using System.Drawing;

namespace RogueLIKE
{
  class Character
  {
    public int x;
    public int y;
    public char sign;
    public bool transparent;
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
        if (i > 0 && i < sizeMap-1)
        {
          for (int j = 1; j < sizeMap-1; j++)
          {
            map[i, j] = '.';
          }
        }
        map[sizeMap-2, sizeMap-1] = ':';
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
      mainHero.Health = 10;
      mainHero.Damage = 3;
      mainHero.sign = '@';
      mainHero.IsHero = true;
      return mainHero;
    }
    static Hero CreateMonsterCharacter(int sizeMap)
    {
      var rand = new Random();
      Hero monster = new Hero();
      monster.x = rand.Next(sizeMap-2, sizeMap-2);
      monster.y = rand.Next(sizeMap-2, sizeMap-2);
      monster.Health = rand.Next(1, 7);
      monster.Damage = rand.Next(1, 5);
      monster.sign = 'M';
      monster.IsHero = false;
      return monster;
    }
    static void Main()
    {
      var rand = new Random();
      int sizeMap = rand.Next(5, 16);

      Hero mainHero = CreateHeroCharacter(sizeMap);
      Hero monster = CreateMonsterCharacter(sizeMap);
      char[,] map = CreatMap(sizeMap);
      map[mainHero.x, mainHero.y] = mainHero.sign;
      map[monster.x, monster.y] = monster.sign;

      PrintMap(sizeMap, map);
    }

  }
}