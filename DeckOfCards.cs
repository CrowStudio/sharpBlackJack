using System;
using System.Collections.Generic;

namespace sharpBlackJack
{
    class DeckOfCards
    {
        private List<string> shuffledDeck = new List<string>();
        private string suites = "♠♥♦♣";
        private string ranks = "23456789XJQKA";
        private Random rnd = new Random();
        
        public DeckOfCards()
        {
            while (ShuffledDeck.Count < 52)
            {
                string randSuit = suites[rnd.Next(0, 4)].ToString();
                string randRank = ranks[rnd.Next(0, 13)].ToString();
                string card = randSuit + (randRank == "X" ? "10" : randRank);

                if (!ShuffledDeck.Contains(card)) ShuffledDeck.Add(card);
            }
        }

        public void NewDeck()
        {
            ShuffledDeck.Clear();

            while (ShuffledDeck.Count < 52)
            {
                string randSuit = suites[rnd.Next(0, 4)].ToString();
                string randRank = ranks[rnd.Next(0, 13)].ToString();
                string card = randSuit + (randRank == "X" ? "10" : randRank);

                if (!ShuffledDeck.Contains(card)) ShuffledDeck.Add(card);
            }
        }

        public List<String> ShuffledDeck { get => shuffledDeck; set => shuffledDeck = value; }
    }
}
