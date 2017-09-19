using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mi_Share.Model;
using System.Web.Mvc;

namespace Mi_Share.ViewModels
{
    public class ItemViewModel
    {
        public int ID { get; set; }
        public int Owner_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category_ID { get; set; }
        public ItemStatus Status { get; set; }
        public int Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual User Owner { get; set; }
        public virtual Category Category { get; set; }
        public IEnumerable<Request> Requests { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}