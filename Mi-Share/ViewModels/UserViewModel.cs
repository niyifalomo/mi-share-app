using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mi_Share.Model;

namespace Mi_Share.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual IEnumerable<Request> ItemRequests { get; set; }
        public virtual IList<CollectionAccess> CollectionAccess { get; set; }
    }
}