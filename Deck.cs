using System;
using System.Collections.Generic;
using System.Linq;

namespace Card
{
    /// <summary>
    /// Deck of cards
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// The actual deck of cards
        /// </summary>
        private Card[] deck;

        /// <summary>
        /// The index of the last card on the deck
        /// </summary>
        private int lastIndex;

        /// <summary>
        /// Default constructor that creates full deck of cards (54 cards)
        /// </summary>
        public Deck()
        {
            this.deck = new Card[54];
            int counter = 0;

            Number[] numbers = (Number[])Enum.GetValues(typeof(Number));
            Shape[] shapes = (Shape[])Enum.GetValues(typeof(Shape));

            foreach (Shape shape in shapes.Take(4))
            {
                foreach (Number num in numbers.Take(13))
                {
                    deck[counter] = new Card(shape, num);
                    counter++;
                }
            }
            deck[counter] = new Card(Shape.Black, Number.joker);
            counter++;
            deck[counter] = new Card(Shape.Red, Number.joker);
            this.lastIndex = counter;
        }

        /// <summary>
        /// Constructor that creates new deck from array of cards
        /// </summary>
        /// <param name="cards">Array of cards</param>
        public Deck(Card[] cards)
        {
            this.deck = cards;
            this.lastIndex = deck.Length - 1;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">Other deck to copy from</param>
        public Deck(Deck other)
        {
            this.lastIndex = other.lastIndex;
            this.deck = (Card[])other.deck.Clone();
        }

        /// <summary>
        /// Constructor that creates empty deck of cards
        /// </summary>
        /// <param name="num">Not important parameter</param>
        public Deck(int num)
        {
            this.lastIndex = -1;
            this.deck = new Card[54];
        }

        /// <summary>
        /// Add cards to the deck
        /// </summary>
        /// <param name="cards"></param>
        public void AddCards(Card[] cards)
        {
            foreach (Card card in cards)
            {
                this.AddCard(card);
            }
        }

        /// <summary>
        /// Resets the deck to an empty deck
        /// </summary>
        public void Reset()
        {
            this.deck = new Card[54];
            this.lastIndex = -1;
        }

        /// <summary>
        /// Returns how much cards are in the deck
        /// </summary>
        /// <returns></returns>
        public int HowMuchCards()
        {
            return this.lastIndex + 1;
        }

        /// <summary>
        /// Adds card to the deck
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            this.lastIndex++;
            this.deck[this.lastIndex] = card;
        }

        /// <summary>
        /// Returns false if there is no card left in the deck, else returns true
        /// </summary>
        /// <returns></returns>
        public bool IsCardsLeft()
        {
            return this.lastIndex >= 0;
        }

        /// <summary>
        /// Removes the last card from the deck
        /// </summary>
        private void RemoveLastCard()
        {
            this.lastIndex--;
        }

        /// <summary>
        /// Returns the last card on the deck
        /// </summary>
        /// <returns></returns>
        private Card GetLastCard()
        {
            return this.deck[this.lastIndex];
        }

        /// <summary>
        /// Returns the last card on the deck and removes it
        /// </summary>
        /// <returns></returns>
        public Card TakeLastCard()
        {
            Card card = GetLastCard();
            RemoveLastCard();
            return card;
        }

        /// <summary>
        /// Shuffles the non-Null cards on the deck
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();
            Card[] noNullDeck = this.deck.Where(card => card != null).ToArray();
            int tmpDeckLength = noNullDeck.Length;
            int n;
            for (int i = 0; i < 10; i++)
            {
                n = tmpDeckLength;
                while (n > 1)
                {
                    n--;
                    int rnd = random.Next(n + 1);
                    Card value = this.deck[rnd];
                    this.deck[rnd] = this.deck[n];
                    this.deck[n] = value;
                }
            }

        }

        /// <summary>
        /// Divides the deck to number of players,
        /// Each player get the same same number of cards if numberOfCards % numOfPlayers == 0
        /// </summary>
        /// <param name="numOfPlayers"></param>
        /// <returns></returns>
        public Card[][] DivideDeck(int numOfPlayers)
        {
            Card[][] arr = new Card[numOfPlayers][];
            int length = deck.Length / numOfPlayers;
            for (int i = 0; i < numOfPlayers; i++)
            {
                arr[i] = deck.Skip(length * i).Take(length).ToArray();
            }
            return arr;
        }
    }
}
