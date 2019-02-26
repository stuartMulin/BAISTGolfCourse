using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Infrastructure.Maps
{
    class HoleMap : EntityTypeConfiguration<Hole>
    {
        public HoleMap()
        {
            HasKey(x => x.ID);

            Property(x => x.Name).HasMaxLength(80);

            HasMany(x => x.PlayerScores)
                    .WithRequired(x => x.Hole)
                    .HasForeignKey(x => x.HoleId)
                    .WillCascadeOnDelete(false);
        }
    }
    public class PlayerScoreMap : EntityTypeConfiguration<PlayerScores>
    {
        public PlayerScoreMap()
        {
            HasKey(x => x.Id);

            Property(x => x.HandicapId).IsOptional();

        }
    }
    public class HandicapMap : EntityTypeConfiguration<HandiCap>
    {
        public HandicapMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Name).HasMaxLength(80);

            HasMany(x => x.PlayerScore)
                    .WithOptional(x => x.Handicap)
                    .HasForeignKey(x => x.HandicapId)
                    .WillCascadeOnDelete(false);
        }
    }
    public class GolfCourseMap : EntityTypeConfiguration<GolfCourse>
    {
        public GolfCourseMap()
        {
            HasKey(x => x.ID);

            Property(x => x.CourseName).HasMaxLength(300);

            Property(x => x.Rating).IsRequired();

            Property(x => x.Slope).IsRequired();

            Property(x => x.Address).HasMaxLength(150);

            Property(x => x.City).HasMaxLength(100);

            Property(x => x.Country).HasMaxLength(150);

            HasMany(x => x.TeeTimes)
                    .WithRequired(x => x.GolfCourse)
                    .HasForeignKey(x => x.GolfCourseID)
                    .WillCascadeOnDelete(false);
        }
    }
}


