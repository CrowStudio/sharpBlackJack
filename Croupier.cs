using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace sharpBlackJack
{
    class Croupier
    {
        private bool deal, game;
        // Initiate list with as many elements as the maximum Players + The Houos
        // - to be able to inject respective points at its respective index
        private List<int> points = new List<int>() {0, 0, 0, 0, 0, 0};
        private DeckOfCards dealersDeck;

        
        public Croupier()
        {
            // Create a new DeckOfCard object with a shuffled Deck
            DealersDeck = new DeckOfCards();
        }

        public void DealHands(List<Player> stakeholders)
        {
            // Game is on
            Game = true;

            // Deal one card to The House and Players until they all got twoo cards each
            while (DealersDeck.ShuffledDeck.Count > 52 - (stakeholders.Count() * 2))
            {
                foreach (var stakholder in stakeholders)
                {
                    // Add a card to the respective hand from the top of the ShuffledDeck
                    stakholder.AddCard(DealersDeck.ShuffledDeck[0]);
                    // Remove the top card of ShuffledDeck
                    DealersDeck.ShuffledDeck.RemoveAt(0);
                }
            }
        }

        public void NewCardPlayer(List<Player> stakeholders)
        {
            // Only deal cards to Players - not The House
            for (int i = 1; i < stakeholders.Count; i++)
            {
                // Initiate deal 
                deal = true;

                // As long as Player wants a card, or NOT Bust
                while (deal) HitOrStand(stakeholders, i);
            }
        }

        private void HitOrStand(List<Player> stakeholders, int index)
        {
            string input;
            int sum;

            // Sum the Players hand and keep track of the score
            Points[index] = sum = stakeholders[index].SumOfHand();

            // If Player is Bust, or got Black Jack - the deal is off
            if (sum == 0 || sum == 21) deal = false;

            else
            {
                Console.Clear();

                if (index == 1)
                {
                    stakeholders[0].ShowDealersFirstCard();
                    stakeholders[index].ShowHand();
                }

                else if (index == 2)
                {
                    stakeholders[0].ShowDealersFirstCard();
                    stakeholders[1].ShowHand();
                    stakeholders[index].ShowHand();
                }

                else
                {
                    stakeholders[0].ShowDealersFirstCard();

                    for (int i = 1; i < index; i++) stakeholders[i].ShowHand();

                    stakeholders[index].ShowHand();
                }

                Console.Write(stakeholders[index].Name + " - (H)it or (S)tand? ");
                input = Console.ReadLine().ToUpper();

                if (input != "H" && input != "S")
                {
                    Console.WriteLine("Incorrect input, must be h or s - try again!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }

                else
                {
                    if (input == "H" && deal)
                    {
                        // Add a card to Players hand from the top of the ShuffledDeck
                        stakeholders[index].AddCard(DealersDeck.ShuffledDeck[0]);
                        // Remove the top card of ShuffledDeck
                        DealersDeck.ShuffledDeck.RemoveAt(0);

                        // Update score again
                        Points[index] = sum = stakeholders[index].SumOfHand();

                        // If all PLayers are Bust - game ends
                        if (sum == 0 )
                        {
                            int allBusted = stakeholders.Count - 1;

                            for (int i = 1; i < stakeholders.Count; i++)
                            {
                                if (stakeholders[i].SumOfHand() == 0) allBusted--;
                                else allBusted = stakeholders.Count - 1;
                            }

                            // All Players are Bust - end game
                            if (allBusted == 0)
                            {
                                deal = false;
                                Game = false;
                            }

                            Console.Clear();
                            stakeholders[0].ShowDealersFirstCard();

                            for (int i = 1; i < index; i++) stakeholders[i].ShowHand();

                            stakeholders[index].ShowHand();
                        }
                    }

                    else if (input == "S")
                    {
                        // Update score again
                        Points[index] = sum = stakeholders[index].SumOfHand();

                        // Deal is off
                        deal = false;
                    }
                }
            }
        }

        public void NewCardDealer(List<Player> stakeholders)
        {
            int sum;

            if (Game)
            {
                Console.Clear();
                stakeholders[0].ShowHand();

                for (int i = 1; i < stakeholders.Count; i++) stakeholders[i].ShowHand();

                // Update The Houses score
                Points[0] = sum = stakeholders[0].SumOfHand();

                // New cards if The Houses score is less than 17 and not Bust
                while (sum <= 17 && sum != 0)
                {
                    Console.WriteLine("Hit enter to see The House's next card\n");
                    Console.ReadKey();

                    // Add a card to The Houses hand from the top of the ShuffledDeck
                    stakeholders[0].AddCard(DealersDeck.ShuffledDeck[0]);
                    // Remove the top card of ShuffledDeck
                    DealersDeck.ShuffledDeck.RemoveAt(0);

                    Console.Clear();
                    stakeholders[0].ShowHand();

                    for (int i = 1; i < stakeholders.Count; i++) stakeholders[i].ShowHand();

                    // Update The Houses score again
                    Points[0] = sum = stakeholders[0].SumOfHand();
                }
            }
        }

        public void Scores(List<Player> stakeholders)
        {
            int houseWin = 0;

            // Compare the Players score vs The Houses score
            for (int i = 1; i < stakeholders.Count; i++)
            {
                // Player Wins
                if (Points[i] > Points[0])
                {
                    Points[i] = 3;
                    houseWin = 0;
                }

                // Player and The House have a draw (Push), NO ONE wins
                else if (Points[i] == Points[0])
                {
                    Points[i] = 2;
                    houseWin = 0;
                }

                // Player Lose
                else if (Points[i] != 0 && Points[i] < Points[0])
                {
                    Points[i] = 1;
                    houseWin++;
                }
            }

            // Tell the Players their outcome
            for (int i = 1; i < stakeholders.Count; i++)
            {
                if (Points[i] == 3) Console.WriteLine("\n" + stakeholders[i].Name + " - Congratulations you WIN!");

                else if (Points[i] == 2 && Points[0] != 0) Console.WriteLine("\n" + stakeholders[i].Name + " - No one wins, it's a PUSH!");

                else if (Points[i] == 1 && houseWin > 0) Console.WriteLine("\n" + stakeholders[i].Name + " - You LOSE!");
            }
        }

        public bool Game { get => game; set => game = value; }
        public List<int> Points { get => points; set => points = value; }
        public DeckOfCards DealersDeck { get => dealersDeck; set => dealersDeck = value; }
    }
}

