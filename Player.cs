using System;
namespace Card
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The main deck of the player
        /// </summary>
        private Deck deck;

        /// <summary>
        /// tmpDeck is the second deck of the player,
        /// the deck which all of the cards that the player recives go into
        /// </summary>
        private Deck tmpDeck;

        /// <summary>
        /// Player's name, Read-Only
        /// </summary>
        public string name { get; }

        /// <summary>
        /// Constructor who creates new player with deck of cards
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="name"></param>
        public Player(Deck deck, string name)
        {
            this.deck = deck;
            this.name = name;
            this.tmpDeck = new Deck(0);
        }

        /// <summary>
        /// Constructor who creates new player with deck of cards from cards array
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="name"></param>
        public Player(Card[] cards, string name)
        {
            this.name = name;
            this.deck = new Deck(cards);
            this.tmpDeck = new Deck(0);
        }

        /// <summary>
        /// Takes the last card from the player's main deck
        /// </summary>
        /// <returns></returns>
        public Card TakeLastCard()
        {
            return this.deck.TakeLastCard();
        }

        /// <summary>
        /// Adds card to the tmpDeck
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            this.tmpDeck.AddCard(card);
        }

        /// <summary>
        /// Adds cards to the tmpDeck
        /// </summary>
        /// <param name="cards"></param>
        public void AddCards(Card[] cards)
        {
            this.tmpDeck.AddCards(cards);
        }

        /// <summary>
        /// Shuffles the tmpDeck and replaces it with the main Deck
        /// </summary>
        public void TmpToMain()
        {
            this.tmpDeck.Shuffle();
            this.deck = new Deck(tmpDeck);
            this.tmpDeck.Reset();
        }

        /// <summary>
        /// Return if any cards left in the main deck
        /// </summary>
        /// <returns></returns>
        public bool IsMainCardsLeft()
        {
            return this.deck.IsCardsLeft();
        }

        /// <summary>
        /// Return if any cards left in the tmpDeck
        /// </summary>
        /// <returns></returns>
        public bool IsTmpCardsLeft()
        {
            return this.tmpDeck.IsCardsLeft();
        }

        /// <summary>
        /// Used for debugging
        /// </summary>
        /// <returns></returns>
        public int HowMuchMainCards()
        {
            return this.deck.HowMuchCards();
        }

        /// <summary>
        /// Used for debugging
        /// </summary>
        /// <returns></returns>
        public int HowMuchTmpCards()
        {
            return this.tmpDeck.HowMuchCards();
        }
    }
}
