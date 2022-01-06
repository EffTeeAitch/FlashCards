using System;
using System.Collections.Generic;
using System.IO;

namespace DoSzkoloy
{
    class FlashCard
    {

        public void AddContent(Dictionary<string, string> dictionary, string con1, string con2)               //you can add new record into dictionary
        {
            if (dictionary.ContainsKey(con1))                                                           //checks if word(key) already exists in dictionary                         
            {
                Console.WriteLine("\nThe element already exists\n\n\tPush any button to continue");
                Console.ReadKey();
            }
            else
                dictionary.Add(con1, con2);

        }

        public void Delete(Dictionary<string, string> dictionary, string con1)                              //you can delete record from dictionary
        {
            if (!dictionary.ContainsKey(con1))                                                      //checks if wanted work was inserted into dictionary
            {
                Console.WriteLine($"\nKey '{con1}' was not found. \n\n\tPush any button to continue");
                Console.ReadKey();
            }
            else
            {
                dictionary.Remove(con1);
            }
        }
        public void ShowAll(Dictionary<string, string> dictionary)                          //you can show all flashcards
        {
            Console.WriteLine("\n");
            foreach (KeyValuePair<string, string> d in dictionary)
            {
                Console.WriteLine($"\nPolish: {d.Key}\t\t\t\t English: {d.Value}");
            }
            Console.WriteLine("\n\tPush any button to continue!");
            Console.ReadKey();
        }
        public void EraseDictionary(Dictionary<string, string> dictionary)
        {
            dictionary.Clear();

        }


        public void Import(List<string[]> lista, Dictionary<string, string> dictionary)
        {
            lista.Clear();
            string line;

            string path = "";                                                       //variable containing path
            //Console.Write($"\nName of the file without extension: ");
            //path = Console.ReadLine() + ".txt";
            //Console.WriteLine();
     
            //try
            //{
            //    using var helper = new StreamReader(path);
            //    helper.Close();
            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.Write($"File not found, try another one: ");
            //    path = Console.ReadLine() + ".txt";
            //}
            do
            {
                Console.Write($"\nName of the file without extension: ");
                path = Console.ReadLine() + ".txt";

                if (!File.Exists(path))
                {
                    Console.WriteLine("File does not exist, try another one");
                }
                else
                    continue;
            } while (File.Exists(path) == false);

                            //copying file into dictionary
            using var file = new StreamReader(path);       
            while ((line = file.ReadLine()) != null)
                lista.Add(line.Split(" "));
            foreach (string[] l in lista)
            {
                string con1 = l[0];
                string con2 = l[1];
                dictionary.Add(con1, con2);
            }
        }
        public void Export(Dictionary<string, string> dictionary)
        {
            Console.Write($"\nName of the file without extension: ");
            string path = Console.ReadLine() + ".txt";
            Console.WriteLine();
            using (FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                lock (fs)
                {
                    fs.SetLength(0);                                               //just for clearing file to start writing from the begining
                }
            }

            using StreamWriter file = new StreamWriter(path);
            foreach (KeyValuePair<string, string> d in dictionary)
            {

                file.WriteLine($"{d.Key} {d.Value}");  //rewriting whole dictionary into file 

            }
        }
    }


    class Program
    {
        static int Main(string[] args)
        {
            Dictionary<string, string> flash = new Dictionary<string, string>();
            FlashCard firstClass = new FlashCard();
            List<string[]> data = new List<string[]>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------Flashcards App-----\n");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("\n\t1: Create card");
                Console.WriteLine("\n\t2: Delete card");
                Console.WriteLine("\n\t3: Show cards");
                Console.WriteLine("\n\t4: Import cards from the file");
                Console.WriteLine("\n\t5: Export cards to the file");
                Console.WriteLine("\n\t6: Exit program\n");
                Console.WriteLine("---------------------------------------------");

                string choice = Console.ReadLine();
                switch (choice)
                {

                    case "1":
                        Console.Clear();
                        Console.WriteLine("What's the content: ");
                        Console.Write("\tin polish: ");
                        string con1 = Console.ReadLine();
                        Console.Write("\tin english: ");
                        string con2 = Console.ReadLine();
                        firstClass.AddContent(flash, con1, con2);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Which card do you want to delete, write the polish content from it: ");
                        Console.Write("\tin polish: ");
                        string del = Console.ReadLine();
                        firstClass.Delete(flash, del);
                        break;
                    case "3":
                        Console.Clear();
                        firstClass.ShowAll(flash);
                        break;
                    case "4":
                        Console.Clear();
                        firstClass.Import(data, flash);
                        Console.WriteLine("Import ended successfully\n\n\t Push any button to continue");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Clear();
                        firstClass.Export(flash);
                        Console.WriteLine("Works but not exactly how it should");
                        Console.WriteLine("\n\n\nYou don't have access to this option. Buy premium!\n\n\t Push any button to continue");
                        Console.ReadKey();
                        break;
                    case "6":
                        return 1;
                    default:
                        Console.WriteLine("Invalid command.\n\n\t Push any button to continue");
                        Console.ReadKey();
                        break;

                }

            }

        }
    }
}
