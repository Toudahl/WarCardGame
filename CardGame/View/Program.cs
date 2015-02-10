using System;
using System.Collections.Generic;
using CardGame.Logic;
using CardGame.Logic.Interfaces;

namespace CardGame.View
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[2] { "Player1", "Player2" };
            Queue<string> logQueue = new Queue<string>();
            Game cardGame = new Game(names);

            cardGame.PlayGame();

            //for (int i = 0; i < 1000; i++)
            //{
            //    cardGame.PlayGame();
            //}
        }
    }
}
