using System;
namespace Card
{
    /// <summary>
    /// Basic card class
    /// </summary>
    public class Card
    {
        public Shape shape { get; }
        public Number number { get; }


        public Card(Shape shape, Number number)
        {
            this.shape = shape;
            this.number = number;
        }
    }

    /// <summary>
    /// The number of the card, Includes prince, queen, king, ace and Joker
    /// </summary>
    public enum Number
    {
       two = 2,
       three = 3,
       four = 4,
       five = 5,
       six = 6,
       seven = 7,
       eight = 8,
       nine = 9,
       ten = 10,
       prince = 11,
       queen = 12,
       king = 13,
       ace = 14,
       joker = 15
    }

    /// <summary>
    /// The shape of the card, Jokers can be only Red or Black
    /// </summary>
    public enum Shape
    {
        Diamond,
        Club,
        Heart,
        Spade,
        Black,
        Red
    }
}
