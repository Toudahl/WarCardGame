using CardGame.Logic.Interfaces;

namespace CardGame.Logic
{
    class FrenchCard: ICard
    {
        private Suits _suit;
        private Values _value;

        #region Enums
        public enum Suits
        {
            Spades,
            Clubs,
            Diamonds,
            Hearts,
        };

        public enum Values
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace,
        }
        #endregion

        public FrenchCard(Suits suit, Values value)
        {
            _suit = suit;
            _value = value;
        }

        public Suits Suit
        {
            get { return _suit; }
        }

        public Values Value
        {
            get { return _value; }
        }

        public int CompareTo(ICard other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value + " of " + Suit;
        }
    }
}
