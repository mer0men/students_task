Андреев Валентин
@Fiksa_YaYuta
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike01
{
    class Program
    {
        static void Main(string[] args)
        {
            int Game_over = 0; 
            int Room_Counter = 1; 
            int Rooms_Counted = 0; 
            int Move_Left_Right = 0;
            int Move_Up_Down = 0;
            //int Road_Steps = 1;

            Random random = new Random();

            //string Draw_Char = "X";
            string[,] Draw_Game_Map = new string[120, 30];
            string[,] Player = new string[120,30];

            //.string Direction = ".";

            do
            {
                Console.Clear();
                int Spawn_Point_Width = random.Next(5, 15); 
                int Spawn_Point_Height = random.Next(5, 15); 

                if (Rooms_Counted != Room_Counter)
                {
                    int Spawn_Point_Generator_X = random.Next(0, 119 - Spawn_Point_Width);
                    int Spawn_Point_Generator_Y = random.Next(0, 29 - Spawn_Point_Height);
                    Move_Left_Right = Spawn_Point_Generator_X + 1;
                    Move_Up_Down = Spawn_Point_Generator_Y + 1;
                    int counter = 0;

                    for (int y = 0; y <= Spawn_Point_Height; y++)
                    {
                        for (int x = 0; x <= Spawn_Point_Width; x++)
                        {
                            if (Spawn_Point_Generator_X + Spawn_Point_Width >= 120)
                            {
                                int X_Difference = Spawn_Point_Generator_X + Spawn_Point_Width - 121;
                                Spawn_Point_Generator_X -= X_Difference;
                            }

                            if (Spawn_Point_Generator_Y + Spawn_Point_Height >= 29)
                            {
                                int Y_Difference = Spawn_Point_Generator_Y + Spawn_Point_Height - 31;
                                Spawn_Point_Generator_Y -= Y_Difference;
                            }

                            int X = Spawn_Point_Generator_X + x;
                            int Y = Spawn_Point_Generator_Y + y;

                            if (y == 0 || y == Spawn_Point_Height)
                            {
                                Draw_Game_Map[X, Y] = "_";
                            }
                            else
                            {
                                if (x == 0 || x == Spawn_Point_Width)
                                {
                                    Draw_Game_Map[X, Y] = "|";
                                }
                                else
                                {
                                    Draw_Game_Map[X, Y] = ".";
                                    //int[,] a = new int[Spawn_Point_Width + 1, X];
                                    //int[,] b = new int[Spawn_Point_Height + 1, Y];
                                }
                            }
                            if (Draw_Game_Map[X, Y] == "." && counter == 0)
                            {
                                Draw_Game_Map[X, Y] = null;
                                counter++;
                                Player[X, Y] = "@";
                            }
                        }
                    }
                    
                            Rooms_Counted++;
                }
                for (int y_Draw = 0; y_Draw < 29; y_Draw++)
                {
                    for (int x_Draw = 0; x_Draw < 119; x_Draw++)
                    {
                        Console.SetCursorPosition(x_Draw, y_Draw);
                        Console.Write(Draw_Game_Map[x_Draw, y_Draw]);
                        Console.Write(Player[x_Draw, y_Draw]);
                    }
                }
                ConsoleKeyInfo KeyInfo;
                KeyInfo = Console.ReadKey(true);
                switch (KeyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (Move_Left_Right < 119)
                        {
                            if (Draw_Game_Map[Move_Left_Right + 1, Move_Up_Down] == "|" || Draw_Game_Map[Move_Left_Right, Move_Up_Down + 1] == null)
                            {

                            }
                            else
                            {
                                Move_Left_Right++;
                                Draw_Game_Map[Move_Left_Right, Move_Up_Down] = "@";
                                Draw_Game_Map[Move_Left_Right - 1, Move_Up_Down] = ".";
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Move_Left_Right > 1)
                        {
                            if (Draw_Game_Map[Move_Left_Right - 1, Move_Up_Down] == "|" || Draw_Game_Map[Move_Left_Right, Move_Up_Down + 1] == null)
                            {

                            }
                            else
                            {
                                Move_Left_Right--;
                                Draw_Game_Map[Move_Left_Right, Move_Up_Down] = "@";
                                Draw_Game_Map[Move_Left_Right + 1, Move_Up_Down] = ".";
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (Move_Up_Down > 0)
                        {
                            if (Draw_Game_Map[Move_Left_Right, Move_Up_Down - 1] == "_" || Draw_Game_Map[Move_Left_Right, Move_Up_Down + 1] == null)
                            {

                            }
                            else
                            {
                                Move_Up_Down--;
                                Draw_Game_Map[Move_Left_Right, Move_Up_Down] = "@";
                                Draw_Game_Map[Move_Left_Right, Move_Up_Down + 1] = ".";
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Move_Up_Down < 28)
                        {
                            if (Draw_Game_Map[Move_Left_Right, Move_Up_Down + 1] == "_" || Draw_Game_Map[Move_Left_Right, Move_Up_Down + 1] == null)
                            {

                            }
                            else
                            {
                                Move_Up_Down++;
                                Draw_Game_Map[Move_Left_Right, Move_Up_Down] = "@";
                                Draw_Game_Map[Move_Left_Right, Move_Up_Down - 1] = ".";
                            }
                        }
                        break;
                }
            } while (Game_over == 0);
        }
    }
}
