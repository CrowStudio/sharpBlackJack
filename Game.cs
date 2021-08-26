using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Game
    {
        private List<Player> stakeholders = new List<Player>();
        Croupier dealer = new Croupier();

        public Game()
        {
            stakeholders.Add(new Player("The House"));
        }

        public void JoinGame()
        {
            /*          int addPlayer = 4;
                        bool join = true;
                        string name, yn;

                        Console.WriteLine("Welcome to this table of Black Jack");

                        // While add player
                        while (join && addPlayer > stakeholders.Count() - 1)
                        {
                            Console.Clear();
                            Console.WriteLine("There is room for " + (addPlayer - stakeholders.Count() + 1) + " more Player\n");
                            Console.WriteLine("Anyone who wants to join (Y/N)? ");
                            yn = Console.ReadLine().ToUpper();

                            if (yn == "Y")
                            {
                                Console.WriteLine("\nWhat is your name: ");
                                name = Console.ReadLine();

                                stakeholders.Add(new Player(name));

                                Console.WriteLine("Welcome " + name + ", please take a seat while waiting for other Players to join.\n");
                                System.Threading.Thread.Sleep(1000);
                            }

                            else if (yn == "N")
                            {
                                Console.Clear();
                                join = false;
                            }

                            else
                            {
                                Console.WriteLine("Incorrect input, must be y or n - try again!");
                                System.Threading.Thread.Sleep(1000);
                            }

                            System.Threading.Thread.Sleep(1000);
                            Console.Clear();
                        }
                        */
            /* Comment code above, and use code below for testing, so you don't have to add Players manually*/
            stakeholders.Add(new Player("Kalle"));
            stakeholders.Add(new Player("Lisa"));
            stakeholders.Add(new Player("Johan"));
            stakeholders.Add(new Player("Pia"));
        }

        public void StartDeal()
        {
            // Deal cards for all players and the house
            dealer.DealHands(stakeholders);
            stakeholders[0].ShowDealersFirstCard();
            //dealer.InitialScores(stakeholders, 0);

            for (int i = 1; i < stakeholders.Count; i++)
            {
                stakeholders[i].ShowHand();
                if (stakeholders[i].SumOfHand() == 21)
                {
                    dealer.Game = false;
                    break;
                }
            }
        }

        public void Play()
        {
            if (dealer.Game)
            {
                Console.WriteLine("Hit a key for cards!");
                Console.ReadKey();
                Console.Clear();

                // Deal new cards to those players who wants
                dealer.NewCardPlayer(stakeholders);
                dealer.NewCardDealer(stakeholders);

                /* Debug print to control that the scores are set corroect
                foreach (var item in dealer.Points)
                {
                    Console.WriteLine(item + "\n");
                } */
            }   
            if (dealer.Game)
            {
                dealer.Scores(stakeholders);
            }
        }
    }
}

