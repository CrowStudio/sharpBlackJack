using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Player
    {
        private string name;
        private HandOfCards myHand;

        public Player(string name)
        {
            Name = name;
            MyHand = new HandOfCards(Name, this);
        }

        public void ShowDealersFirstCard()
        {
            MyHand.ShowDealersFirstCard();
        }

        public void ShowHand()
        {
            MyHand.ShowCards();
            // Debug print to control that the sum of cards is correct
            //Console.WriteLine(this.SumOfHand());
        }

        public int SumOfHand()
        {
            return MyHand.SumCards();
        }

        public void AddCard(string card)
        {
            MyHand.Hand.Add(card);
        }

        public string Name { get => name; set => name = value; }
        public HandOfCards MyHand { get => myHand; set => myHand = value; }
    }
}

