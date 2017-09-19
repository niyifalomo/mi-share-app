using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Data.Infrastructure;
using Mi_Share.Model;

namespace Mi_Share.Data.Repositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
    public interface IItemRepository : IRepository<Item>
    {

    }
}
