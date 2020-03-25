using System;
using System.Collections.Generic;
using System.Linq;

namespace Card
{
    /// <summary>
    /// Game class that creates a new game and runs it
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The players in the game
        /// </summary>
        private Player[] players;

        /// <summary>
        /// Constuctor who creates a new game with specific number of players
        /// </summary>
        /// <param name="numOfPlayers"></param>
        public Game(int numOfPlayers)
        {
            this.players = new Player[numOfPlayers];
            ResetGame(numOfPlayers);
        }

        /// <summary>
        /// Runs the game, runs the function DoTurn() until there is a winner
         /// </summary>
        public void Run()
        {
            List<Player>[] results = new List<Player>[14];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new List<Player>();
            }

            while (!DoTurn(this.players, new List<Card>(), results))
            {
                for (int i = 0; i < results.Length; i++)
                {
                    results[i].RemoveAll(list => true);
                }
            }
            return;
        }

        /// <summary>
        /// Resets the game, creates a full deck with 54 cards, suffles it
        /// and then gives each player the same number of cards
        /// </summary>
        /// <param name="numOfPlayers"></param>
        private void ResetGame(int numOfPlayers)
        {
            Deck fullDeck = new Deck();
            fullDeck.Shuffle();
            Card[][] cardsForeachPlayer = fullDeck.DivideDeck(numOfPlayers);
            for (int i = 0; i < numOfPlayers; i++)
            {
                players[i] = new Player(cardsForeachPlayer[i], i.ToString());
            }
        }

        /// <summary>
        /// Playes a turn for the players
        /// </summary>
        /// <param name="arr">The players who play in the turn</param>
        /// <param name="cardsToAdd">The cards to add for the winning player</param>
        /// <param name="results">What is the result of each player</param>
        /// <returns>Returns true if there is a winner</returns>
        private bool DoTurn(Player[] arr, List<Card> cardsToAdd, List<Player>[] results)
        {
            Card tmpCard;
            Player tmpPlayer;
            for (int i = 0; i < arr.Length; i++)
            {
                tmpPlayer = arr[i];

                tmpCard = tmpPlayer.TakeLastCard();

                cardsToAdd.Add(tmpCard);

                results[(int)tmpCard.number - 2].Add(tmpPlayer);

                if (!tmpPlayer.IsMainCardsLeft())
                {
                    if (!tmpPlayer.IsTmpCardsLeft())
                    {
                        PlayerLost(tmpPlayer);
                        Console.WriteLine(tmpPlayer.name + " lost");
                        results = RemoveFromResults(results, tmpPlayer);
                        arr = RemoveFromPlayersArray(arr, tmpPlayer);
                        i--;
                        if (IsWinner())
                        {
                            return true;
                        }
                    }
                    else
                    {
                        tmpPlayer.TmpToMain();
                        Console.WriteLine(tmpPlayer.name + " shuffled");

                    }
                }

            }
            foreach (List<Player> playersList in results.Reverse())
            {
                if (playersList.Count == 1)
                {
                    playersList[0].AddCards(cardsToAdd.ToArray());
                    Console.WriteLine(playersList[0].name + " won the round");
                    return false;
                }
                else if (playersList.Count > 1)
                {
                    Console.Write("Tie between");
                    foreach (var tiePlayer in playersList)
                    {
                        Console.Write(" " + tiePlayer.name);
                    }
                    Console.Write("\n");
                    Player[] tmpArr = playersList.ToArray();
                    playersList.RemoveAll(player => true);
                    return War(tmpArr, cardsToAdd, results);
                }
            }
            return false;
        }

        /// <summary>
        /// Called when two players place the same card, each player
        /// places 2 more cards and the it calls the function DoTurn() again
        /// for the last card
        /// </summary>
        ///<param name = "arr" > The players who play in the turn</param>
        /// <param name="cardsToAdd">The cards to add for the winning player</param>
        /// <param name="results">What is the result of each player</param>
        /// <returns>Returns true if there is a winner</returns>
        private bool War(Player[] arr, List<Card> cardsToAdd, List<Player>[] results)
        {
            Player tmpPlayer;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    tmpPlayer = arr[j];
                    cardsToAdd.Add(tmpPlayer.TakeLastCard());

                    if (!tmpPlayer.IsMainCardsLeft())
                    {
                        if (!tmpPlayer.IsTmpCardsLeft())
                        {
                            PlayerLost(tmpPlayer);
                            Console.WriteLine(tmpPlayer.name + " lost");
                            results = RemoveFromResults(results, tmpPlayer);
                            arr = RemoveFromPlayersArray(arr, tmpPlayer);
                            i--;
                            if (IsWinner()) return true;
                        }

                        else
                        {
                            Console.WriteLine(tmpPlayer.name + " shuffled");
                            tmpPlayer.TmpToMain();
                        }
                    }
                }
            }
            return DoTurn(arr, cardsToAdd, results);
        }

        /// <summary>
        /// Removes a specific player from an array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private Player[] RemoveFromPlayersArray(Player[] arr, Player player)
        {
            var list = arr.ToList();
            list.Remove(player);
            return list.ToArray();
        }

        /// <summary>
        /// Called when player lost, removes it from this.players
        /// </summary>
        /// <param name="index"></param>
        private void PlayerLost(int index)
        {
            var tmp = this.players.ToList();
            tmp.RemoveAt(index);
            this.players = tmp.ToArray();
        }

        /// <summary>
        /// Removes player from an array of list<Player>
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private List<Player>[] RemoveFromResults(List<Player>[] arr, Player player)
        {
            foreach (var list in arr)
            {
                list.Remove(player);
            }
            return arr;
        }

        /// <summary>
        /// Called when player lost, removes it from this.players
        /// </summary>
        /// <param name="other"></param>
        private void PlayerLost(Player other)
        {
            int index = this.players.ToList().FindIndex(player => player.name.Equals(other.name));
            PlayerLost(index);
        }

        /// <summary>
        /// Returns if there is a winner
        /// </summary>
        /// <returns></returns>
        private bool IsWinner()
        {
            if (this.players.Length == 1)
            {
                Console.WriteLine(this.players[0].name + " Won!");
                return true;
            }
            return false;
        }
    }
}
