using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Infrastructure.Maps
{
    public class EmployeesMap : EntityTypeConfiguration<Employees>
    {
        public EmployeesMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Gender).IsRequired();

            Property(x => x.FirstName).HasMaxLength(50).IsRequired();

            Property(x => x.LastName).HasMaxLength(70).IsRequired();

            Property(x => x.EmailAddress).HasMaxLength(80).IsRequired();

            Property(x => x.Password).HasMaxLength(600).IsRequired();

            Property(x => x.PasswordSalt).HasMaxLength(10).IsRequired();

            Property(x => x.Address1).HasMaxLength(150).IsRequired();

            Property(x => x.Address2).HasMaxLength(150).IsOptional();

            Property(x => x.Phone).HasMaxLength(15).IsRequired();

            Property(x => x.AlternatePhone).HasMaxLength(15).IsOptional();

            Property(x => x.Role).IsRequired();

            Property(x => x.City).HasMaxLength(50).IsRequired();

            Property(x => x.Province).HasMaxLength(60).IsRequired();

            Property(x => x.PCode).HasMaxLength(8).IsRequired();

            Property(x => x.DateOfBirth).IsRequired();

            Property(x => x.DateCreated).IsRequired();
        }
       
    }
}
