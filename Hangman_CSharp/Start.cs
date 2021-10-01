
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Text;

namespace Hangman_CSharp
{
    class Start
    {
        static string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        bool isGuessed = false;
        public static void StartGame(int Language) // Starts Game
        {
            WordsRepository.SaveFile(Language);
            int maxTries = 10;
            StringBuilder guessedWrong = new StringBuilder();
            Start ob = new Start();
            Players player1 = new Players();
            player1.playerName = GetPlayerName(1);

            
            player1.tries = 10;
            int ifGuessRight = 0;
            var secretWord = GetSecretWord(Language);  // Gets secret word
            char[] secretWordChar = secretWord.ToCharArray();
            char[] guessedRight = new char[secretWord.Length];
            for (int i = 0; i < secretWord.Length; i++)
            {
                guessedRight[i] = Convert.ToChar("_");
            }
            SystemSounds.Asterisk.Play();
            for (player1.tries = maxTries; player1.tries > 0; player1.tries--)
            {
                if (!IfGussed(secretWord,guessedRight))
                {
                    DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName); // Displays output to user
                    string guess = GetGuessFromPlayer(guessedRight, guessedWrong); // Takes input from user as guess
                    bool ifGuessExists = IfGuessExists(guess, guessedRight, guessedWrong); // checks if the user has already guessed this input 
                    if (ifGuessExists) { player1.tries += 1; SystemSounds.Asterisk.Play(); Console.WriteLine("\n Already guessed!! Try again. "); } // no try is consumed
                    else
                    {
                        ifGuessRight = IfGuessRight(secretWord, guess); // checks if user guessed right 
                        switch (ifGuessRight)
                        {
                            case 0: guessedWrong.Append(guess); guessedWrong.AppendFormat(","); break;
                            case 1: guessedRight = UpdateguessedRight(guessedRight, guess, secretWord); break; // if the secret word contains letter guessed by user
                            case 2:// if user guess full word successfully before tries end
                                player1.tries = 0;
                                ob.isGuessed = true;
                                DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName);
                                break;
                            default: guessedWrong.Append(guess); guessedWrong.AppendFormat(","); break; // if user entered wrong

                        }

                    }

                    if (!ob.isGuessed && !ifGuessExists)
                    {
                        Refresh(); // runtime refresh console
                    }
                    if (player1.tries == 1)// if user failed to guess after using all tries
                    { Console.WriteLine("\a \nGAME OVER\n" + player1.playerName + " Loses!! \n"); } 
                }
                else // if user guesses word by entering letter one by one and consumed less tries than maxTries
                {
                    player1.tries = 0;
                    ob.isGuessed = true;
                    DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName);
                    break;
                }
            }




        }
        
        // Runtime refresh screen
        public static void Refresh()
        {

            Console.Clear();
            Program.TitleMenu();
        }

        // Get secret word from WordRepository
        static string GetSecretWord(int Language)
        {
            var rand = new Random();
            int wordIndex = rand.Next(0, WordsRepository.GetWords(Language).Length);

            return WordsRepository.GetWords(Language)[wordIndex];
        }
        // Display output on Console
        static void DisplayToPlayer(string secretWord, bool isGuessed, int tries, char[] guessedRight, StringBuilder guessedWrong, string playerName)
        {
            if (!isGuessed)
            {
                if (guessedWrong.Length > 0)
                {
                    string[] guessWrong = guessedWrong.ToString().Split(",");
                    Console.Write("\nplayerName: " + playerName + "\tTriesLeft: " + tries + "\nWrong Guesses: ( ");

                    for (int i = 0; i < guessWrong.Length-1; i++)
                    {
                        var output = i==guessWrong.Length - 2? guessWrong [i]: guessWrong[i] + " , ";
                        Console.Write(output);
                    }
                    Console.Write(")\n\nGuess the word: ");
                }

                else { Console.Write("\nplayerName: " + playerName + "\tTriesLeft: " + tries + "\n\nGuess the word: "); }

                for (int i = 0; i < secretWord.Length; i++)
                {
                    Console.Write(guessedRight[i] + "  ");
                }
                Console.Write("\n");
            }
            else
            {
                SystemSounds.Asterisk.Play();
                Console.WriteLine("\n\a " + playerName + " winns!! \nThe word was: " + secretWord);
            }
            
        }

        // checks if the player guesses right word by entering letters one by one (can be somehow merged with IfGuessRight)
        static bool IfGussed(string secretWord, char[] Guessed)
        {
            string wGuessed = new String(Guessed);
            bool result= wGuessed.Equals(secretWord) ? true : false;
            return result;
        }
        // Checks if secretWord contains the user input or if it is same as user input in case user enters word 
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
        // Updates in case user correctly guesses a letter
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
        // check is user has already guessed thi input
        static bool IfGuessExists(string guess, char[] guessedRight, StringBuilder guessedWrong)
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
                if (guessedWrong.Length > 0)
                {
                    
                    foreach (string item in guessedWrong.ToString().Split(","))
                    {
                        if (guess.Equals(item))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (guessedWrong.Length > 0)
                {
                    //string[] guessWrong = guessedWrong.Remove(guessedWrong.ToString().LastIndexOf(","), 1).ToString().Split(",");
                    foreach (string item in guessedWrong.ToString().Split(","))
                    {
                        if (guess.Equals(item))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            
            return result;
        }

        // check if the input is alphabetic
        static bool IsAlphabets(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return false;

            for (int i = 0; i < inputString.Length; i++)
                if (!char.IsLetter(inputString[i]))
                    return false;
            return true;
        }

        // Takes guess from user only alphabets. 
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
            return guess;           
        }
        // Gets name from user or already sets as player one if user do not provide name
        static string GetPlayerName(long n)
        {
            string playerName;
            SystemSounds.Asterisk.Play();
            Console.WriteLine("\n Enter Player Name ");
            playerName = Console.ReadLine();
            playerName = playerName == "" ? "Player" + n.ToString() : playerName;
            return playerName;
        }

    }
}
