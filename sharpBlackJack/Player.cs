using System;
using System.Collections.Generic;
using System.Linq;

namespace sharpBlackJack
{
    public class Player
    {
        private string name;
        private HandOfCards myHand;

        // Create a new Croupier object with its own HandOfCards object 
        public Player(string name)
        {
            Name = name;
            // Create the a new HandOfCards object and give This instance of Player as an argument
            MyHand = new HandOfCards(this);
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
            // Add new Card to Hand
            MyHand.Hand.Add(card);
        }

        public string Name { get => name; set => name = value; }
        public HandOfCards MyHand { get => myHand; set => myHand = value; }
    }
}

