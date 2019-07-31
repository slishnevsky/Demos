using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using NHibernateDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo
{
    class Program
    {
        private static ISessionFactory _sessionFactory;

        static void Main(string[] args)
        {
            //ConfigureNHibernate("AngusDemo");
            //GetCheckedInVisitors(DateTime.Today, DateTime.Today.AddDays(1));
            ConfigureNHibernate("Customers");
            AddCustomers();
            GetCustomers();
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        private static void ConfigureNHibernate(string connectionStringName)
        {
            NHibernateProfiler.Initialize();
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionStringName = connectionStringName;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
                //x.LogFormattedSql = true;
                //x.LogSqlInConsole = true;
            });
            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            _sessionFactory = cfg.BuildSessionFactory();
        }

        private static void AddCustomers()
        {
            Console.WriteLine("Adding new customers to the database...");
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Save(new Customer { Name = "Slava Lishnevsky" });
                session.Save(new Customer { Name = "Eugene Lishnevsky" });
                tx.Commit();
            }

        }

        private static void GetCustomers()
        {
            Console.WriteLine("Retrieving customers from the database...");
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customers = session.CreateCriteria<Customer>().List<Customer>();

                Console.WriteLine($"There are {customers.Count} customers in the database\n");

                foreach (var customer in customers)
                {
                    Console.WriteLine(
                        $"customer.Id: {customer.Id}\n" +
                        $"customer.Name: {customer.Name}\n");
                }
                Console.WriteLine();
                tx.Commit();
            }

        }

        private static void GetCheckedInVisitors(DateTime fromDate, DateTime toDate)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var visitors = session.CreateCriteria<Visitor>()
                    .Add(Restrictions.Between("DateCheckedIn", fromDate, toDate))
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.Property("DateCheckedIn"), "DateCheckedIn")
                        .Add(Projections.Property("VisitorId"), "VisitorId")
                        .Add(Projections.Property("VisitorName"), "VisitorName")
                        .Add(Projections.Property("SecuritySystemBarCode"), "SecuritySystemBarCode"))
                    .SetResultTransformer(Transformers.AliasToBean<Visitor>())
                    .List<Visitor>();

                Console.WriteLine($"There are {visitors.Count} visitors checked in between {fromDate} and {toDate}\n");

                foreach (var visitor in visitors)
                {
                    Console.WriteLine(
                        $"visitor.VisitorId: {visitor.VisitorId}\n" +
                        $"visitor.VisitorName: {visitor.VisitorName}\n" +
                        $"visitor.DateCheckedIn: {visitor.DateCheckedIn.ToString("yyyy-MM-dd")}\n" +
                        $"visitor.SecuritySystemBarCode: {visitor.SecuritySystemBarCode}\n");
                }
                Console.WriteLine();
                tx.Commit();
            }
        }
    }
}



