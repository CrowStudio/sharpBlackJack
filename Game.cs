using System;
using System.Collections.Generic;
using System.Threading;

namespace BlackJack
{
    class Game
    {
        private bool anotherRound;
        private List<Player> stakeholders = new List<Player>();
        Croupier dealer = new Croupier();

        public Game()
        {
            Stakeholders.Add(new Player("The House"));
            AnotherRound = true;
        }

        public void JoinGame()
        {
            int addPlayer = 5;
            bool join = true;
            string name, yn;

            Console.WriteLine("Welcome to this table of Black Jack");

            // While add player
            while (AnotherRound && join && addPlayer > Stakeholders.Count - 1)
            {
                Console.Clear();
                Console.WriteLine("There is room for " + (addPlayer - Stakeholders.Count + 1) + " more Player\n");
                Console.WriteLine("Anyone who wants to join (Y/N)? ");
                yn = Console.ReadLine().ToUpper();

                if (yn == "Y")
                {
                    Console.WriteLine("\nWhat is your name: ");
                    name = Console.ReadLine();

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

            if (Stakeholders.Count == 1) AnotherRound = false;
        }

        public void StartDeal()
        {
            // Deal cards for all players and the house
            Dealer.DealHands(Stakeholders);
            Stakeholders[0].ShowDealersFirstCard();
            //dealer.InitialScores(stakeholders, 0);

            for (int i = 1; i < Stakeholders.Count; i++)
            {
                Stakeholders[i].ShowHand();
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
                Console.WriteLine("Hit a key for cards!");
                Console.ReadKey();
                Console.Clear();

                // Deal new cards to those players who wants
                Dealer.NewCardPlayer(Stakeholders);
                Dealer.NewCardDealer(Stakeholders);

                /* Debug print to control that the scores are set corroect
                foreach (var item in dealer.Points)
                {
                    Console.WriteLine(item + "\n");
                } */
            }   
            if (Dealer.Game)
            {
                Dealer.Scores(Stakeholders);
            }
        }

        public void PlayAgain()
        {
            string stay;
            int who = 1;

            if (Stakeholders.Count == 1) Console.WriteLine("Hit any key to QUIT");

            else
            {
                Dealer.DealersDeck.NewDeck();

                Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine(Stakeholders[who].Name + " do you want to stay for another round (Y/N)? ");
            }

            stay = Console.ReadLine().ToUpper();

            while (Stakeholders.Count != 1)
            {
                if (stay == "N")
                {
                    Stakeholders[who].MyHand.Hand.Clear();
                    Stakeholders.RemoveAt(who);

                    if (who < Stakeholders.Count)
                    {
                        Console.WriteLine(Stakeholders[who].Name + " do you want to stay for another round (Y/N)? ");
                        stay = Console.ReadLine().ToUpper();
                    }

                    else break;
                }

                if (stay == "Y")
                {
                    Stakeholders[who].MyHand.Hand.Clear();
                    AnotherRound = true;
                    who++;

                    if (who < Stakeholders.Count)
                    {
                        Console.WriteLine(Stakeholders[who].Name + " do you want to stay for another round (Y/N)? ");
                        stay = Console.ReadLine().ToUpper();
                    }

                    else break;
                }

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

        public bool AnotherRound { get => anotherRound; set => anotherRound = value; }
        public List<Player> Stakeholders { get => stakeholders; set => stakeholders = value; }
        public Croupier Dealer { get => dealer; set => dealer = value; }
    }
}

