using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
    public class AccessCard
    {
        public int AccessCardId { get; protected set; }
        public CardHolder CardHolder { get; set; }
		public int CardHolderId { get; set; }
		public DateTime ActivationDate { get; set; }
        public bool ActivationProcessed { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public string DeactivationReason { get; set; }
        public bool DeactivationProcessed { get; set; }
	}
}