using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Data
{
    public class Mi_ShareSeedData : DropCreateDatabaseIfModelChanges<Mi_ShareEntities>
    {
        protected override void Seed(Mi_ShareEntities context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            context.Commit();
        }
        private static List<Category> GetCategories()
        {
            return new List<Category>
            {

                new Category {
                    Name = "Games"
                },
                new Category {
                    Name = "Movies"
                },
                new Category {
                    Name = "Music"
                }
            };
        }
    }
}
