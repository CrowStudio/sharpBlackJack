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
            // Create a shuffled Deck of cards
            while (ShuffledDeck.Count < 52)
            {
                string randSuit = suites[rnd.Next(0, 4)].ToString();
                string randRank = ranks[rnd.Next(0, 13)].ToString();
                string card = randSuit + (randRank == "X" ? "10" : randRank);

                // If the card does not exist in the Deck - add it to the Deck
                if (!ShuffledDeck.Contains(card)) ShuffledDeck.Add(card);
            }
        }

        public void NewDeck()
        {
            // Clear list of Cards
            ShuffledDeck.Clear();

            // Create a new shuffled Deck of cards
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
