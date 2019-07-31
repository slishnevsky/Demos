using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
	public class CardHolder
	{
		public int CardHolderId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public string TenantName { get; set; }
		public virtual ICollection<AccessCard> AccessCards { get; set; }

		public CardHolder()
		{
			AccessCards = new List<AccessCard>();
		}
	}

}