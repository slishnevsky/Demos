using EntityFrameworkDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();
                var blog = new Blog { Name = name };



                Console.Write("Enter a name for a new Post: ");
                var title = Console.ReadLine();
                var post = new Post { Title = title };

                blog.Posts.Add(post);
                db.Blogs.Add(blog);

                db.SaveChanges();

                Console.WriteLine("All blogs and posts in the database:");

                foreach (var b in db.Blogs)
                {
                    Console.WriteLine($"Blog: {b.Name}");
                    foreach (var p in b.Posts)
                    {
                        Console.WriteLine($"Post: {p.Title}");
                    }
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
