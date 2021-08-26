﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Croupier
    {
        private bool deal, game, firstRound;
        private DeckOfCards dealersDeck;
        List<Player> stakeholders;

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

            if (index == 1 && FirstRound)
            {
                stakeholders[0].ShowDealersFirstCard();
                stakeholders[index].ShowHand();
            }

            else if (index == 2 && FirstRound) stakeholders[index].ShowHand();

            else
            {
                Console.Clear();
                stakeholders[0].ShowDealersFirstCard();
                for (int i = 1; i < index; i++) stakeholders[i].ShowHand();
                stakeholders[index].ShowHand();
            }

            if (stakeholders[index].SumOfHand() == 0) Deal = false;

            else if (stakeholders[index].SumOfHand() == 21)
            {
                Deal = false;
                Game = false;
            }

            else
            {
                Console.Write(stakeholders[index].Name + " - (H)it or (S)tand? ");
                input = Console.ReadLine().ToUpper();

                if (input != "H" && input != "S")
                {
                    if (index == 1 && FirstRound || index == 2 && FirstRound) Console.Clear();

                    Console.Clear();
                    Console.WriteLine("Incorrect input, must be h or s - try again!");
                }

                else
                {
                    if (input == "H")
                    {
                        if (stakeholders[index].SumOfHand() == 0) Deal = false;

                        if (Deal)
                        {
                            stakeholders[index].AddCard(dealersDeck.ShuffledDeck[0]);
                            dealersDeck.ShuffledDeck.RemoveAt(0);

                            if (index == 1 && FirstRound) Console.Clear();

                            else if (index == 2 && FirstRound) FirstRound = false;
                        }
                    }

                    else if (input == "S")
                    {
                        if (index == 1 && FirstRound)
                        {
                            Console.Clear();
                            stakeholders[0].ShowDealersFirstCard();
                            stakeholders[1].ShowHand();
                        }

                        //Console.WriteLine("No more cards for " + stakeholders[index].Name + "\n");
                        Deal = false;
                    }
                }
            }
        }

        public void NewCardDealer(List<Player> stakeholders)
        {
            if (Game)
            {
                Console.Clear();
                stakeholders[0].ShowHand();
                for (int i = 1; i < stakeholders.Count; i++) stakeholders[i].ShowHand();

                while (stakeholders[0].SumOfHand() <= 17 && stakeholders[0].SumOfHand() != 0)
                {
                    Console.WriteLine("Hit enter to see The House's next card\n");
                    Console.ReadKey();
                    stakeholders[0].AddCard(dealersDeck.ShuffledDeck[0]);
                    dealersDeck.ShuffledDeck.RemoveAt(0);
                    Console.Clear();
                    stakeholders[0].ShowHand();
                    for (int i = 1; i < stakeholders.Count; i++) stakeholders[i].ShowHand();
                }
            }
        }

        public bool Deal { get => deal; set => deal = value; }
        public bool Game { get => game; set => game = value; }
        public bool FirstRound { get => firstRound; set => firstRound = value; }
    }
}

