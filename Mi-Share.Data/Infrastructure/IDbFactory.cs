using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Data;

namespace Mi_Share.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        Mi_ShareEntities init();
    }
}
