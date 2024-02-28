using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEnriquez2
{
    using System;
    using static LibraryEnriquez2.Holding.Periodical;
    using static LibraryEnriquez2.Holding;

    class Program
    {
        static void Main(string[] args)
        {
            // Display the header
            Console.WriteLine("*******************************************************");
            Console.WriteLine("         LIBRARY MANAGEMENT SYSTEM VERSION 1.0");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("\nThis tool helps you manage a library's collections.");
            Console.WriteLine("Please use the menu to choose what you want to do.\n");

            // Create a Library object
            Library library = new Library();

            // Variable to store user's choice
            int choice;

            // Menu loop
            do
            {
                // Display menu options
                Console.WriteLine("Here are your options:");
                Console.WriteLine("1. List holdings");
                Console.WriteLine("2. Add a book");
                Console.WriteLine("3. Add a periodical");
                Console.WriteLine("4. Reserve a holding");
                Console.WriteLine("5. Return a holding");
                Console.WriteLine("6. See statistics");
                Console.WriteLine("7. Quit");

                // Prompt user for choice
                Console.Write("Enter the number of your choice: ");
                choice = int.Parse(Console.ReadLine());

                // Perform action based on user's choice
                switch (choice)
                {
                    case 1:
                        // List holdings
                        library.ListAll();
                        break;
                    case 2:
                        // Add a book
                        AddBook(library);
                        break;
                    case 3:
                        // Add a periodical
                        AddPeriodical(library);
                        break;
                    case 4:
                        // Reserve a holding
                        ReserveHolding(library);
                        break;
                    case 5:
                        // Return a holding
                        ReturnHolding(library);
                        break;
                    case 6:
                        // See statistics
                        //library.GetStats();
                        break;
                    case 7:
                        // Quit the program
                        Console.WriteLine("\nThank you for using this program.");
                        break;
                    default:
                        // Invalid choice
                        Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 7.");
                        break;
                }

                // Add a separator line
                Console.WriteLine("\n-------------------------------------------------------\n");

            } while (choice != 7); // Continue loop until user chooses to quit
        }

        // Method to add a book to the library
        static void AddBook(Library library)
        {
            Console.Write("Enter ID Number: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Copyright Year: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            // Create a new Book object and add it to the library
            Book book = new Book(id, title, description, year, author);
            library.AddHolding(book);
        }

        // Method to add a periodical to the library
        static void AddPeriodical(Library library)
        {
            Console.Write("Enter ID Number: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter date: ");
            string date = Console.ReadLine();

            // Create a new Periodical object and add it to the library
            Periodical periodical = new Periodical(id, title, description, date);
            library.AddHolding(periodical);
        }

        // Method to reserve a holding
        static void ReserveHolding(Library library)
        {
            Console.Write("Enter the ID Number of the holding to reserve: ");
            int id = int.Parse(Console.ReadLine());
            library.CheckOut(id);
        }

        // Method to return a holding
        static void ReturnHolding(Library library)
        {
            Console.Write("Enter the ID Number of the holding to check in: ");
            int id = int.Parse(Console.ReadLine());
            library.CheckIn(id);
        }
    }


    //Define the superclass Holding
    public class Holding
    {
        //Properties common to all holding
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool CheckedOut { get; set; }

        //Constructor
        public Holding(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            CheckedOut = false;//By default the holding is not checked out
        }

        //Method to check out the holding 
        public void CheckOut()
        {
            CheckedOut = true;
        }

        //Method to check in the holding
        public void CheckIn()
        {
            CheckedOut = false;
        }

        //Abstract method to return the holding type as a string
        public virtual string HoldingType()
        {
            throw new NotImplementedException("Subclasses must implement this method.");
        }

        //Method to return a detailed description of teh holding
        public override string ToString()
        {
            return $"ID: {Id}\nTitle: {Title}\nDescription: {Description}\nStatus: {(CheckedOut ? "Checked Out" : "Available")}";

        }


        //Define the subclass Book inheriting from holding
        public class Book : Holding
        {
            //Additonal properties for Book
            public int CopyrightYear { get; set; }
            public string Author { get; set; }

            //Constructor
            public Book(int id, string title, string description, int copyrightYear, string author) : base(id, title, description)
            {
                //Validate copyright year
                if (copyrightYear < 1800 || copyrightYear > 2024)
                {
                    throw new ArgumentException("Copyright year must be between 1800 and 2024. ");
                }

                CopyrightYear = copyrightYear;
                Author = author;
            }

            //Override the HoldingType method to return "book"
            public override string HoldingType()
            {
                return "Book";
            }

            //override the ToString method to provide detailed information about the book
            public override string ToString()
            {
                return base.ToString() + $"\nAuthor: {Author}\nCopyright Year: {CopyrightYear}";
            }
        }

        //Define the subclass periodical inheriting from holding
        public class Periodical : Holding
        {
            //Additional property for periodical
            public string Date { get; set; }

            //Constructor
            public Periodical(int id, string title, string description, string date) : base(id, title, description)
            {
                Date = date;
            }

            //Override the holdingtype method to return periodical
            public override string HoldingType()
            {
                return "Periodical";
            }

            //override the tostring method to provide detailed info about the periodical 
            public override string ToString()
            {
                return base.ToString() + $"\nDate: {Date}";
            }


            //Define the library class
            public class Library
            {
                //list to hold holding objects
                private List<Holding> holdings;

                //Constructor
                public Library()
                {
                    holdings = new List<Holding>();
                }

                //method to add new holding to library
                public void AddHolding(Holding holding)
                {
                    holdings.Add(holding);
                }
                //method to check out holding by ID
                public void CheckOut(int id)
                {
                    foreach (var holding in holdings)
                    {
                        if (holding.Id == id)
                        {
                            if (!holding.CheckedOut)
                            {
                                holding.CheckOut();
                                Console.WriteLine("You have checked it out.");
                            }
                            else
                            {
                                Console.WriteLine("The holding is already checked out.");
                            }
                            return;
                        }
                    }
                    Console.WriteLine("There was a problem with your request.");
                }

                //Method to check in holding by ID
                public void CheckIn(int id)
                {
                    foreach (var holding in holdings)
                    {
                        if (holding.Id == id)
                        {
                            if (holding.CheckedOut)
                            {
                                holding.CheckIn();
                                Console.WriteLine("You have checked it in.");
                            }
                            else
                            {
                                Console.WriteLine("The holding is already checked in.");
                            }
                            return;
                        }
                    }
                    Console.WriteLine("There was a problem with your request.");
                }
                //Method to list all holdings in the library
                public void ListAll()
                {
                    Console.WriteLine("These holdings are checked out:");
                    var checkedOutHoldings = holdings.FindAll(h => h.CheckedOut);
                    if (checkedOutHoldings.Count == 0)
                    {
                        Console.WriteLine("No holdings are checked out.");
                    }
                    else
                    {
                        foreach (var holding in checkedOutHoldings)
                        {
                            Console.WriteLine(holding);
                        }
                    }
                    Console.WriteLine("\nThese holdings are available:");
                    var availableHolding = holdings.FindAll(holdings => !holdings.CheckedOut);
                }
            }
        }
    }

}
    
    
 

        

    
   