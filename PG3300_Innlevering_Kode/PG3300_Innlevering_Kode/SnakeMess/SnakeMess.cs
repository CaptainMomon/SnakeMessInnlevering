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
    class SnakeMess
    {
        public static void Main(string[] arguments)
        {
            
            Player player = new Player();
            Board board = new Board();
            GameManager gameManager = new GameManager();

            //bool gg = false; //gameover 
            bool pause = false;//gamepause
            bool inUse = false;///eatingApple
            short newDir = 2; // 0 = up, 1 = right, 2 = down, 3 = left //new direction of the snake
            short last = newDir;//lastdirection of the snake


            
            //For the apperance of the apple randomly
            //Random rng = new Random();  //må flyttes

            Point app = new Point();//app=apple or food og må flyttes

            //Board
            //Get the snake appear on the screen
            board.CreateSnake();


            //Place apple on the board randomly(place apple)
            gameManager.PlaceApple(board.boardW, board.boardH,app, board.snake);
            //(should be in game controller)
            //(Get the state of the snake
            Stopwatch t = new Stopwatch();
            t.Start();
            player.StateOfGame( ref pause, ref inUse, ref newDir, ref last, board.boardW, board.boardH, gameManager.rng, app, board.snake, t);
        }
    }
}