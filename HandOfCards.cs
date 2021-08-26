using System;
using System.Collections.Generic;

namespace BlackJack
{
    class HandOfCards
    {
        private List<string> hand = new List<string>();
        private string handBelongsTo;
        private Player player;
        private int sumOfCards = new int();

        public HandOfCards(string name, Player pl)
        {
            HandBelongsTo = name;
            player = pl;
        }

        public void ShowDealersFirstCard()
        {
            Console.WriteLine(HandBelongsTo + "'s hand:");
            Console.WriteLine(hand[0] + " *\n");
        }

        public void ShowCards()
        {
            Console.WriteLine(player.Name + "'s hand:");

            foreach (var card in player.MyHand.Hand) Console.Write(card + " ");

            int value = SumCards();

            if (value == 21 && player.Name != "The House") Console.WriteLine("\n" + player.Name + " - Congratulations you got BLACK JACK!\n");

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
                    sumOfCards = sumOfCards + 11;
                    aces++;

                    if (sumOfCards > 21 && aces > 1)
                    {
                        if (aces == 2) sumOfCards = sumOfCards - 10;

                        else if (aces == 3) sumOfCards = sumOfCards - 20;

                        else sumOfCards = sumOfCards - 30;
                    }
                }

                else if (card.Remove(0, 1) == "J" || card.Remove(0, 1) == "Q" ||
                         card.Remove(0, 1) == "K" || card.Remove(0, 1) == "10") sumOfCards = sumOfCards + 10;

                else sumOfCards = sumOfCards + (int)Char.GetNumericValue(card[1]);
            }

            if (sumOfCards > 21) return 0;

            else return sumOfCards;
        }

        public List<string> Hand { get => hand; set => hand = value; }
        public string HandBelongsTo { get => handBelongsTo; set => handBelongsTo = value; }
    }
}