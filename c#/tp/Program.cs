// See https://aka.ms/new-console-template for more information
using System;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;
class Program
{
    public static List<Book> Library = new List<Book>();
    public static bool running = true;
    static void Main(string[] args)
    {
        while(running){
            Options();
        }
    }
 
    public static void Options()
    {       
        Console.WriteLine("Welcome into your Library!");
        Console.WriteLine("Choose an option: ");
        Console.WriteLine("1. Add a new Book to the Library");
        Console.WriteLine("2. Display all books in the Library");
        Console.WriteLine("3. Display a book: ");
        Console.WriteLine("4. Delete a book: ");
        Console.WriteLine("5. Exit Library");
    string option = Console.ReadLine()!;
        switch(option)
        {
            case "1":               
                AddMoreBook();            
                break;
            case "2":
                DisplayAllBooks();
                break;
            case "3":
                DisplayBook();
                break;
            case "4":
                DeleteBook();
                break;
            case "5":
                ConfirmExit();
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
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
            Console.WriteLine($"{exp.Message}, Invalid field, please try again.");
        }
    }

    public static void DisplayAllBooks()
    {
        Console.WriteLine("Books in the library: ");
        foreach (Book book in Library)
        {
            book.getInfos();
        }
         Console.WriteLine("Back to option: yes or no");
            string answer = Console.ReadLine()!;
            if(answer == "yes" || answer == "y")
            {
                Options();
            }
            else{
                Console.WriteLine("Exit Library: yes or no");
                string exitAnswer = Console.ReadLine()!;
                if(answer == "yes" || answer == "y")
                {
                    running = false;
                }
            }    
    }

    public static void DisplayBook()
    {
        Console.WriteLine("Enter the exact title of the book you are looking for: ");
        try
        {
            string title = Console.ReadLine()!;
            Book book = Library.Find(book => book.Title == title)!;
            book.getInfos();       
        }
        catch(Exception exp)
        {
            Console.WriteLine($"{exp.Message}, No book found in the library for this title: {Console.ReadLine()!}");
            
        }
    }

    public static void DeleteBook()
    {
        Console.WriteLine("Enter the exact title of the book you want to delete: ");
        try
        {
            string title = Console.ReadLine()!;
            Book book = Library.Find(book => book.Title == title)!;
            Library.Remove(book); 
            Console.WriteLine("This book was deleted from the library.");      
        }
        catch(Exception exp)
        {
            Console.WriteLine($"{exp.Message}, No book found in the library for this title: {Console.ReadLine()!}");
        }
    }

    public static void ConfirmExit()
    {
        Console.WriteLine("Confirm exist Library: yes or no");
        string answer = Console.ReadLine()!;
        if (answer == "yes" || answer == "Yes" || answer == "y")
        {
            running = false;
        }
        else {
            Options();
        }
    }

}

