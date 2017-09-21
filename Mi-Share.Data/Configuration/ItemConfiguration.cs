using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Model;
using System.Data.Entity.ModelConfiguration;

namespace Mi_Share.Data.Configuration
{
    class ItemConfiguration: EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            ToTable("Item");
            Property(x => x.Name).IsRequired().HasMaxLength(150);
            Property(x => x.Description).IsOptional().HasMaxLength(250);
            Property(x => x.Category_ID).IsRequired();
            Property(x => x.Status).IsRequired();
            Property(s => s.DateCreated).IsRequired();
            Property(s => s.IsDeleted).IsRequired();
            Property(s => s.Owner_ID).IsRequired();
            Property(s => s.DeletedDate).IsOptional();
            Property(s => s.DeletedBy).IsOptional();
            Property(s => s.Updated_At).IsOptional();
            Property(s => s.Updated_By).IsOptional();
            HasRequired(s => s.Category).WithMany(x => x.Items).HasForeignKey(s => s.Category_ID).WillCascadeOnDelete(false);
            HasRequired(s => s.Owner).WithMany(x => x.Items).HasForeignKey(s => s.Owner_ID).WillCascadeOnDelete(false);
        }
    }
}
