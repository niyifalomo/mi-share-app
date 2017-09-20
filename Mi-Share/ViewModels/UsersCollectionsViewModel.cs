using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mi_Share.ViewModels
{
    public class UsersCollectionsViewModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CollectionAccessStatus Access { get; set; }
    }
}