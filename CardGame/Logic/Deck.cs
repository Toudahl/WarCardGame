using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardGame.Logic.Interfaces;

namespace CardGame.Logic
{
    class Deck : IDeck
    {
        private List<ICard> _cardsInDeck;


        public Deck()
        {
            _cardsInDeck = new List<ICard>();
            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    CardsInDeck.Add(new FrenchCard((FrenchCard.Suits) j, (FrenchCard.Values) i));
                }
            }
        }

        public void ShuffleDeck()
        {
            Random rand = new Random();
            var result = _cardsInDeck.OrderBy(item => rand.Next());
            _cardsInDeck = new List<ICard>(result);
        }

        public ICard DealCard()
        {
            var cardToDeal = _cardsInDeck[0];
            _cardsInDeck.Remove(cardToDeal);
            return cardToDeal;
        }

        public Dictionary<int, List<ICard>> SplitDeck(int numberOfDecks)
        {
            throw new NotImplementedException();
        }

        public List<ICard> CardsInDeck
        {
            get { return _cardsInDeck; }
            set { _cardsInDeck = value; }
        }

        public IEnumerator<ICard> GetEnumerator()
        {
            return _cardsInDeck.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ICard item)
        {
            _cardsInDeck.Add(item);
        }

        public void Clear()
        {
            _cardsInDeck.Clear();
        }

        public bool Contains(ICard item)
        {
            return _cardsInDeck.Contains(item);
        }

        public void CopyTo(ICard[] array, int arrayIndex)
        {
            _cardsInDeck.CopyTo(array, arrayIndex);
        }

        public bool Remove(ICard item)
        {
            return _cardsInDeck.Remove(item);
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public override string ToString()
        {
            return String.Join(",\n", _cardsInDeck);
        }
    }
}
