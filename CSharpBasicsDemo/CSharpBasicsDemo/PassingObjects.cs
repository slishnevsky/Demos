using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasicsDemo
{
    public static class PassingObjects
    {
        public static void Invoke()
        {
            // Passing objects by value or by reference, test1
            Person p = new Person { Name = "Slava" };
            Console.WriteLine("Created a Person: {0}\n", p.Name);

            Console.WriteLine("Calling ChangePersonByVal(p)\n");
            ChangePersonByVal(p);
            Console.WriteLine("Now we have: {0}\n", p.Name);

            Console.WriteLine("Calling ChangePersonByRef(ref p)\n");
            ChangePersonByRef(ref p);
            Console.WriteLine("Now we have: {0}\n", p.Name);

            Console.WriteLine("Conclusion: no difference when changing object properties\n".ToUpper());

            Console.WriteLine("Calling ReplacePersonByVal(p)\n");
            ReplacePersonByVal(p);
            Console.WriteLine("Now we have: {0}\n", p.Name);

            Console.WriteLine("Calling ReplacePersonByRef(ref p)\n");
            ReplacePersonByRef(ref p);
            Console.WriteLine("Now we have: {0}\n", p.Name);

            Console.WriteLine("Conclusion: there is a difference, when reassigning the object, new object is passed back\n".ToUpper());
        }


        private static void ChangePersonByVal(Person p)
        {
            p.Name = "Eugene";
        }

        private static void ChangePersonByRef(ref Person p)
        {
            p.Name = "Linda";
        }

        private static void ReplacePersonByVal(Person p)
        {
            p = new Person { Name = "Michael" };
        }
        private static void ReplacePersonByRef(ref Person p)
        {
            p = new Person { Name = "Matilda" };
        }
    }

    class Person
    {
        public string Name { get; set; }
    }
}
