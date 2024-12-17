// See https://aka.ms/new-console-template for more information
using System;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;
class Program
{
    public static List<Book> Library = [];
    static void Main(string[] args)
    {
        Options();
    }
 
    public static void Options()
    {       
             Console.WriteLine("Welcome into your Library!");
            Console.WriteLine("Choose an option: ");
    Console.WriteLine("1. Add a new Book to the Library");
    Console.WriteLine("2. Display your books entered in the Library");
    string option = Console.ReadLine()!;
        switch(option)
        {
            case "1":               
                AddMoreBook();            
                break;
            case "2":
                DisplayBooks();
                break;
            default:
                Console.WriteLine("Not an option");
                break;
        }
    }

    public static void AddMoreBook()
    {   
        try
        {
            Console.WriteLine("Enter the book title: ");
            string title = Console.ReadLine()!;
            Console.WriteLine("Enter the book year of publication (yyyy): ");
            string date = Console.ReadLine()!;
            Console.WriteLine("Enter the book Author firstname: ");
            string firstname = Console.ReadLine()!;
            Console.WriteLine("Enter the book Author lastname: ");
            string lastname = Console.ReadLine()!;
            Book book = new Book();
            book.Title = title;
            book.PublicationDate = int.Parse(date);
            book.author = new Author();
            book.author.FirstName = firstname;
            book.author.LastName = lastname;
            Library.Add(book);
            Console.WriteLine("Do you want to add more books?");
            string answer = Console.ReadLine()!;
            if(answer == "yes" || answer == "y")
                {
                    AddMoreBook();
                }
                else{
                    Options();
                }        
        }
        catch(FormatException exp)
        {
            Console.WriteLine("wrong format:", exp.Message);
            Console.WriteLine("Do you want to try to add the book again?");
            string answer = Console.ReadLine()!;
            if(answer == "yes" || answer == "y")
                {
                    AddMoreBook();
                }
                else{
                    Options();
                }    
        }
    }

    public static void DisplayBooks()
    {
        foreach (Book book in Library)
        {
            book.getInfos();
        }

    }

}

