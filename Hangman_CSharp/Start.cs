
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
        public static void StartGame(int Language)
        {
            StringBuilder guessedWrong = new StringBuilder();
            Start ob = new Start();
            //WordsRepository.SaveFile(Language);
            Players player1 = new Players();
            player1.playerName = GetPlayerName(1);

            
            player1.tries = 10;
            int ifGuessRight = 0;
            var secretWord = GetSecretWord(Language);
            char[] secretWordChar = secretWord.ToCharArray();
            char[] guessedRight = new char[secretWord.Length];
            for (int i = 0; i < secretWord.Length; i++)
            {
                guessedRight[i] = Convert.ToChar("_");
            }
            SystemSounds.Asterisk.Play();
            for (player1.tries = 10; player1.tries > 0; player1.tries--)
            {
                if (!IfGussed(secretWord,guessedRight))
                {
                    DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName);
                    string guess = GetGuessFromPlayer(guessedRight, guessedWrong);
                    bool ifGuessExists = IfGuessExists(guess, guessedRight, guessedWrong);
                    if (ifGuessExists) { player1.tries += 1; SystemSounds.Asterisk.Play(); }
                    else
                    {
                        ifGuessRight = IfGuessRight(secretWord, guess);
                        switch (ifGuessRight)
                        {
                            case 0: guessedWrong.Append(guess); guessedWrong.AppendFormat(","); break;
                            case 1: guessedRight = UpdateguessedRight(guessedRight, guess, secretWord); break;
                            case 2:
                                player1.tries = 0;
                                ob.isGuessed = true;
                                DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName);
                                break;
                            default: guessedWrong.Append(guess); guessedWrong.AppendFormat(","); break;

                        }

                    }

                    if (!ob.isGuessed && !ifGuessExists)
                    {
                        Refresh();
                    }
                    if (player1.tries == 1)
                    { Console.WriteLine("\a \nGAME OVER\n" + player1.playerName + " Loses!! \n"); }
                }
                else
                {
                    player1.tries = 0;
                    ob.isGuessed = true;
                    DisplayToPlayer(secretWord, ob.isGuessed, player1.tries, guessedRight, guessedWrong, player1.playerName);
                    break;
                }
            }




        }
        public static void Refresh()
        {

            Console.Clear();
            Program.TitleMenu();
        }

        static string GetSecretWord(int Language)
        {
            var rand = new Random();
            int wordIndex = rand.Next(0, WordsRepository.GetWords(Language).Length);

            return WordsRepository.GetWords(Language)[wordIndex];
        }
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

        static bool IfGussed(string secretWord, char[] Guessed)
        {
            string wGuessed = new String(Guessed);
            bool result= wGuessed.Equals(secretWord) ? true : false;
            return result;
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
            SystemSounds.Asterisk.Play();
            Console.WriteLine("\n Enter Player Name ");
            playerName = Console.ReadLine();
            playerName = playerName == "" ? "Player" + n.ToString() : playerName;
            return playerName;
        }

    }
}
