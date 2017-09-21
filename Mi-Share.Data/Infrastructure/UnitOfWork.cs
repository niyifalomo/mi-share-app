using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Mi_Share.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private Mi_ShareEntities dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        public Mi_ShareEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.init()); }
        }
        public int Commit()
        {
            return DbContext.Commit();
        }
    }
}
