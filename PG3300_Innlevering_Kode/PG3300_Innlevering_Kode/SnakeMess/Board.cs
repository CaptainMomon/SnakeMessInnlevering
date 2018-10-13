using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Board {
        public List<Point> snake { get; set; }
        public int boardW { get; set; }
        public int boardH { get; set; }



        public Board()
        {
            snake = new List<Point>();
            //Decide the size of the game board
            boardW = Console.WindowWidth; 
            boardH = Console.WindowHeight;
            Console.Title = "Høyskolen Kristiania - SNAKE";
        }
           


        public List<Point> CreateSnake()
        {            
            //add four bodyparts initially to the snake
            snake.Add(new Point(10, 10));
            snake.Add(new Point(10, 10));
            snake.Add(new Point(10, 10));
            snake.Add(new Point(10, 10));
            Console.CursorVisible = false;
            //Draw head of the snake
            Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(10, 10); Console.Write("@");
            return snake;
        }
    }
}
