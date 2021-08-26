using System;
using System.Collections.Generic;

namespace BlackJack
{
    class DeckOfCards
    {
        private List<string> shuffledDeck = new List<string>();

        public DeckOfCards()
        {
            string suites = "♠♥♦♣";
            string ranks = "23456789XJQKA";
            Random rnd = new Random();

            while (shuffledDeck.Count < 52)
            {
                string randSuit = suites[rnd.Next(0, 4)].ToString();
                string randRank = ranks[rnd.Next(0, 13)].ToString();
                string card = randSuit + (randRank == "X" ? "10" : randRank);

                if (!shuffledDeck.Contains(card)) shuffledDeck.Add(card);
            }
        }

        public List<String> ShuffledDeck { get => shuffledDeck; set => shuffledDeck = value; }
    }
}
