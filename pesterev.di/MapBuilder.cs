using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeGame
{
    class Floor
    {
        public static List<List<GameObject>> Builder()
        {
            var rand = new Random();
            int X = rand.Next(10, 15);
            int Y = rand.Next(6, 15);
            List<List<GameObject>> Floor = new List<List<GameObject>>();
            for (int i = 0; i < Y; i++)
            {
                Floor.Add(new List<GameObject>());
            }
            for (int i = 0; i < Y; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < X; j++)
                    {
                        Floor[i].Add(new GameObject(i, j, '.'));
                    }
                }
                else if (i == 1 || i == Y - 1)
                {
                    for (int j = 0; j < X; j++)
                    {
                        Floor[i].Add(new GameObject(i, j, '#'));
                    }
                }
                else
                {
                    for (int j = 0; j < X; j++)
                    {
                        if (j == 0 || j == X - 1)
                        { 
                            Floor[i].Add(new GameObject(i, j, '#'));
                        }
                        else
                        {
                            Floor[i].Add(new GameObject(i, j, '.'));
                        }
                    }
                }
            }
            return Floor;
        }
        public static void Сartographer(List<List<GameObject>> Floor)
        {
            for (int i = 1; i < Floor.Count; i++)
            {
                for (int j = 0; j < Floor[0].Count; j++)
                {
                    Console.Write(Floor[i][j].SP);    
                }
                Console.WriteLine();
            }
        }
    }
}
