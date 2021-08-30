using System;
using System.Collections.Generic;

namespace sharpBlackJack
{
    public class HandOfCards
    {
        private List<string> hand = new List<string>();
        private Player player;
        private int sumOfCards = new int();

        public HandOfCards(Player pl)
        {
            // Create a pointer to the instance of the Player objekt that is called
            player = pl;
        }

        public void ShowDealersFirstCard()
        {
            Console.WriteLine(player.Name + "'s hand:");
            Console.WriteLine(hand[0] + " *\n");
        }

        public void ShowCards()
        {
            Console.WriteLine(player.Name + "'s hand:");

            foreach (var card in player.MyHand.Hand) Console.Write(card + " ");

            int value = SumCards();

            // Show if Player or The House is Bust, or if Player got Black Jack
            if (value == 21 && player.Name != "The House") Console.WriteLine("\n" + player.Name + " - You got BLACK JACK!\n");

            else if (value == 0 && player.Name != "The House") Console.WriteLine("\n" + player.Name + " - You are BUST!\n");

            else if (value == 0 && player.Name == "The House") Console.WriteLine("\nThe House is BUST!\n");

            else Console.WriteLine("\n");
        }

        public int SumCards()
        {
            int aces = 0;
            sumOfCards = 0;

            foreach (var card in player.MyHand.Hand)
            {
                if (card.Remove(0, 1) == "A")
                {
                    // First "A"ce counts as 11
                    sumOfCards = sumOfCards + 11;
                    aces++;

                    // If more than one "A"ce, "A"ce counts as 1
                    if (sumOfCards > 21 && aces > 1)
                    {
                        if (aces == 2) sumOfCards = sumOfCards - 10;

                        else if (aces == 3) sumOfCards = sumOfCards - 20;

                        else sumOfCards = sumOfCards - 30;
                    }
                }

                // "J"ack, "Q"ueen, "K"ing, and "10" counts as 10
                else if (card.Remove(0, 1) == "J" || card.Remove(0, 1) == "Q" ||
                         card.Remove(0, 1) == "K" || card.Remove(0, 1) == "10") sumOfCards = sumOfCards + 10;

                // '2', '3', '4', '5', '6', '7', '8', and '9' is converted from char to int  
                else sumOfCards = sumOfCards + (int)Char.GetNumericValue(card[1]);
            }

            // If Player's sumOfCards is more than 21 - sumOfCards = 0 to signal Player is Bust
            if (sumOfCards > 21) return 0;

            else return sumOfCards;
        }

        public List<string> Hand { get => hand; set => hand = value; }
    }
}