using System;
using System.Media;

namespace Hangman_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            bool exitApp = false;
            while (!exitApp)  // Control Application Exit
            {

                TitleMenu();      // Show Title

                int chLanguage = -1;
                while (chLanguage == -1)
                { chLanguage = UserChoice(); }

                try
                {
                    PerformUserchoice(chLanguage); // Executes according choice
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred.\n - Details: " + e.Message);
                }

                // Wait for the user to respond before closing
                if (!exitApp)
                {
                    Console.Write("\n\nPress any key: ");
                    Console.ReadLine();
                    Console.Clear();
                }
                Console.WriteLine("\n"); // Friendly linespacing.

                // Starts the game or exit according to user choice 
                void PerformUserchoice(int chLanguage)
                {
                    switch (chLanguage)
                    {
                        case 0: exitApp = true; break;
                        case 1: Console.Clear(); TitleMenu(); Start.StartGame(chLanguage); break;
                        case 2: Console.Clear(); TitleMenu(); Start.StartGame(chLanguage); break;
                        default: Console.WriteLine("\tInvalid choice! Try again"); break;

                    }

                }
            }

            return;
        }

        public static void TitleMenu()
        {
            SystemSounds.Hand.Play();
            // Display title 
            Console.WriteLine("\n  HANGMAN\r");
            Console.Write(" =========\n");
        }
        static int UserChoice()
        {
            int chLanguage = -1;
            try
            {

                Console.WriteLine(" Choose (1 for English , 2 for Swedish or 0 to Exit) : ");
                string userLanguageChoice = ChkUserinput(Console.ReadLine());
                chLanguage = int.Parse(userLanguageChoice);
            }
            catch { Console.WriteLine("\n Invalid! 1 for English , 2 for Swedish or 0 to Exit"); }
            if (chLanguage >= 0 && chLanguage <= 2) { }
            else
            {
                Console.WriteLine("\n Invalid!");
                chLanguage = -1;
            }
            return chLanguage;

        }
        public static string ChkUserinput(string Userinput)
        {
            while (Userinput == "")
            {
                Console.WriteLine("\n Invalid! Try again.");
                Userinput = Console.ReadLine();
            }
            return Userinput;
        }

    }
}
