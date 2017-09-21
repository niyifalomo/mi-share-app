using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Model;
using System.Data.Entity.ModelConfiguration;

namespace Mi_Share.Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration() {
            ToTable("User");
            Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            Property(x => x.LastName).IsRequired().HasMaxLength(50);
            Property(x => x.UserName).IsRequired().HasMaxLength(150);
            Property(x => x.Password).IsRequired();

        }
    }
}
