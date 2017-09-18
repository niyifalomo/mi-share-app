using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Model
{
    public class CollectionAccess
    {
        public int ID { get; set; }
        public int Owner_ID {get;set;}
        public int Requester_ID { get; set; }
        public DateTime DateRequested { get; set; }
        public Status Status { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual User Owner { get; set; }
        public virtual User Requester { get; set; }

        public CollectionAccess() {
            DateRequested = DateTime.Now;
        }

    }

    public enum Status {
        Pending,
        Granted,
        Denied
    }

}
