using System;

namespace Complex
{
    class Program
    {
        public static void RenderMap(char[,] map, int H)
        {
            Console.Clear();
            Console.Write("HP:");
            Console.WriteLine(H);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int heroX = 1;
            int heroY = 1;
            int doorX = 6;
            int doorY = 6;
            int monsterY = 4;
            int monsterX = 4;
            int HP = 2;


            char[,] map =
            {
                {'#','#','#','#','#','#','#','#'},
                {'#','.','.','.','.','.','.','#'},
                {'#','.','.','.','.','.','.','#'},
                {'#','.','.','.','.','.','.','#'},
                {'#','.','.','.','.','.','.','#'},
                {'#','.','.','.','.','.','.','#'},
                {'#','.','.','.','.','.','.','#'},
                {'#','#','#','#','#','#','#','#'}
            };

            map[heroY, heroX] = '@';
            map[doorY, doorX] = '>';
            map[monsterY, monsterX] = 'M';

            RenderMap(map, HP);

            ConsoleKeyInfo keypress;
            do
            {
                keypress = Console.ReadKey();
                
                // TODO: switch case
                if (keypress.KeyChar == 'w')
                {
                    if (map[heroY - 1, heroX] != '#')
                    {
                        if (map[heroY - 1, heroX] != 'M')
                        {
                            map[heroY, heroX] = '.';
                            heroY--;
                            map[heroY, heroX] = '@';
                         
                            if (map[monsterY - 1, monsterX] != '#')
                            {
                                if (map[monsterY + 1, monsterX] != '#')
                                {
                                    if (heroY <= 3)
                                    {
                                        map[monsterY, monsterX] = '.';
                                        map[monsterY - 1, monsterX] = 'M';
                                        monsterY--;
                                        RenderMap(map, HP);
                                    }
                                    else if (heroY > 3)
                                    {
                                        map[monsterY, monsterX] = '.';
                                        map[monsterY + 1, monsterX] = 'M';
                                        monsterY++;
                                        RenderMap(map, HP);
                                    }
                                    else
                                    {
                                        RenderMap(map, HP);
                                    }
                                    RenderMap(map, HP);

                                }
                                else
                                {
                                    HP--;
                                    map[monsterY, monsterX] = '.';
                                    RenderMap(map, HP);
                                }
                                //RenderMap(map);
                            }
                        }
                    }

                    else
                    {
                        if (map[monsterY - 1, monsterX] != '#')
                        {
                            if (heroY <= 3)
                            {
                                map[monsterY, monsterX] = '.';
                                map[monsterY - 1, monsterX] = 'M';
                                monsterY--;
                                RenderMap(map, HP);
                            }
                            else if (heroY > 3)
                            {
                                map[monsterY, monsterX] = '.';
                                map[monsterY + 1, monsterX] = 'M';
                                monsterY++;
                                RenderMap(map, HP); ;
                            }
                            //RenderMap(map);
                            else
                            {
                                HP--;
                                map[monsterY, monsterX] = '.';
                                RenderMap(map, HP);
                            }
                        }
                    }
                }

                if (keypress.KeyChar == 'd')
                {
                    if (map[heroY, heroX + 1] != '#')
                    {
                        if (map[heroY, heroX + 1] != 'M')
                        {
                            map[heroY, heroX] = '.';
                            map[heroY, heroX + 1] = '@';
                            heroX++;
                            if (map[monsterY, monsterX + 1] != '#')
                            {
                                if (heroY <= 3)
                                {
                                    map[monsterY, monsterX] = '.';
                                    map[monsterY, monsterX - 1] = 'M';
                                    monsterX--;
                                    RenderMap(map, HP);
                                }
                                else if (heroY > 3)
                                {
                                    map[monsterY, monsterX] = '.';
                                    map[monsterY, monsterX + 1] = 'M';
                                    monsterX++;
                                    RenderMap(map, HP);
                                }
                                else
                                {
                                    RenderMap(map, HP);
                                }
                            }
                            RenderMap(map, HP);
                        }
                        else
                        {
                            HP--;
                            map[monsterY, monsterX] = '.';
                            RenderMap(map, HP);
                        }
                    }
                    else
                    {
                        if (map[monsterY, monsterX + 1] != '#')
                        {
                            map[monsterY, monsterX] = '.';
                            map[monsterY, monsterX + 1] = 'M';
                            monsterX++;
                            RenderMap(map, HP);
                        };
                        RenderMap(map, HP);
                    }


                }

                if (keypress.KeyChar == 's')
                {
                    if (map[heroY + 1, heroX] != '#')
                    {
                        if (map[heroY + 1, heroX] != 'M')
                        {
                            map[heroY, heroX] = '.';
                            map[heroY + 1, heroX] = '@';
                            heroY++;
                            if (map[monsterY + 1, monsterX] != '#')
                            {
                                if (map[monsterY - 1, monsterX] != '#')
                                {
                                    if (heroY <= 3)
                                    {
                                        map[monsterY, monsterX] = '.';
                                        map[monsterY - 1, monsterX] = 'M';
                                        monsterY--;
                                        RenderMap(map, HP);
                                    }
                                    else if (heroY > 3)
                                    {
                                        map[monsterY, monsterX] = '.';
                                        map[monsterY + 1, monsterX] = 'M';
                                        monsterY++;
                                        RenderMap(map, HP);
                                    }
                                    else
                                    {
                                        RenderMap(map, HP);
                                    }
                                    RenderMap(map, HP);

                                }
                                else
                                {
                                    RenderMap(map, HP);
                                }
                                //RenderMap(map);
                            }
                        }
                    }

                    else
                    {
                        if (map[monsterY + 1, monsterX] != '#')
                        {
                            if (heroY <= 3)
                            {
                                map[monsterY, monsterX] = '.';
                                map[monsterY - 1, monsterX] = 'M';
                                monsterY--;
                                RenderMap(map, HP);
                            }
                            else if (heroY > 3)
                            {
                                map[monsterY, monsterX] = '.';
                                map[monsterY + 1, monsterX] = 'M';
                                monsterY++;
                                RenderMap(map, HP); ;
                            }
                            //RenderMap(map);
                            else
                            {
                                HP--;
                                map[monsterY, monsterX] = '.';
                                RenderMap(map, HP);
                            }
                        }
                    }
                }

                if (keypress.KeyChar == 'a')
                {
                    if (map[heroY, heroX - 1] != '#')
                    {
                        if (map[heroY, heroX - 1] != 'M')
                        {
                            map[heroY, heroX] = '.';
                            map[heroY, heroX - 1] = '@';
                            heroX--;
                            // TODO: Out of range (fix it)
                            if (map[monsterY, monsterX - 1] != '#')
                            {
                                if (heroY <= 3)
                                {
                                    map[monsterY, monsterX] = '.';
                                    map[monsterY, monsterX + 1] = 'M';
                                    monsterX++;
                                    RenderMap(map, HP);
                                }
                                else if (heroY > 3)
                                {
                                    map[monsterY, monsterX] = '.';
                                    map[monsterY, monsterX - 1] = 'M';
                                    monsterX--;
                                    RenderMap(map, HP);
                                }
                                else
                                {
                                    RenderMap(map, HP);
                                }
                            }
                            RenderMap(map, HP);
                        }
                        else
                        {
                            HP--;
                            map[monsterY, monsterX] = '.';
                            RenderMap(map, HP);
                        }
                    }
                    else
                    {
                        if (map[monsterY, monsterX - 1] != '#')
                        {
                            map[monsterY, monsterX] = '.';
                            map[monsterY, monsterX - 1] = 'M';
                            monsterX--;
                            RenderMap(map, HP);
                        };
                        RenderMap(map, HP);
                    }
                }
                if (map[heroY, heroX] == map[doorY, doorX])
                {
                    Console.Clear();
                    Console.WriteLine("Next Level");
                }
                if (HP < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over");
                }



            } while (keypress.KeyChar != '`');
        }
    }
}