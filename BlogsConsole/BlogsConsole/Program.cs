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
                    Console.WriteLine("4) View posts");
                    Console.WriteLine("5) Exit");
                    choice = Console.ReadLine();
                    if (choice == "2") {
                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                        var db = new BloggingContext();
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);

                    } else if (choice == "1")
                    {
                        //View blogs
                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine("All blogs in the database: \n");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                            Console.WriteLine();
                        }
                    } else if (choice == "3")
                    {
                        //Posting to a blog
                        var context = new BloggingContext();

                        Console.Write("Enter the name of blog you want to post in: ");
                        string ablog = Console.ReadLine();

                            var blog = context.Blogs
                                            .Where(b => b.Name == ablog)
                                            .FirstOrDefault();
                        if (blog == null)
                        {
                            Console.WriteLine("You cannot post to this blog because it might be misspelled or an error occured.");
                            logger.Warn("Query returned with {0}", blog);
                        }
                        else
                        {
                            Console.Write("Enter title of post: ");
                            string title = Console.ReadLine();

                            if (title == "")
                            {
                                Console.WriteLine("Title cannot be blank. Try again.");
                            }
                            else
                            {
                                Console.WriteLine("Enter post content: ");
                                string content = Console.ReadLine();
                                if (content == "")
                                {
                                    content = null;
                                }
                                var post = new Post { Blog = blog, Title = title, Content = content, BlogId = blog.BlogId };
                                context.AddPost(post);
                            }

                            logger.Info("New blog post with title " + title + " on the blog " + blog);
                        }


                    } else if (choice == "4")
                    {
                        //view posts
                        var context = new BloggingContext();

                        Console.Write("Enter name of blog to view posts of: ");
                        string ablog = Console.ReadLine();

                        var blog = context.Blogs
                                        .Where(b => b.Name == ablog)
                                        .FirstOrDefault();

                        var id = context.Posts
                                        .Where(p => p.BlogId == blog.BlogId)
                                        .FirstOrDefault();

                        if (blog == null)
                        {
                            Console.WriteLine("That blog does not exist.");
                            logger.Info("User tried to search for " + ablog);
                        } else
                        {



                        }

                    }
                    else if (choice == "5")
                    {
                        //choice to exit
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                        choice = "1";
                    }
                } //end of while loop
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