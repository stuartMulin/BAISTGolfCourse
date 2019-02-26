using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Infrastructure.Maps
{
   public  class ApplicantsMap : EntityTypeConfiguration<Applicants>
    {
        public ApplicantsMap()
        {
            HasKey(x => x.ID);

            Property(x => x.FirstName).HasMaxLength(50).IsRequired();

            Property(x => x.Gender).IsRequired();

            Property(x => x.RejectionReason).IsOptional();

            Property(x => x.LastName).HasMaxLength(70).IsRequired();

            Property(x => x.EmailAddress).HasMaxLength(80).IsRequired();

            Property(x => x.HasShareHolderOneConfirmed).IsOptional();

            Property(x => x.HasShareHolderTwoConfirmed).IsOptional();

            Property(x => x.Password).HasMaxLength(600).IsRequired();

            Property(x => x.PasswordSalt).HasMaxLength(10).IsRequired();

            Property(x => x.Address1).HasMaxLength(150).IsRequired();

            Property(x => x.Address2).HasMaxLength(150).IsOptional();

            Property(x => x.Phone).HasMaxLength(15).IsRequired();

            Property(x => x.AlternatePhone).HasMaxLength(15).IsOptional();

            HasMany(x => x.ProspectiveMembers)
                    .WithOptional(x => x.PreviousApplication)
                    .HasForeignKey(x => x.ApplicantID)
                    .WillCascadeOnDelete(false);

            Property(x => x.City).HasMaxLength(50).IsRequired();

            Property(x => x.Province).HasMaxLength(60).IsRequired();

            Property(x => x.PostalCode).HasMaxLength(8).IsRequired();

            Property(x => x.DateOfBirth).IsRequired();

            Property(x => x.DateCreated).IsRequired();
        }

    }
}
