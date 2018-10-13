using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class GameManager { 
        public Random rng { get; set; }
        public bool inUse = false;///eatingApple


        public GameManager()
        {
            rng = new Random();
            
        }

        //Method for placing the apples/$ on the board randomly aslong as the game is running.
        public void PlaceApple(int boardW, int boardH,Point AppleCord, List<Point> snake)
        {

            while (true)
            {
                AppleCord.X = rng.Next(0,boardH); AppleCord.Y = rng.Next(0, boardH);
                bool spot = true;//if the randomspot is same as the position of apple then break
                foreach (Point i in snake)
                    if (i.X == AppleCord.X && i.Y == AppleCord.Y)
                    {
                        spot = false;
                        break;
                    }
                if (spot)
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(AppleCord.X, AppleCord.Y); Console.Write("$");
                    break;
                }
            }
        }
        //Method for detecting if the player hits the board walls
        public void DetectCollisionWithConsole(int boardW, int boardH, Point newHead)
        {
            if (newHead.X < 0 || newHead.X >= boardW)
                GameOver();
            else if (newHead.Y < 0 || newHead.Y >= boardH)
                GameOver();
        }
        //Method for detecting if the player hit an apple/$ or if the player/snake is to big to place any adtitional apples/$
        public void DetectCollisionWithApple(int boardW,int boardH, Point AppleCord, List<Point> snake, Point newHead)
        {

            if (newHead.X == AppleCord.X && newHead.Y == AppleCord.Y)
            {
                if (snake.Count + 1 >= boardW * boardH)
                    // No more room to place apples - game over.
                    GameOver();
                else
                {//check if apple is eaten
                    while (true)
                    {
                        AppleCord.X = rng.Next(0, boardW); AppleCord.Y = rng.Next(0, boardH);
                        bool found = false;
                        foreach (Point i in snake)
                            if (i.X == AppleCord.X && i.Y == AppleCord.Y)
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
        //Method for detecting if the player/snake eat itself/ hits itself.
        public void DetectCollisonWithItself( List<Point> snake, Point newHead)
        {
            if (!inUse)
            {
                snake.RemoveAt(0);
                foreach (Point x in snake)
                    if (x.X == newHead.X && x.Y == newHead.Y)
                    {
                        // Death by accidental self-cannibalism.
                        GameOver();
                        break;
                    }
            }
        }
        
        public void GameOver()
        {
            Environment.Exit(0);
        }
        //Method describing the state of the snake/game while running(not paused)
        public void GameUpdate(ref Direction newDirrection, ref Direction lastDirrection, Point AppleCord, List<Point> snake, Point tail, Point head, Point newHead)
        {
            if (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(head.X, head.Y); Console.Write("0");
                if (!inUse)
                {
                    Console.SetCursorPosition(tail.X, tail.Y); Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(AppleCord.X, AppleCord.Y); Console.Write("$");
                    inUse = false;
                }
                snake.Add(newHead);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.SetCursorPosition(newHead.X, newHead.Y); Console.Write("@");
                lastDirrection = newDirrection;
            }
        }
    }
}
