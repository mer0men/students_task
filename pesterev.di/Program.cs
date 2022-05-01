using System;
using RougeLikeGame;

namespace Game
{
    class Programm
    {
        public static void Main()
        {      
            int HeroDamage = 5;
            int HeroHealth = 100;
            GameObject Exit = new GameObject(0, 0,'<');           
            Character hero = new Character(0, 0, '@', HeroHealth, HeroDamage, 1, 0 , 0, 0, true);
            int Level = 1;
            while (hero.HP > 0)
            {
                List<List<GameObject>> Map = Floor.Builder();
                int MonsterDamage = 2 * Level;
                int MonsterHealth = 20 * Level;
                Character Monster = new Character(0, 0, 'M', MonsterHealth, MonsterDamage, 0, 0, 0, 1, false);
                hero = Character.CoordinateRandomizer(Map, hero);
                Monster = Character.CoordinateRandomizer(Map, Monster);
                Exit = GameObject.CoordinateRandomizer(Map, Exit);
                Map = GameObject.Seter(Map, hero);
                Map = GameObject.Seter(Map, Monster);
                Map = GameObject.Seter(Map, Exit);
                while (Map[0][0].SP != '<' & hero.HP > 0)
                {
                    Floor.Сartographer(Map);
                    if (Monster.HP <= 0)
                    {
                        Map[Monster.XP][Monster.YP] = Map[0][1];
                        Monster.XP = 0;
                        Monster.YP = 1;
                    }
                    else
                    {
                        Monster = Character.MonsterControl(Map, hero, Monster);
                    }
                    Map = Character.Mover(Map, Monster);
                    hero = Character.HeroControl(Map, hero, Monster);
                    Map = Character.Mover(Map, hero);
                    Console.Clear();
                    Console.WriteLine($"HP={hero.HP} Level={hero.LP}");
                }
                Level++;
            }   
        }
    }
}