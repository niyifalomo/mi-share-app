using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mi_Share.Model;

namespace Mi_Share.ViewModels
{
    public class LoanViewModel
    {
        public int ID { get; set; }
        public int Request_ID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
        public virtual Request Request { get; set; }
    }
}