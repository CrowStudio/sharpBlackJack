using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace sharpBlackJack
{
    class Croupier
    {
        private bool deal, game, firstTurn;
        private List<int> points = new List<int>() {0, 0, 0, 0, 0, 0};
        private DeckOfCards dealersDeck;

        public Croupier()
        {
            DealersDeck = new DeckOfCards();
        }

        public void DealHands(List<Player> stakeholders)
        {
            Game = true;

            while (DealersDeck.ShuffledDeck.Count > 52 - (stakeholders.Count() * 2))
            {
                foreach (var stakholder in stakeholders)
                {
                    stakholder.AddCard(DealersDeck.ShuffledDeck[0]);
                    DealersDeck.ShuffledDeck.RemoveAt(0);
                }
            }
        }

        public void NewCardPlayer(List<Player> stakeholders)
        {
            for (int i = 1; i < stakeholders.Count; i++)
            {
                deal = true;
                firstTurn = true;

                if (stakeholders[i].SumOfHand() == 21)
                {
                    Game = false;
                    break;
                }

                while (Game && deal) HitOrStand(stakeholders, i);
            }
        }

        private void HitOrStand(List<Player> stakeholders, int index)
        {
            string input;
            int sum;

            Points[index] = sum = stakeholders[index].SumOfHand();

            if (sum == 0 || sum == 21) deal = false;

            else
            {
                Console.Clear();

                if (index == 1 && firstTurn)
                {
                    stakeholders[0].ShowDealersFirstCard();
                    stakeholders[index].ShowHand();
                }

                else if (index == 2 && firstTurn)
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
                    if (input == "H")
                    {
                        Points[index] = sum = stakeholders[index].SumOfHand();

                        if (sum == 0) deal = false;

                        if (deal)
                        {
                            stakeholders[index].AddCard(DealersDeck.ShuffledDeck[0]);
                            DealersDeck.ShuffledDeck.RemoveAt(0);

                            if (index == 1 && firstTurn) Console.Clear();

                            else if (index == 2 && firstTurn) firstTurn = false;

                            Points[index] = sum = stakeholders[index].SumOfHand();
                        }
                    }

                    else if (input == "S")
                    {
                        Points[index] = sum = stakeholders[index].SumOfHand();

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

                Points[0] = sum = stakeholders[0].SumOfHand();

                while (sum <= 17 && sum != 0)
                {
                    Console.WriteLine("Hit enter to see The House's next card\n");
                    Console.ReadKey();

                    stakeholders[0].AddCard(DealersDeck.ShuffledDeck[0]);
                    DealersDeck.ShuffledDeck.RemoveAt(0);

                    Console.Clear();
                    stakeholders[0].ShowHand();
                    for (int i = 1; i < stakeholders.Count; i++) stakeholders[i].ShowHand();

                    Points[0] = sum = stakeholders[0].SumOfHand();
                }
            }
        }

        public void Scores(List<Player> stakeholders)
        {
            int houseWin = 0;

            for (int i = 1; i < stakeholders.Count; i++)
            {
                if (Points[i] > Points[0])
                {
                    Points[i] = 3;
                    houseWin = 0;
                }

                else if (Points[i] == Points[0])
                {
                    Points[i] = 2;
                    houseWin = 0;
                }

                else if (Points[i] != 0 && Points[i] < Points[0])
                {
                    Points[i] = 1;
                    houseWin++;
                }
            }

            for (int i = 1; i < stakeholders.Count; i++)
            {
                if (Points[i] == 3) Console.WriteLine("\n" + stakeholders[i].Name + " - Congratulations you WIN!");

                else if (Points[i] == 2 && Points[0] != 0) Console.WriteLine("\n" + stakeholders[i].Name + " - No one wins, it's a PUSH!");

                else if (Points[i] == 1 && houseWin > 0) Console.WriteLine("\n" + stakeholders[i].Name + " - You LOST!");
            }
        }

        public bool Game { get => game; set => game = value; }
        public List<int> Points { get => points; set => points = value; }
        public DeckOfCards DealersDeck { get => dealersDeck; set => dealersDeck = value; }
    }
}

