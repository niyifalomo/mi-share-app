using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Model
{
    public class Loan
    {
        public int ID { get; set; }
        public int Request_ID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
        public virtual Request Request { get; set; }

    }

    public enum LoanStatus {
        OnLoan,
        Returned
    }
}
