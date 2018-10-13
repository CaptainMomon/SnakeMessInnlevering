using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Player
    {
        Stopwatch timer = new Stopwatch();
        GameManager game = new GameManager();
        bool pause = false;
        Direction newDirrection = Direction.DOWN;
        Direction lastDirrection;

        
        public void StateOfGame(int boardW, int boardH, Random rng, Point AppleCord, List<Point> snake)
        {
            //the game has three states, Game Over, Game is running and game is Paused.
            //Describe the state of the game; where the snake is and where it is going, Also if the snake hit something
            timer.Start();
            lastDirrection = newDirrection;
            while (true)
            {//if gameIsNotOver
                //the different player controlls/what each button do.
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape)
                        game.GameOver();
                    else if (cki.Key == ConsoleKey.Spacebar)
                        pause = !pause;
                    //(Get the direction)
                    //if uparrow key is pressed and the last direction of the snake is not down then go up 
                    else if (cki.Key == ConsoleKey.UpArrow && lastDirrection != Direction.DOWN)
                        newDirrection = Direction.UP;
                    else if (cki.Key == ConsoleKey.RightArrow && lastDirrection != Direction.LEFT)
                        newDirrection = Direction.RIGHT;
                    else if (cki.Key == ConsoleKey.DownArrow && lastDirrection != Direction.UP)
                        newDirrection = Direction.DOWN;
                    else if (cki.Key == ConsoleKey.LeftArrow && lastDirrection != Direction.RIGHT)
                        newDirrection = Direction.LEFT;
                }
                //loop for while the game is running
                if (!pause)
                {
                    //controlls the speed of the snake/game.
                    if (timer.ElapsedMilliseconds < 100)
                        continue;
                    timer.Restart();
                    //Direction of the snake according to the input
                    Point tail = new Point(snake.First());
                    Point head = new Point(snake.Last());
                    Point newHead = new Point(head);

                    //(move snake)
                    GetDirection( newHead);

                    //(Check if the snake meets the apple, boundary or itself)
                    game.DetectCollisionWithConsole(boardW, boardH, newHead);
                    game.DetectCollisionWithApple(boardW, boardH, AppleCord, snake, newHead);
                    game.DetectCollisonWithItself(snake, newHead);
                    game.GameUpdate(ref newDirrection, ref lastDirrection, AppleCord, snake, tail, head, newHead);
                }
            }
        }
        



        public void GetDirection( Point newHead)
        {
            switch (newDirrection)
            {
                case Direction.UP://Direction up
                    newHead.Y -= 1;
                    break;
                case Direction.RIGHT://Direction right
                    newHead.X += 1;
                    break;
                case Direction.DOWN://Diection down
                    newHead.Y += 1;
                    break;
                default://direction left
                    newHead.X -= 1;
                    break;
            }
        }
    }
}
