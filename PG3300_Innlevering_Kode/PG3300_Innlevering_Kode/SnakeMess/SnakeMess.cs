using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

// WARNING: DO NOT code like this. Please. EVER! 
//          "Aaaargh!" 
//          "My eyes bleed!" 
//          "I facepalmed my facepalm." 
//          Etc.
//          I had a lot of fun obfuscating this code! And I can now (proudly?) say that this is the uggliest short piece of code I've ever written!
//          (It could maybe have been even ugglier? But the idea wasn't to make it fuggly-uggly, just funny-uggly or sweet-uggly. ;-)
//
//          -Tomas
//
namespace SnakeMess
{
    class Point
    {
        public const string Ok = "Ok";

        public int X; public int Y;
        public Point(int x = 0, int y = 0) { X = x; Y = y; }
        public Point(Point input) { X = input.X; Y = input.Y; }
    }

    class SnakeMess
    {
        public static void Main(string[] arguments)
        {
            bool gg = false; //gameover 
            bool pause = false;//gamepause
            bool inUse = false;///eatingApple
            short newDir = 2; // 0 = up, 1 = right, 2 = down, 3 = left //new direction of the snake
            short last = newDir;//lastdirection of the snake


            //Build boundary of the game
            int boardW = Console.WindowWidth, boardH = Console.WindowHeight;

            //For the apperance of the apple randomly
            Random rng = new Random();

            Point app = new Point();//app=apple or food

            //Board
            //Get the snake appear on the screen
            List<Point> snake = Board();


            //Place apple on the board randomly(place apple)
            PlaceApple(boardW, boardH, rng, app, snake);
            //(should be in game controller)
            //(Get the state of the snake
            Stopwatch t = new Stopwatch();
            t.Start();
            StateOfGame(ref gg, ref pause, ref inUse, ref newDir, ref last, boardW, boardH, rng, app, snake, t);
        }

        private static List<Point> Board()
        {
            List<Point> snake = new List<Point>();
            //add four bodyparts initially to the apple
            snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10));
            Console.CursorVisible = false;
            Console.Title = "Høyskolen Kristiania - SNAKE";
            //Draw head of the apple
            Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(10, 10); Console.Write("@");
            return snake;
        }

        private static void PlaceApple(int boardW, int boardH, Random rng, Point app, List<Point> snake)
        {
            while (true)
            {
                app.X = rng.Next(0, boardW); app.Y = rng.Next(0, boardH);
                bool spot = true;//if the randomspot is same as the position of apple then break
                foreach (Point i in snake)//if the position of the apple is same as the snake then the snake will eat the apple
                    if (i.X == app.X && i.Y == app.Y)
                    {
                        spot = false;
                        break;
                    }
                if (spot)
                {//
                    Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(app.X, app.Y); Console.Write("$");
                    break;
                }
            }
        }

        private static void StateOfGame(ref bool gg, ref bool pause, ref bool inUse, ref short newDir, ref short last, int boardW, int boardH, Random rng, Point app, List<Point> snake, Stopwatch t)
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
                    DetectCollision(ref gg, ref inUse, boardW, boardH, rng, app, snake, newH);
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
                    NewMethod1(gg, ref inUse, newDir, ref last, app, snake, tail, head, newH);
                }
            }
        }

        private static void GetDirection(short newDir, Point newH)
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

        private static void NewMethod1(bool gg, ref bool inUse, short newDir, ref short last, Point app, List<Point> snake, Point tail, Point head, Point newH)
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

        private static void DetectCollision(ref bool gg, ref bool inUse, int boardW, int boardH, Random rng, Point app, List<Point> snake, Point newH)
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
    }
}