using System;

namespace CardGame.Logic.Interfaces
{
    interface ICard: IComparable<ICard>
    {
        FrenchCard.Suits Suit { get;}
        FrenchCard.Values Value { get; }
    }
}
