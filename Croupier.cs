using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Croupier
    {
        private bool deal, game, firstRound;
        private int[] points = new int[5];
        private DeckOfCards dealersDeck;

        public Croupier()
        {
            dealersDeck = new DeckOfCards();
        }

        public void DealHands(List<Player> stakeholders)
        {
            Game = true;

            while (dealersDeck.ShuffledDeck.Count > 52 - (stakeholders.Count() * 2))
            {
                foreach (var stakholder in stakeholders)
                {
                    stakholder.AddCard(dealersDeck.ShuffledDeck[0]);
                    dealersDeck.ShuffledDeck.RemoveAt(0);
                }
            }
        }

        public void NewCardPlayer(List<Player> stakeholders)
        {
            for (int i = 1; i < stakeholders.Count; i++)
            {
                Deal = true;
                FirstRound = true;

                if (stakeholders[i].SumOfHand() == 21)
                {
                    Game = false;
                    break;
                }

                while (Game && Deal) HitOrStand(stakeholders, i);
            }
        }

        private void HitOrStand(List<Player> stakeholders, int index)
        {
            string input;
            int sum;

            Console.Clear();

            points[index] = sum = stakeholders[index].SumOfHand();

            if (sum == 0 || sum == 21) Deal = false;

            else
            {
                if (index == 1 && FirstRound)
                {
                    stakeholders[0].ShowDealersFirstCard();
                    stakeholders[index].ShowHand();
                }

                else if (index == 2 && FirstRound)
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
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }

                else
                {
                    if (input == "H")
                    {
                        points[index] = sum = stakeholders[index].SumOfHand();

                        if (sum == 0) Deal = false;

                        if (Deal)
                        {
                            stakeholders[index].AddCard(dealersDeck.ShuffledDeck[0]);
                            dealersDeck.ShuffledDeck.RemoveAt(0);

                            if (index == 1 && FirstRound) Console.Clear();

                            else if (index == 2 && FirstRound) FirstRound = false;

                            points[index] = sum = stakeholders[index].SumOfHand();
                        }
                    }

                    else if (input == "S")
                    {
                        points[index] = sum = stakeholders[index].SumOfHand();

                        Deal = false;
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

                points[0] = sum = stakeholders[0].SumOfHand();

                while (sum <= 17 && sum != 0)
                {
                    Console.WriteLine("Hit enter to see The House's next card\n");
                    Console.ReadKey();

                    stakeholders[0].AddCard(dealersDeck.ShuffledDeck[0]);
                    dealersDeck.ShuffledDeck.RemoveAt(0);

                    Console.Clear();
                    stakeholders[0].ShowHand();
                    for (int i = 1; i < stakeholders.Count; i++) stakeholders[i].ShowHand();

                    points[0] = sum = stakeholders[0].SumOfHand();
                }
            }
        }

        public void Scores(List<Player> stakeholders)
        {
            int houseWin = 0;

            for (int i = 1; i < stakeholders.Count; i++)
            {
                if (points[i] > points[0])
                {
                    points[i] = 3;
                    houseWin = 0;
                }

                else if (points[i] == points[0])
                {
                    points[i] = 2;
                    houseWin = 0;
                }

                else if (points[i] != 0 && points[i] < points[0])
                {
                    points[i] = 1;
                    houseWin++;
                }
            }

            for (int i = 1; i < stakeholders.Count; i++)
            {
                if (points[i] == 3) Console.WriteLine("\n" + stakeholders[i].Name + " - Congratulations you WIN!");

                else if (points[i] == 2 && points[0] != 0) Console.WriteLine("\n" + stakeholders[i].Name + " - No one wins, it's a PUSH!");

                else if (points[i] == 1 && houseWin > 0) Console.WriteLine("\n" + stakeholders[i].Name + " - You LOST!");
            }
        }

        public bool Deal { get => deal; set => deal = value; }
        public bool Game { get => game; set => game = value; }
        public bool FirstRound { get => firstRound; set => firstRound = value; }
    }
}

