using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Contacts;
using System.Net;
using System.Net.Mail;
using System.Collections.Specialized;
using Google.GData.Client;
using System.Threading;
using System.Configuration;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EmailContacts
{
    public class Program
    {
        static NetworkCredential credential;
        static ContactsService service;
        static SmtpClient smtp;

        static void Main(string[] args)
        {
            Initialize();

            UpdateContacts();
            //EmailContacts();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void Initialize()
        {
            smtp = new SmtpClient();
            credential = smtp.Credentials.GetCredential(smtp.Host, smtp.Port, "Basic");
            service = new ContactsService("Contacts");
            service.setUserCredentials(credential.UserName, credential.Password);
        }

        static AtomEntryCollection GetGroups()
        {
            var query = new GroupsQuery(GroupsQuery.CreateGroupsUri("default")) { NumberToRetrieve = 1000 };
            var feed = service.Query(query);
            var groups = feed.Entries;
            return groups;
        }

        public static AtomEntryCollection GetContacts(AtomEntry group = null)
        {
            var query = new ContactsQuery(ContactsQuery.CreateContactsUri("default")) { NumberToRetrieve = 1000 };
            if (group != null) query.Group = group.Id.AbsoluteUri;
            var feed = service.Query(query);
            var contacts = feed.Entries;
            return contacts;
        }
        
        private static void UpdateContacts()
        {
            Console.Write("This operation will update all contacts.\n\nPress [Y] if you want to continue or any key to abort: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine("\n");
            if (key.Key != ConsoleKey.Y) return;
            var contacts = GetContacts();
            foreach (var entry in contacts)
            {
                try
                {
                    var contact = (ContactEntry)entry;
                    if (contact.Name == null) contact.Name.FullName = "Unknown";

                    // format emails
                    foreach (var email in contact.Emails) email.Address = email.Address.ToLower();

                    // format phone numbers
                    foreach (var phone in contact.Phonenumbers)
                    {
                        var regex = new Regex(@"[^\d]");
                        string numbers = regex.Replace(phone.Value, "");
                        string formatted = (numbers.Length > 10) ?
                            String.Format("{0:###-###-####},{1}", Convert.ToInt64(numbers.Substring(0, 10)), Convert.ToInt64(numbers.Substring(10))) :
                            String.Format("{0:###-###-####}", Convert.ToInt64(numbers));
                        phone.Value = formatted;
                    }

                    entry.Update();
                    Console.WriteLine("Contact {0} is updated", contact.Name.FullName);
                }
                catch (Exception) { }
            }
        }

        private static void EmailContacts()
        {
            Console.Write("This operation will email all agents.\n\nPress [Y] if you want to continue or any key to abort: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine("\n");
            if (key.Key != ConsoleKey.Y) return;
            
            string subject = ConfigurationManager.AppSettings["Subject"];
            string message = ConfigurationManager.AppSettings["Message"];
            string resume = ConfigurationManager.AppSettings["Resume"];

            var group = GetGroups().Single(g => g.Title.Text == "Agents");
            var agents = GetContacts(group);

            foreach (var agent in agents)
            {
                var address = ((ContactEntry)agent).PrimaryEmail.Address;
                var name = ((ContactEntry)agent).Name.GivenName;

                try
                {
                    Console.Write("Sending to {0} <{1}>... ", name, address);
                    SendMessage(name, address, subject, message, resume);
                    Console.WriteLine("DONE\n");
                    Thread.Sleep(1000);
                }
                catch (Exception) { continue; }
            }
        }

        static void SendMessage(string name, string address, string subject, string message, string resume)
        {
            address = "slishnevsky@gmail.com"; // for testing

            var mail = new MailMessage();
            mail.To.Add(new MailAddress(address, name));
            mail.Subject = subject;
            mail.Body = String.Format(message, name);
            if (resume != null) mail.Attachments.Add(new Attachment(resume));
            smtp.Send(mail);
        }

        private static void PrintDetails(AtomEntryCollection contacts)
        {
            var agencies = contacts.Select(c => ((ContactEntry)c).PrimaryEmail.Address.Split('@')[1].ToUpper()).Distinct().OrderBy(a => a);

            Console.WriteLine("FOUND {0} AGENCIES\n", agencies.Count());

            foreach (string agency in agencies)
            {
                Console.WriteLine(agency);
                foreach (ContactEntry contact in contacts)
                {
                    var domain = ((ContactEntry)contact).PrimaryEmail.Address.Split('@')[1].ToUpper();
                    if (domain != agency) continue;

                    Console.WriteLine(contact.Name.FullName);
                    Console.WriteLine(contact.Name.GivenName);

                    Console.WriteLine(contact.PrimaryEmail.Address);
                    Console.WriteLine(contact.PrimaryPhonenumber);
                    Console.WriteLine(contact.PrimaryPostalAddress);

                    foreach (var email in contact.Emails)
                    {
                        Console.WriteLine(email.Address);
                    }
                    foreach (var phone in contact.Phonenumbers)
                    {
                        Console.WriteLine(phone);
                    }
                    foreach (var address in contact.PostalAddresses)
                    {
                        Console.WriteLine(address);
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("FOUND {0} CONTACTS\n", contacts.Count());


        }

    }


}
