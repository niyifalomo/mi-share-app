using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mi_Share.Model;

namespace Mi_Share.ViewModels
{
    public class RequestViewModel
    {
        public int ID { get; set; }
        public int Requester_ID { get; set; }
        public int Item_ID { get; set; }
        public RequestStatus Status { get; set; }
        
        public int Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }


        public virtual User Requester { get; set; }
        public virtual Item Item { get; set; }
    }
}