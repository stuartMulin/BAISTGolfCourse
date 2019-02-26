using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TheBackEndLayer.Infrastructure.Maps;

namespace TheBackEndLayer.DbModels
{
    public class BAISTGolfCourseDbContext : DbContext
    {
        public BAISTGolfCourseDbContext() :
            base("BAISTGolfCourseDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<GolfCourse> GolfCourses { get; set; }

        public virtual DbSet<HandiCap> HandCap { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<PlayerScores> Scores { get; set; }
        public virtual DbSet<TeeTime> TeeTime { get; set; }
        public virtual DbSet<Hole> Hole { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Members>().HasKey(x => x.ID);

        //    modelBuilder.Entity<Reservations>()
        //    .HasMany(d => d.PlayerScores)
        //    .WithRequired(w => w.Reservation)
        //    .HasForeignKey(x => x.ReservationID)
        //    .WillCascadeOnDelete(false);

        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicantsMap());
            modelBuilder.Configurations.Add(new MembersMap());
            modelBuilder.Configurations.Add(new EmployeesMap());
            modelBuilder.Configurations.Add(new ReservationMap());
            modelBuilder.Configurations.Add(new TeeTimeMap());
            modelBuilder.Configurations.Add(new HoleMap());
            modelBuilder.Configurations.Add(new PlayerScoreMap());
            modelBuilder.Configurations.Add(new GolfCourseMap());
            modelBuilder.Configurations.Add(new HandicapMap());
        }

    }
}
