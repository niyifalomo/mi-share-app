using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
