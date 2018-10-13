using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace SnakeMess
{
    class SnakeMess
    {
        public static void Main(string[] arguments)
        {
            
            Player player = new Player();
            Board board = new Board();
            GameManager gameManager = new GameManager();
            Point AppleCord = new Point();//apple or food cordinates

            //Get the snake appear on the screen
            board.CreateSnake();

            //Place apple on the board randomly(place apple)
            gameManager.PlaceApple(board.boardW, board.boardH, AppleCord, board.snake);
            //Get the state of the game if its not paused(move the snake, make it longer if apple/$ is eaten etc.)
            player.StateOfGame(board.boardW, board.boardH, gameManager.rng, AppleCord, board.snake);
        }
    }
}