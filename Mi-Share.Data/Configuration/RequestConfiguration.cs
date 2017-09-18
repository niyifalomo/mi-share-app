using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Data.Configuration
{
    public class RequestConfiguration:EntityTypeConfiguration<Request>
    {
        public RequestConfiguration()
        {
            ToTable("Request");
            Property(s => s.Item_ID).IsRequired();
            Property(s => s.Requester_ID).IsRequired();
            Property(s => s.DateCreated).IsRequired();
            Property(s => s.Status).IsRequired();
            Property(s => s.Updated_At).IsOptional();
            Property(s => s.Updated_By).IsOptional();
            HasRequired(s => s.Item).WithMany(x => x.Requests).HasForeignKey(x => x.Item_ID).WillCascadeOnDelete(false);
            HasRequired(s => s.Requester).WithMany(x => x.ItemRequests).HasForeignKey(x => x.Requester_ID).WillCascadeOnDelete(false);
        }
    }
}
