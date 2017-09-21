using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Data;

namespace Mi_Share.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        Mi_ShareEntities dbContext;
        public Mi_ShareEntities init()
        {
            return dbContext ?? (dbContext = new Mi_ShareEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
