using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEnriquez
{
    //Superclass for the kinds of things a library offers its customers
    public class Holding
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCheckedOut { get; set; }

        public Holding(int id, string title, string description)
        {
            ID = id;
            Title = title;
            Description = description;
            IsCheckedOut = false; //By default set to false because holding is not checked out
        }
        public void CheckOut()
        {
            IsCheckedOut = true;
            Console.WriteLine($"{Title} has been checked out.");
        }
        public void CheckIn()
        {
            IsCheckedOut = false;
            Console.WriteLine($"{Title} has been chekced in.");
        }
    }
    // Subclass of holding representing a book
    public class Book : Holding
    {
        public int CopyrightYear { get; set; }
        public string Author { get; set; }
        public Book(int id, string title, string description, int copyrightYear, string author)
            : base(id, title, description)
        {
            CopyrightYear = copyrightYear;
            Author = author;
        }
   }

    //Subclass of Holding representing a periodical
    public class Periodical : Holding
    {
        public DateTime Date { get; set; }
        public Periodical(int id, string title, string description, DateTime date)
            : base(id, title, description)
        {
            Date = date;   
        }
    }
class Program
    {
        //Example uage
        Book book = new Book(1, "C# Programming", "A comprehensive guide to C# programming language.", 2023, "John Smith");
        Periodical periodical = new Periodical(2, "Scientific American", "A popular science magazine.", new DateTime(2024, 2, 15));    
        
        Console.WriteLine($"Book: {book.Title}, Author: {book.Author}, Copyright Year: {book.CopyrightYear}");
        Console.WriteLine($"Periodical: {periodical.Title}, Date: {periodical.Date.ToShortDateString()}");
        
        //Check out and check in example
        book.CheckOut();
        book.CheckIn();

    }

}