using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Model
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName {get;set;}
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return FirstName + ' ' + LastName; }
        }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual IList<Item> Items { get; set; }
        public virtual IList<Request> ItemRequests { get; set; }
        public virtual IList<CollectionAccess> SentCollectionAccessRequests { get; set; }
        public virtual IList<CollectionAccess> RecievedCollectionAccess { get; set; }

    }
}
