using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mi_Share.Model;

namespace Mi_Share.ViewModels
{
    public class CollectionAccessViewModel
    {
        public int ID { get; set; }
        public int Owner_ID { get; set; }
        public int Requester_ID { get; set; }
        public DateTime DateRequested { get; set; }
        public CollectionAccessStatus Status { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual User Owner { get; set; }
        public virtual User Requester { get; set; }
    }
}