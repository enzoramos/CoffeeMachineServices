using CoffeeMachine.DataProvider;
using CoffeMachine.Models;
using SQLite.Net;
using SQLite.Net.Platform.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace DBCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //File.Delete("CoffeMachineDB.db3");
                //var file  = File.Create("CoffeMachineDB.db3");
                //file.Close();
                SQLiteConnection connection = SQLIHelper.GetSQLConnection();
                //SQLiteConnection connection = new SQLiteConnection(new SQLitePlatformWin32(), "CoffeMachineDB.db3");
                connection.CreateTable<Drink>();
                connection.InsertAll(new List<Drink> { new Drink { Name = "The" }, new Drink { Name = "Coffee" }, new Drink { Name = "Chocolat" } });
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);

                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }

        }
    }
}
