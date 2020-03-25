using System;

namespace Card
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of players >>>");
            int numberOfPlayers = int.Parse(Console.ReadLine());
            Game game = new Game(numberOfPlayers);
            game.Run();
        }
    }
}
