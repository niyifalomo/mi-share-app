using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Model
{
    public class UsersCollections
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public int ItemCount { get; set; }
        public CollectionAccessStatus Access { get; set; }

        
    }
}
