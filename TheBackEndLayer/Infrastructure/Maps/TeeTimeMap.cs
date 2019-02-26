using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Infrastructure.Maps
{
    class TeeTimeMap : EntityTypeConfiguration<TeeTime>
    {
        public TeeTimeMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Status).IsRequired();

            HasMany(x => x.Reservations)
                    .WithRequired(x => x.TeeTime)
                    .HasForeignKey(x => x.TeeTimeID)
                    .WillCascadeOnDelete(false);

            Property(x => x.StartDate).IsRequired();

            Property(x => x.EndDate).IsRequired();

            Property(x => x.DateCreated).IsRequired();
        }
    }
}
