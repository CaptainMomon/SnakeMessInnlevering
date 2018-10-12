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


            Point app = new Point();//app=apple or food og må flyttes

            //Board
            //Get the snake appear on the screen
            board.CreateSnake();


            //Place apple on the board randomly(place apple)
            gameManager.PlaceApple(board.boardW, board.boardH,app, board.snake);
            //(should be in game controller)
            //(Get the state of the snake
            
            player.StateOfGame(board.boardW, board.boardH, gameManager.rng, app, board.snake);
        }
    }
}