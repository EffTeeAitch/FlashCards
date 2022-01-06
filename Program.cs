using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DoSzkoloy
{
    class FlashCard
    {

        public void AddContent(Dictionary<string, string> dictionary, string con1, string con2)
        {
            if (dictionary.ContainsKey(con1))
            {
                Console.WriteLine("\nThe element already exists\n\n\tPush any button to continue");
                Console.ReadKey();
            }
            else
                dictionary.Add(con1, con2);

        }

        public void Delete(Dictionary<string, string> dictionary, string con1)
        {
            if (!dictionary.ContainsKey(con1))
            {
                Console.WriteLine($"\nKey '{con1}' was not found. \n\n\tPush any button to continue");
                Console.ReadKey();
            }
            else
            {
                dictionary.Remove(con1);
            }
        }
        public void ShowAll(Dictionary<string, string> dictionary)
        {
            Console.WriteLine("\n");
            foreach (KeyValuePair<string, string> d in dictionary)
            {
                Console.WriteLine("\nPolish: {0}\t\t\t\t English: {1}",
                    d.Key, d.Value);
            }
            Console.WriteLine("\n\tPush any button to continue!");
            Console.ReadKey();
        }
        public void EraseFile(Dictionary<string, string> dictionary)
        {
            foreach(var d in dictionary)
            {
                
            }
         
        }


        public void Import(List<string[]> lista, Dictionary<string, string> dictionary)
        {
            lista.Clear();
            string line;
            
            Console.Write($"\nName of the file without extension: ");
            string path = Console.ReadLine() + ".txt";
            if(path == "" || path == " ")
            {
                Console.WriteLine("Wrong path selected");
            }
            Console.WriteLine();
            using var file = new StreamReader(path);
            //dictionary.Clear();                                                   //why sould I clear dictionary?
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
                    fs.SetLength(0);   //tylko do wyczyszczenia plliku !!DZIALA!!
                }
            }

            using StreamWriter file = new StreamWriter(path);     //oryginal
            foreach (KeyValuePair<string, string> d in dictionary)
            {

                file.WriteLine($"{d.Key} {d.Value}");  //przepisywanie slownika do pliku -dziala tylko podczas dzialania jednej sesji uzytkownika, po wyjsciu plik nadal ma stare wartosci-

            }

            //using(StreamWriter file = new StreamWriter(path))
            //{
            //    foreach (KeyValuePair<string, string> d in dictionary)
            //    {

            //        
            //        //using (StreamWriter write = File.AppendText(@$"{path}"))
            //        //{
            //        //    file.WriteLine($"{d.Key} {d.Value}");
            //        //}
            //        file.WriteLine($"{d.Key} {d.Value}");
            //    }
            //}



        }
    }
        class Program
        {
            static int Main(string[] args)
            {
                Dictionary<string, string> flash = new Dictionary<string, string>();
                FlashCard firstClass = new FlashCard();
                List<string[]> dane = new List<string[]>();

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
                            firstClass.Import(dane, flash);
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
