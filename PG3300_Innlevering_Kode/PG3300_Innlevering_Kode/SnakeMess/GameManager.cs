﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class GameManager { 
        public Random rng { get; set; }
        public GameManager()
        {
            rng = new Random();
            
        }
    
        public void PlaceApple(int boardW, int boardH,Point app, List<Point> snake)
        {
            while (true)
            {
                app.X = rng.Next(0,boardH); app.Y = rng.Next(0, boardH);
                bool spot = true;//if the randomspot is same as the position of apple then break
                foreach (Point i in snake)
                    if (i.X == app.X && i.Y == app.Y)
                    {
                        spot = false;
                        break;
                    }
                if (spot)
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(app.X, app.Y); Console.Write("$");
                    break;
                }
            }
        }
        public void DetectCollision(ref bool gg, ref bool inUse, int boardW,int boardH, Random rng, Point app, List<Point> snake, Point newH)
        {
            if (newH.X < 0 || newH.X >= boardW)
                gg = true;
            else if (newH.Y < 0 || newH.Y >= boardH)
                gg = true;
            if (newH.X == app.X && newH.Y == app.Y)
            {
                if (snake.Count + 1 >= boardW * boardH)
                    // No more room to place apples - game over.
                    gg = true;
                else
                {//check if apple is eaten
                    while (true)
                    {
                        app.X = rng.Next(0, boardW); app.Y = rng.Next(0, boardH);
                        bool found = false;
                        foreach (Point i in snake)
                            if (i.X == app.X && i.Y == app.Y)
                            {
                                found = true;
                                break;
                            }
                        if (!found)
                        {
                            inUse = true;//searching the apple
                            break;
                        }
                    }
                }
            }
        }
        public void gameUpdate(bool gg, ref bool inUse, short newDir, ref short last, Point app, List<Point> snake, Point tail, Point head, Point newH)
        {
            if (!gg)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(head.X, head.Y); Console.Write("0");
                if (!inUse)
                {
                    Console.SetCursorPosition(tail.X, tail.Y); Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(app.X, app.Y); Console.Write("$");
                    inUse = false;
                }
                snake.Add(newH);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.SetCursorPosition(newH.X, newH.Y); Console.Write("@");
                last = newDir;
            }
        }
    }
}
