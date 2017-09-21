using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mi_Share.Model;

namespace Mi_Share.ViewModels
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Item> Items { get; set; }
    }
}