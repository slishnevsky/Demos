using SQLiteDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SQLiteDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new MyContext())
			{
				SaveData(db);
				ReadData(db);
			}

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static void SaveData(MyContext db)
		{
			var cardHolder1 = new CardHolder { FirstName = "Slava", LastName = "Lishnevsky" };
			var cardHolder2 = new CardHolder { FirstName = "Eugene", LastName = "Lishnevsky" };

			var accessCard1 = new AccessCard { ActivationDate = DateTime.Now, DeactivationDate = DateTime.Now.AddDays(5) };
			var accessCard2 = new AccessCard { ActivationDate = DateTime.Now.AddDays(5), DeactivationDate = DateTime.Now.AddDays(10) };

			cardHolder1.AccessCards.Add(accessCard1);
			cardHolder1.AccessCards.Add(accessCard2);

			db.CardHolders.Add(cardHolder1);

			cardHolder2.AccessCards.Add(accessCard1);
			cardHolder2.AccessCards.Add(accessCard2);

			db.CardHolders.Add(cardHolder2);

			db.SaveChanges();
		}

		private static void ReadData(MyContext db)
		{
			var cardHolders = db.CardHolders.Include(x => x.AccessCards).ToList();
			cardHolders.ForEach(holder =>
			{
				Console.WriteLine($"CardHolder >> CardHolderId: {holder.CardHolderId}, FirstName: {holder.FirstName}, LastName: {holder.LastName} \n");
				var accessCards = holder.AccessCards.ToList();
				accessCards.ForEach(card =>
				{
					Console.WriteLine($"AccessCard >> AccessCardId: {card.AccessCardId}, ActivationDate: {card.ActivationDate}, DeactivationDate: {card.DeactivationDate}");
				});
				Console.WriteLine("");
			});
		}
	}
}
