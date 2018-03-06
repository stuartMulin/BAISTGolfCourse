using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.DbModels
{
    public class BAISTGolfCourseDbContext : DbContext
    {
        public BAISTGolfCourseDbContext() :
            base("BAISTGolfCourseDbContext")
        {

        }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<GolfCourse> GolfCourses { get; set; }

        public virtual DbSet<HandiCap> HandCap { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Scores> Scores { get; set; }
        public virtual DbSet<TeeTime> TeeTime { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Hole> Hole { get; set; }

        public static BAISTGolfCourseDbContext Create()
        {
            return new BAISTGolfCourseDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Members>().HasKey(x => x.ID);

            modelBuilder.Entity<Reservations>()
            .HasMany(d => d.PlayerScores)
            .WithRequired(w => w.Reservation)
            .HasForeignKey(x => x.ReservationID)
            .WillCascadeOnDelete(false);

        }
        
    }
}
