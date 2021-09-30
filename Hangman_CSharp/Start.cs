
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman_CSharp
{
    class Start
    {


        bool isGuessed = false;
        public static void StartGame(int Language)
        {
            StringBuilder guessedWrong = new StringBuilder();
            Start ob = new Start();
            Players player1 = new Players();
            player1.playerName = GetPlayerName(1);

            player1.tries = 10;
            int ifGuessRight = 0;
            var secretWord = Language switch
            {
                1 => GetSecretWord_English(),
                2 => GetSecretWord_Swedish(),

            };

            char[] guessedRight = new char[secretWord.Length];
            for (int i = 0; i < secretWord.Length; i++)
            {
                guessedRight[i] = Convert.ToChar("_");
            }

            for (player1.tries = 10; player1.tries > 0; player1.tries--)
            {
                DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName);
                string guess = GetGuessFromPlayer(guessedRight, guessedWrong);
                bool ifGuessExists = IfGuessExists(guess, guessedRight, guessedWrong);
                if (ifGuessExists) { player1.tries += 1; }
                else
                {
                    ifGuessRight = IfGuessRight(secretWord, guess);
                    switch (ifGuessRight)
                    {
                        case 0:  guessedWrong.Append(guess) ; guessedWrong.AppendFormat(", "); break;
                        case 1: guessedRight = UpdateguessedRight(guessedRight, guess, secretWord); break;
                        case 2:
                            player1.tries = 0;
                            ob.isGuessed = true;
                            DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName);
                            break;
                        default: guessedWrong.Append(guess); guessedWrong.AppendFormat(", ");  break;

                    }

                }

                if (!ob.isGuessed && !ifGuessExists)
                {
                    Refresh();
                }
                if (player1.tries == 1 && (secretWord != guessedRight.ToString()))
                { Console.WriteLine("\a \nGAME OVER\n" + player1.playerName + " Loses!! \n"); }
            }




        }
        public static void Refresh()
        {

            Console.Clear();
            Program.TitleMenu();
        }

        static string GetSecretWord_English()
        {
            WordsRepository ob = new WordsRepository();
            var rand = new Random();
            int wordIndex = rand.Next(0, ob.wordsInEnglish.Length);

            return ob.wordsInEnglish[wordIndex];
        }
        static string GetSecretWord_Swedish()
        {
            WordsRepository ob = new WordsRepository();
            var rand = new Random();
            int wordIndex = rand.Next(0, ob.wordsInSwedish.Length);

            return ob.wordsInSwedish[wordIndex];
        }
        static void DisplayToPlayer(string secretWord, bool isGuessed, int tries, char[] guessedRight, StringBuilder guessedWrong, string playerName)
        {
            if (!isGuessed)
            {
                if (guessedWrong.Length > 0)
                {
                    Console.Write("\nplayerName: " + playerName + "\tTriesLeft: " + tries + "\nWrong Guesses: ( ");

                   // foreach (string item in guessedWrong.) {
                        Console.Write(guessedWrong.ToString());// }
                    Console.Write(")\n\nGuess the word: ");
                }

                else { Console.Write("\nplayerName: " + playerName + "\tTriesLeft: " + tries + "\nGuess the word: "); }

                for (int i = 0; i < secretWord.Length; i++)
                {
                    Console.Write(guessedRight[i] + "  ");
                }
                Console.Write("\n");
            }
            else
            {
                Console.WriteLine("\a " + playerName + " winns!! \nThe word was: " + secretWord);
            }

        }

        static int IfGuessRight(string secretWord, string guess)
        {

            if (guess.Length == 1)
            {
                return secretWord.Contains(guess) ? 1 : 0;
            }
            else
            {
                return secretWord == guess ? 2 : 3;
            }

        }
        static char[] UpdateguessedRight(char[] guessedRight, string guess, string secretWord)
        {

            char[] sWord = secretWord.ToCharArray();

            for (int i = 0; i < secretWord.Length; i++)
            {
                if (sWord[i] == guess.ToCharArray()[0])
                    guessedRight[i] = sWord[i];
            }

            return guessedRight;
        }

        static bool IfGuessExists(string guess, char[] guessedRight, StringBuilder guessedwrong)
        {
            bool result = false;

            if (guess.Length == 1)
            {
                foreach (char item in guessedRight)
                {

                    if (guess.Equals(item.ToString()))
                    {
                        result = true;
                        break;
                    }
                }
                
                if (guessedwrong.Equals(guess))
                {
                    result = true;                   
                }

                //foreach (string item in guessedwrong)
                //{
                //    if (guess.Equals(item))
                //    {
                //        result = true;
                //        break;
                //    }
                //}
            }
            else
            {
                if (guessedwrong.Equals(guess))
                {
                    result = true;
                }
                //foreach (string item in guessedwrong)
                //{
                //    if (guess.Equals(item))
                //    {
                //        result = true;
                //        break;
                //    }
                //}
            }
            return result;
        }
        static bool IsAlphabets(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return false;

            for (int i = 0; i < inputString.Length; i++)
                if (!char.IsLetter(inputString[i]))
                    return false;
            return true;
        }
        static string GetGuessFromPlayer(char[] guessedRight, StringBuilder guessedwrong)
        {
            string guess = "";
            bool ChkAlphabet = false;

            while (!ChkAlphabet)
            {
                guess = Program.ChkUserinput(Console.ReadLine());
                ChkAlphabet = IsAlphabets(guess);
                if (!ChkAlphabet) { Console.WriteLine("\n Invalid! Enter alphabetic guess"); }
            }
            if (IfGuessExists(guess, guessedRight, guessedwrong))
            { Console.WriteLine("\n Already guessed!! Try again. "); }

            return guess;
        }

        static string GetPlayerName(long n)
        {
            string playerName;

            Console.WriteLine("\n Enter Player Name ");
            playerName = Console.ReadLine();
            playerName = playerName == "" ? "Player" + n.ToString() : playerName;
            return playerName;
        }

    }
}
