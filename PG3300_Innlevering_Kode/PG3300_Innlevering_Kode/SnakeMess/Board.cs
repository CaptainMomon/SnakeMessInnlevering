using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Board
    {


        public static List<Point> newBoard()
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
    }
}
