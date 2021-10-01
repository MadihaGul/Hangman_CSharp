
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hangman_CSharp
{
    class WordsRepository
    {        
        static string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        // Reads data from relevant CSV File based wordsRepository
        private static string[] ReadFromFile_csv(string wordsRepository)
        {
            string data = "";
            string Filename = path + "\\" + wordsRepository;
            // Reads data from a file saved on HD
            try
            {
                FileStream fsReadFile = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(fsReadFile))
                {
                    data = sr.ReadToEnd();
                }
               
            }
            // If file with the given name not exists
            catch (FileNotFoundException)
            {             
                Console.WriteLine("\n\aError: CSV File not found\n");
                
            }
            return data.Split(",");
        }
        // Saves CSV file contents in string array based on language calls relevant word repository
        public static string[]  GetWords(int language)         
        {
            string[] words = language
            switch
            {
                1 => ReadFromFile_csv("englishWords.csv"),
                2 => ReadFromFile_csv("swedishWords.csv"),

            };
            return words;
        }

        //public static void SaveFile(int language)
        //{
        //    bool isSaved = language
        //    switch
        //    {
        //        1 => SaveFile("home,country,mobile,laptop,telephone,students,teenager,notebook,ground,doll,science,hospital,popular," +
        //        "football,truck,travel,business,lamp,table,fan,food,life,bottle,grass,flower,salary,employer,story,gift,people," +
        //        "umbrella,grocery,bus,car,bag,file,musik,message,family,beach,world,religion,game,culture", "englishWords.csv"),

        //        2 => SaveFile("mataffär,samarbete,kunnig,byggnad,tålamod,studerar,leksak,godkänd,kompis,kärlek,yrke,forskning," +
        //        "förare,säkerhet,omgivning,försök,onödan,kontor,lampa,penna,frukt,djur,trappa,upplevelse,tavla,spegel,skogen" +
        //        "fönster,rullstol,brev,restaurang,barn,miljö,kunskap,utbildning,språk,idrott,väg,hiss,fackla,fest", "swedishWords.csv"),
        //    };
        //    if (isSaved) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\n File saved "); Console.ResetColor(); }
        //    else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n Error! File not saved "); Console.ResetColor(); }
        //}

        //private static bool SaveFile(string saveText, string wordsRepository)
        //{
        //    bool issaved = false;
        //    try
        //    {
        //        string Filename = path + "\\" + wordsRepository;
        //        bool Fileexists = CheckifFileExist(Filename);

        //        // Checks if file exists and prompt user to choose between edit or replace file
        //        if (Fileexists)
        //        {
        //            File.Delete(Filename);

        //            FileStream fs = new FileStream(Filename, FileMode.Append);
        //            byte[] bdata = Encoding.Default.GetBytes(saveText);
        //            fs.Write(bdata, 0, bdata.Length);
        //            fs.Close();
        //            issaved = true;
        //        }
        //        else
        //        {
        //            FileStream fs = new FileStream(Filename, FileMode.Append);
        //            byte[] bdata = Encoding.Default.GetBytes(saveText);
        //            fs.Write(bdata, 0, bdata.Length);
        //            fs.Close();
        //            issaved = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("\n <font color=red> Oooops! We are facing some problems, Please contact system administrator.</font>"); issaved = false;
        //    }

        //    return issaved;
        //}

        //public static bool CheckifFileExist(string Chkfile)
        //{
        //    if (File.Exists(@Chkfile))
        //    {
        //        return true;
        //    }
        //    else { return false; }
        //}

        
        //public string[] wordsInSwedish = 
        //{"mataffär","samarbete","kunnig","byggnad"};
    }
}
