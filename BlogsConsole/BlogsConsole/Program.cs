using System;
using NLog;
using BlogsConsole.Models;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                string choice = "1";
                while (choice == "1" || choice == "2" || choice == "3") {
                    Console.WriteLine("What would you want to do?");
                    Console.WriteLine("1) Display all blogs");
                    Console.WriteLine("2) Create a new blog");
                    Console.WriteLine("3) Post to a blog");
                    Console.WriteLine("4) Exit");
                    choice = Console.ReadLine();
                    if (choice == "1") {
                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                        var db = new BloggingContext();
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);

                    } else if (choice == "2")
                    {
                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }
                    } else if (choice == "3")
                    {




                    } else if (choice == "4")
                    {
                        
                    } else
                    {
                        Console.WriteLine("Invalid choice.");
                        choice = "1";
                    }
                }
            } // end of try
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            Console.WriteLine("Press enter to Quit");
            Console.ReadLine();
            logger.Info("Program ended");
        }
    }
}