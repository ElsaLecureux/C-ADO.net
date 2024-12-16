// See https://aka.ms/new-console-template for more information
using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choisissez une option");
        string option = Console.ReadLine()!;
            switch(option)
            {
                case "1":
                    Console.WriteLine("option 1");
                    break;
                case "2":
                    Console.WriteLine("option 2");
                    break;
                default:
                    Console.WriteLine("Not an option");
                    break;
            }
    }

}

