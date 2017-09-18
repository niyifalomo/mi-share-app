using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Model
{
    public class Item : ISoftDelete
    {
        public int ID { get; set; }
        public int Owner_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category_ID { get; set; }
        public ItemStatus Status {get;set;}
        public  int Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual User Owner { get; set; }
        public virtual Category Category { get; set; }
        public IList<Request> Requests { get; set; }

    }

    public enum ItemStatus {
        [Description("Available")]
        Available,
        [Description("Borrowed")]
        Borrowed
    }
}
