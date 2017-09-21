using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Threading.Tasks;

namespace Mi_Share.Data.Configuration
{
    public class CollectionAccessConfiguration : EntityTypeConfiguration<CollectionAccess>
    {
        public CollectionAccessConfiguration()
        {
            ToTable("CollectionAccess");
            Property(s => s.DateRequested).IsRequired();
            Property(s => s.DateUpdated).IsOptional();
            Property(s => s.Status).IsRequired();
            Property(s => s.Owner_ID).IsRequired();
            Property(s => s.Requester_ID).IsRequired();
            HasRequired(s => s.Owner).WithMany(s => s.RecievedCollectionAccess).HasForeignKey(s => s.Owner_ID).WillCascadeOnDelete(false);
            HasRequired(s => s.Requester).WithMany(s => s.SentCollectionAccessRequests).HasForeignKey(s => s.Requester_ID).WillCascadeOnDelete(false);
        }
    }
}
