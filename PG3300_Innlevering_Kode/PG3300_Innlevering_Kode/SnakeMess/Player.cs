using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Player
    {
        Stopwatch t = new Stopwatch();
        
        public static void StateOfGame(ref bool gg, ref bool pause, ref bool inUse, ref short newDir, ref short last, int boardW, int boardH, Random rng, Point app, List<Point> snake, Stopwatch t)
        {
            while (!gg)
            {//if gameIsNotOver
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape)
                        gg = true;//gameover
                    else if (cki.Key == ConsoleKey.Spacebar)
                        pause = !pause;
                    //(Get the direction)
                    //if uparrow key is pressed and the last direction of the snake is not down then go up 
                    else if (cki.Key == ConsoleKey.UpArrow && last != 2)
                        newDir = 0;
                    else if (cki.Key == ConsoleKey.RightArrow && last != 3)
                        newDir = 1;
                    else if (cki.Key == ConsoleKey.DownArrow && last != 0)
                        newDir = 2;
                    else if (cki.Key == ConsoleKey.LeftArrow && last != 1)
                        newDir = 3;
                }
                if (!pause)
                {
                    if (t.ElapsedMilliseconds < 100)
                        continue;
                    t.Restart();
                    //Direction of the snake according to the input
                    Point tail = new Point(snake.First());
                    Point head = new Point(snake.Last());
                    Point newH = new Point(head);

                    //(move snake)
                    GetDirection(newDir, newH);

                    //(Check if the snake meets the apple, boundary or itself)
                    //Detect when the snake hits the boundary
                    GameManager.DetectCollision(ref gg, ref inUse, boardW, boardH, rng, app, snake, newH);
                    if (!inUse)
                    {
                        snake.RemoveAt(0);
                        foreach (Point x in snake)
                            if (x.X == newH.X && x.Y == newH.Y)
                            {
                                // Death by accidental self-cannibalism.
                                gg = true;
                                break;
                            }
                    }
                    GameManager.NewMethod1(gg, ref inUse, newDir, ref last, app, snake, tail, head, newH);
                }
            }
        }
        public static void GetDirection(short newDir, Point newH)
        {
            switch (newDir)
            {
                case 0://Direction up
                    newH.Y -= 1;
                    break;
                case 1://Direction right
                    newH.X += 1;
                    break;
                case 2://Diection down
                    newH.Y += 1;
                    break;
                default://direction left
                    newH.X -= 1;
                    break;
            }
        }
    }
}
