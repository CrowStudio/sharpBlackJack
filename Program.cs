/* Hiarchy:
 *     Program:
 *     |__
 *        Game
 *        |__
 *        |  Croupier
 *        |  |
 *        |  DeckOfCards
 *        |__
 *           Player
 *           |
 *           HandOfCards
 * 
 * Black Jack rules - 1-4 players
 * 2-10 = 2-10
 * J, Q, K = 10
 * E = 11, if sum is more than 21 and more than one ace is present, the new ace counts as 1.
 * The House starts with one card open and one faced down.
 * Every Player gets two cards open, if no one got 21 the game continues.
 * Starting with the first Player who joined, the Player gets the question if they want to Hit or Stand.
 *  - If the Player choose to Hit they gets a new card.
 *  - If a Player gets more than 21 the Player gets Bust.
 *  - If the Player choose to Stand it's time for the next Player in line.
 * When the last Plyer is finished it's the House's turn. 
 *  - If the House cards sum is less than 17, the House gets a new card.
 *      - If the House gets 21 and no other Player has 21, the House wins!.
 *      - If the House gets more than the Player, the House wins!
 *      - If the House and the Player gets the same sum, it's a Push (draw) and no one wins!
 *      - If the House gets less than the Player, the Player wins!
 *      - If the House gets more than 21, the House gets Bust.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Game blackJack = new Game();

            blackJack.JoinGame();
            blackJack.StartDeal();
            blackJack.Play();

        }
    }
}

