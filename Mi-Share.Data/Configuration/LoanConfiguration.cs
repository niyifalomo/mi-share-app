using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Data.Configuration
{
    public class LoanConfiguration: EntityTypeConfiguration<Loan>
    {
        public LoanConfiguration()
        {
            ToTable("Loan");
            Property(s => s.Request_ID).IsRequired();
            Property(s => s.ReturnDate).IsOptional();
            Property(s => s.BeginDate).IsRequired();
            Property(s => s.Status).IsRequired();
            HasRequired(s => s.Request).WithMany(s => s.Loan).HasForeignKey(s=>s.Request_ID).WillCascadeOnDelete(false);
        }
    }
}
