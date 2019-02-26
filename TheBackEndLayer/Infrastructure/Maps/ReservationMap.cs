using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Infrastructure.Maps
{
   public class ReservationMap : EntityTypeConfiguration<Reservations>
    {
    public ReservationMap()
    {
        HasKey(x => x.ID);

        Property(x => x.DateCreated).IsRequired();

        Property(x => x.Status).IsOptional();
    }
}
}
