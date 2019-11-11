using System;
using System.Collections.Generic;


namespace Blackjack
{
    /*
    This algorithm can be translated into the Main() routine

        Let money = 100
        while (true):
            do {
                Ask the user to enter a bet
                Let bet = the user's response
            } while bet is < 0 or > money
            if bet is 0:
                Break out of the loop
            Let userWins = PlayBlackjack()
            if userWins:
                Pay off the user's bet (money = money + bet)
            else
                Collect the user's bet (money = money - bet)
            If money == 0:
                Break out of the loop.


    The PlayBlackjack function:

        Create and shuffle a deck of cards
        Create two BlackjackHands, userHand and dealerHand
        Deal two cards into each hand
        if dealer has blackjack
            User loses and the game ends now
        If user has blackjack
            User wins and the game ends now
        Repeat:
            Display all the cards in the user's hand
            Display the user's total
            Display the dealers face-up card, i.e. dealerHand.getCard(0)
            Ask if user wants to hit or stand 
            Get user's response, and make sure it's valid

            if user stands:
                break out of loop
            if user hits:
                Give user a card
                if userHand.getBlackjackValue() > 21:
                    User loses and the game ends now
        while dealerHand.getBlackJackValue() <= 16 :
            Give dealer a card
            if dealerHand.getBlackjackValue() > 21:
                User wins and game ends now
        if dealerHand.getBlackjackValue() >= userHand.getBlackjackValue()
            User loses
        else
            User wins
    */

    class Program
    {
        static void Main(string[] args)
        {
            int money = 100;
            int bet;
            bool userWins;

            while (true)
            {
                do
                {
                    Console.WriteLine("Please enter a bet!");
                    bet = Convert.ToInt32(Console.ReadLine());
                }
                while (bet < 0 || bet > money);
                if (bet == 0) break;
                userWins = PlayBlackjack();
                if (userWins)
                {
                    money += bet;
                    Console.WriteLine(money);
                }
                else
                {
                    money -= bet;
                    Console.WriteLine(money);
                }
                if (money == 0)
                {
                    break;
                }
            }
        }


        static bool PlayBlackjack()
        {
            var deck = new Deck();
            deck.Shuffle();

            var dealerHand = new BlackjackHand();
            var userHand = new BlackjackHand();

            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
            Console.WriteLine("\nDealer's face card is " + dealerHand.GetCard(0));

            userHand.AddCard(deck.DealCard());
            userHand.AddCard(deck.DealCard());
            Console.Write("\nUser has" + " " + userHand.GetBlackjackValue() + "\n");
            userHand.DisplayCards();

            if (userHand.GetBlackjackValue() == 21)
            {
                Console.WriteLine("\nCongratulations you win!");
                userHand.Clear();
                return true;

            }
            else if (dealerHand.GetBlackjackValue() == 21)
            {
                Console.WriteLine("\n You Lose");
                userHand.Clear();
                return false;
            }
            
            while (true)
            {
                if (userHand.GetBlackjackValue() < 21)
                {
                    int choice = GetChoice();
                    switch (choice)
                    {
                        case 1:
                            {
                                userHand.AddCard(deck.DealCard());
                                userHand.DisplayCards();
                                break;
                            }
                        case 2:
                            {
                                Console.Write("\n" + userHand.GetBlackjackValue());
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Please enter a valid choice");
                                break;
                            }
                    }
                }
                else 
                {
                    Console.WriteLine("You lose!");
                    Console.WriteLine(dealerHand.GetBlackjackValue());
                    return false;
                }
                // Console.WriteLine("\nDealer has " + dealerHand.GetBlackjackValue() + "/21");
                while (dealerHand.GetBlackjackValue() <= 16)
                {
                    dealerHand.AddCard(deck.DealCard());
                    // Console.Write("\nDealer has" + " " + dealerHand.GetCardCount() + " ");

                    if (dealerHand.GetBlackjackValue() > 21)
                    {
                        Console.WriteLine("\nDealer busts... User wins!");
                        Console.WriteLine("\nDealer has " + dealerHand.GetBlackjackValue() + "/21");
                        return true;
                    }
                    else if (dealerHand.GetBlackjackValue() >= userHand.GetBlackjackValue())
                    {
                        Console.WriteLine("\nDealer Wins!");
                        Console.WriteLine("\nDealer has " + dealerHand.GetBlackjackValue() + "/21");
                        return false;

                    }
                    else if (userHand.GetBlackjackValue() >= dealerHand.GetBlackjackValue())
                    {
                        Console.WriteLine("\nUser wins!");
                        Console.WriteLine("\nDealer has " + dealerHand.GetBlackjackValue() + "/21");
                        return true;
                    }
                }
                
            }
        }
        static int GetChoice()
        {
            Console.WriteLine("Choose an action");
            Console.WriteLine("1) Hit");
            Console.WriteLine("2) Stand");

            int choice;
            bool selected = false;

            do
            {
                Console.Write("Please enter your selection");

                if (Int32.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice > 0 && choice < 3)
                    {
                        selected = true;
                    }
                }
                if (!selected)
                {
                    Console.WriteLine("Invalid input");
                }
            } while (!selected);

            return choice;
        }
    }
}
