using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Model
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual IList<Item> Items { get; set; }
    }
}
