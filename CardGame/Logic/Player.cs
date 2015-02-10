using System.Collections.Generic;
using CardGame.Logic.Interfaces;

namespace CardGame.Logic
{
    class Player : IPlayer
    {
        internal enum GameStatus { Playing, Lost,
            StoppedPlaying,
            Won
        }
        private string _name;
        private List<ICard> _cardsOnHand;

        public Player(string name)
        {
            _name = name;
            _cardsOnHand = new List<ICard>();
        }

        public List<ICard> CardsOnHand
        {
            get { return _cardsOnHand; }
            set { _cardsOnHand = value; }
        }

        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public GameStatus Status { get; set; }

    }
}
