using Mi_Share.Data.Infrastructure;
using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Data.Repositories
{
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository { 
        public LoanRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
    public interface ILoanRepository : IRepository<Loan>
    {

    }
}
