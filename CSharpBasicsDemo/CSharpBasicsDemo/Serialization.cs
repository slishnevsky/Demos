using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasicsDemo
{
    public static class Serialization
    {
        private static List<Customer> _customers = new List<Customer>();

        public static void Invoke()
        {
            CreateCustomers();
            SerializeCustomers();
            DeserializeCustomers();
        }

        private static void CreateCustomers()
        {
            var customer1 = new Customer { Id = 1, Name = "Slava Lishnevsky" };
            customer1.Orders.Add(new Order { Id = 1, Name = "Hardware" });
            customer1.Orders.Add(new Order { Id = 1, Name = "Software" });

            var customer2 = new Customer { Id = 1, Name = "Eugene Lishnevsky" };
            customer2.Orders.Add(new Order { Id = 1, Name = "Furniture" });
            customer2.Orders.Add(new Order { Id = 1, Name = "Food" });

            _customers.Add(customer1);
            _customers.Add(customer2);
        }

        private static void SerializeCustomers()
        {
            var file = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "customers.json");
            var json = JsonConvert.SerializeObject(_customers);
            File.WriteAllText(file, json, Encoding.UTF8);
        }

        private static void DeserializeCustomers()
        {
            var file = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "customers.json");
            var json = File.ReadAllText(file, Encoding.UTF8);

            _customers = JsonConvert.DeserializeObject<List<Customer>>(json);

            foreach (var customer in _customers)
            {
                Console.WriteLine(customer.Name);
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine(order.Name);
                }
            }
        }
    }

    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
