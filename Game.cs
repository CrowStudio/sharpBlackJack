using System;
using System.Collections.Generic;
using System.Threading;

namespace sharpBlackJack
{
    class Game
    {
        private bool newRound;
        private List<Player> stakeholders = new List<Player>();
        Croupier dealer = new Croupier();

        public Game()
        {
            // Create a new Player object with its own HandOfCards object
            // This will take care of The Houses/dealers hand
            Stakeholders.Add(new Player("The House"));
            NewRound = true;
        }

        public void JoinGame()
        {
            int addPlayer = 5;
            bool join = true;
            string name, yn;

            Console.WriteLine("Welcome to this table of Black Jack");

            // Add up to 5 Players at the table, fill up or answer no to start game
            // - Quit by answering NO when table is empty
            while (NewRound && join && addPlayer > Stakeholders.Count - 1)
            {
                Console.Clear();
                if (Stakeholders.Count == 1) Console.WriteLine("The table is empty!\n");
                
                else Console.WriteLine("There is room for " + (addPlayer - Stakeholders.Count + 1) + " more Player\n");

                Console.WriteLine("Anyone who wants to join (Y/N)? ");
                yn = Console.ReadLine().ToUpper();

                if (yn == "Y")
                {
                    Console.WriteLine("\nWhat is your name: ");
                    name = Console.ReadLine();

                    // Create a new Player object with its own HandOfCards object
                    Stakeholders.Add(new Player(name));

                    Console.WriteLine("Welcome " + name + ", please take a seat while waiting for other Players to join.\n");
                    Thread.Sleep(1000);
                }

                else if (yn == "N")
                {
                    Console.Clear();
                    join = false;
                }

                else
                {
                    Console.WriteLine("Incorrect input, must be y or n - try again!");
                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
                Console.Clear();
            }

            /* Comment code above, and use code below for testing, so you don't have to add Players manually
            Stakeholders.Add(new Player("Kalle"));
            Stakeholders.Add(new Player("Lisa"));
            Stakeholders.Add(new Player("Johan"));
            Stakeholders.Add(new Player("Pia")); */

            // If no Players added quit game
            if (Stakeholders.Count == 1) NewRound = false;
        }

        public void StartDeal()
        {
            // Deal cards for all Players and The House
            Dealer.DealHands(Stakeholders);

            // Only show The Houses first card, and show all Players cards
            Stakeholders[0].ShowDealersFirstCard();
            for (int i = 1; i < Stakeholders.Count; i++)
            {
                Stakeholders[i].ShowHand();
                // If PLayer get a Natural - 21 with two first cards - game is over
                if (Stakeholders[i].SumOfHand() == 21)
                {
                    Dealer.Game = false;
                    break;
                }
            }
        }

        public void Play()
        {
            if (Dealer.Game)
            {
                // Added pause so all Players can see their hands and The Houses first card
                Console.WriteLine("Hit a key for cards!");
                Console.ReadKey();
                Console.Clear();

                // Deal new cards for those Players who wants
                Dealer.NewCardPlayer(Stakeholders);
                // The House takes a new card if hand is less than 17
                Dealer.NewCardDealer(Stakeholders);

                /* Debug print to control that the scores are set corroect
                foreach (var item in dealer.Points)
                {
                    Console.WriteLine(item + "\n");
                } */
            }   

            // Check if the game still is live, if it is victories and draws are determined
            if (Dealer.Game)
            {
                Dealer.Scores(Stakeholders);
            }
        }

        public void PlayAgain()
        {
            string stay = "";
            int who = 1;

            // As long as it is not only The House left
            if (Stakeholders.Count != 1)
            {
                // Add a new and freshly shuffled deck of cards for another round
                Dealer.DealersDeck.NewDeck();
                // Clear The Houses hand
                Stakeholders[0].MyHand.Hand.Clear();

                // Long pause so the Players have time to see who won and/or lost
                Thread.Sleep(3000);
                Console.Clear();

                Console.WriteLine(Stakeholders[who].Name + " do you want to stay for another round (Y/N)? ");
                stay = Console.ReadLine().ToUpper();
            }

            // As long as there is Players at the table (The House does not count)
            while (Stakeholders.Count != 1)
            {
                if (stay == "N")
                {
                    Stakeholders.RemoveAt(who);

                    // As long as there is Players left
                    if (who < Stakeholders.Count)
                    {
                        Console.WriteLine(Stakeholders[who].Name + " do you want to stay for another round (Y/N)? ");
                        stay = Console.ReadLine().ToUpper();
                    }

                    // If there is NO Players left, stop asking
                    else break;
                }

                if (stay == "Y")
                {
                    // Clear the hand of the Player who choose to play again
                    Stakeholders[who].MyHand.Hand.Clear();
                    NewRound = true;
                    who++;

                    // As long as there is Players left
                    if (who < Stakeholders.Count)
                    {
                        Console.WriteLine(Stakeholders[who].Name + " do you want to stay for another round (Y/N)? ");
                        stay = Console.ReadLine().ToUpper();
                    }

                    // If there is NO Players left, stop asking
                    else break;
                }

                // Catch incorrect inputs
                else if (stay != "Y" && stay != "N")
                {
                    Console.WriteLine("Incorrect input, must be h or s - try again!");
                    Thread.Sleep(1000);
                    Console.Clear();

                    Console.WriteLine(Stakeholders[who].Name + " do you want to stay for another round (Y/N)? ");
                    stay = Console.ReadLine().ToUpper();
                }
            }

            Console.Clear();
        }       

        public bool NewRound { get => newRound; set => newRound = value; }
        public List<Player> Stakeholders { get => stakeholders; set => stakeholders = value; }
        public Croupier Dealer { get => dealer; set => dealer = value; }
    }
}

