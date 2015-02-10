using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using CardGame.Logic.Interfaces;

namespace CardGame.Logic
{
    class Game
    {
        private GameState _status;
        private Deck _gameDeck;
        private List<Player> _players;
        private Queue<string> _log;
        private int _numberOfPlayers = 2;
        int _player1AcesOnHand = 0;
        int _player2AcesOnHand = 0;
        private Queue<string> _logQueue;
        private int _totalWinsPlayer1;
        private int _totalAcesPlayer1;
        private int _totalWinsPlayer2;
        private int _totalAcesPlayer2;


        internal enum GameState { Playing, Paused, NotStarted,
            NotPlaying,
            Ended
        }
        public Game(string[] playerNames)
        {
            Status = GameState.NotStarted;
            _gameDeck = new Deck();
            Log = new Queue<string>();
            SetUpPlayers(_numberOfPlayers, playerNames);
        }

        public Game(string[] playerNames, Queue<string> logQueue) : this(playerNames)
        {
            _logQueue = logQueue;
        }

        private void SetUpPlayers(int numberOfPlayers, string[] playerNames)
        {
            _players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                _players.Add(new Player(playerNames[i]));
            }
        }

        private void GiveCardsToPlayers(int numberOfPlayers)
        {
            int cardsPerPerson = _gameDeck.CardsInDeck.Count / numberOfPlayers;
            foreach (Player player in _players)
            {
                for (int i = 0; i < cardsPerPerson; i++)
                {
                    player.CardsOnHand.Add(_gameDeck.DealCard());
                }
            }
        }

        public void DealCards()
        {
            GiveCardsToPlayers(_numberOfPlayers);
        }

        public void ShuffleDeck()
        {
            _gameDeck.ShuffleDeck();
        }

        public void PlayGame()
        {
            int round = 1;

            if (Status == GameState.NotStarted)
            {
                ShuffleDeck();
                DealCards();
                CountAces();

                Status = GameState.Playing;
            }
            if (Status == GameState.Paused)
            {
                Status = GameState.Playing;
            }
            while (Status == GameState.Playing)
            {
                if (_players[0].CardsOnHand[0].Value > _players[1].CardsOnHand[0].Value)
                {
                    LogThis(round, _players[0].Name, _players[0].CardsOnHand[0], _players[1].Name, _players[1].CardsOnHand[0]);
                    _players[0].CardsOnHand.Add(_players[1].CardsOnHand[0]);
                    _players[0].CardsOnHand.Add(_players[0].CardsOnHand[0]);
                    _players[0].CardsOnHand.RemoveAt(0);
                    _players[1].CardsOnHand.RemoveAt(0);
                }
                else
                {
                    LogThis(round, _players[1].Name, _players[1].CardsOnHand[0], _players[0].Name, _players[0].CardsOnHand[0]);

                    _players[1].CardsOnHand.Add(_players[0].CardsOnHand[0]);
                    _players[1].CardsOnHand.Add(_players[1].CardsOnHand[0]);
                    _players[1].CardsOnHand.RemoveAt(0);
                    _players[0].CardsOnHand.RemoveAt(0);

                }

                if (_players[0].CardsOnHand.Count == 0)
                {
                    _players[0].Status = Player.GameStatus.Lost;
                    _players[1].Status = Player.GameStatus.Won;
                    Log.Enqueue(_players[1].Name + " won in " + round + "s");
                    Status = GameState.Ended;
                }
                if (_players[1].CardsOnHand.Count == 0)
                {
                    _players[1].Status = Player.GameStatus.Lost;
                    _players[0].Status = Player.GameStatus.Won;
                    Log.Enqueue(_players[0].Name + " won in " + round + " rounds");
                    Status = GameState.Ended;
                }
                if (Status == GameState.Ended)
                {
                    Log.Enqueue("Player1 had " + _player1AcesOnHand + "aces");
                    Log.Enqueue("Player2 had " + _player2AcesOnHand + "aces");
                }
                round++;
            }
            foreach (string s in Log)
            {
                Console.WriteLine(s);
            }
        }

        private void CountAces()
        {
            foreach (Player player in _players)
            {
                foreach (ICard card in player.CardsOnHand)
                {
                    if (card.Value == FrenchCard.Values.Ace)
                    {
                        if (player.Name == "Player1")
                        {
                            _player1AcesOnHand++;
                        }
                        else
                        {
                            _player2AcesOnHand++;
                        }
                    }
                }
            }
        }

        public void StopGame()
        {
            foreach (Player player in _players)
            {
                player.CardsOnHand.Clear();
                player.Status = Player.GameStatus.StoppedPlaying;
            }
            Status = GameState.NotPlaying;
        }

        public void PauseGame()
        {
            Status = GameState.Paused;
        }

        private void LogThis(int round, string winner, ICard winningCard, string loser, ICard losingCard)
        {
            string log;
            log = "Round: " + round + " - ";
            log += winner + "s ";
            log += winningCard + " ";
            log += "beat ";
            log += loser + "s ";
            log += losingCard;
            Log.Enqueue(log);
        }

        public GameState Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Queue<string> Log
        {
            get { return _log; }
            private set { _log = value; }
        }
    }
}
