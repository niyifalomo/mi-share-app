using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Model
{
    public class Request
    {
        public int ID { get; set;}
        public int Requester_ID { get; set; }
        public int Item_ID { get; set; }
        public RequestStatus Status { get; set; }

        public DateTime DateCreated { get; set; }
        
        public DateTime? Updated_At { get; set; }


        public virtual User Requester {get; set;}
        public virtual Item Item { get; set;}

        public virtual IList<Loan> Loan { get; set; }

        public Request()
        {
            DateCreated = DateTime.Now;
        }
    }

    public enum RequestStatus {
        [Description("Pending")]
        Pending,

        [Description("Granted")]
        Granted,

        [Description("Denied")]
        Denied
    }
}
