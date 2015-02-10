using System.Collections;
using System.Collections.Generic;

namespace CardGame.Logic.Interfaces
{
    interface IDeck: ICollection<ICard>
    {
        void ShuffleDeck();
        ICard DealCard();
        Dictionary<int, List<ICard>> SplitDeck(int numberOfDecks);
        List<ICard> CardsInDeck { get; set; }
    }
}