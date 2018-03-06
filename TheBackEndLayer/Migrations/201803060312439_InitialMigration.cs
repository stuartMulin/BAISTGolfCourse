namespace TheBackEndLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Role = c.Int(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GolfCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        Rating = c.Double(nullable: false),
                        Slope = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TeeTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        TeeState = c.Int(nullable: false),
                        GolfCourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GolfCourses", t => t.GolfCourseID, cascadeDelete: true)
                .Index(t => t.GolfCourseID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeeTimeID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.MemberID, cascadeDelete: true)
                .ForeignKey("dbo.TeeTimes", t => t.TeeTimeID, cascadeDelete: true)
                .Index(t => t.TeeTimeID)
                .Index(t => t.MemberID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        PasswordSalt = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Gender = c.Int(nullable: false),
                        City = c.String(),
                        Province = c.String(),
                        PostalCode = c.String(),
                        Phone = c.String(nullable: false),
                        AlternatePhone = c.String(),
                        MembershipType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        HoleId = c.Int(nullable: false),
                        HandicapId = c.Int(nullable: false),
                        ReservationID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                        Slope = c.Int(nullable: false),
                        DatePlayed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HandiCaps", t => t.HandicapId, cascadeDelete: true)
                .ForeignKey("dbo.Holes", t => t.HoleId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationID)
                .Index(t => t.HoleId)
                .Index(t => t.HandicapId)
                .Index(t => t.ReservationID);
            
            CreateTable(
                "dbo.HandiCaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Holes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "TeeTimeID", "dbo.TeeTimes");
            DropForeignKey("dbo.Scores", "ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.Scores", "HoleId", "dbo.Holes");
            DropForeignKey("dbo.Scores", "HandicapId", "dbo.HandiCaps");
            DropForeignKey("dbo.Reservations", "MemberID", "dbo.Members");
            DropForeignKey("dbo.TeeTimes", "GolfCourseID", "dbo.GolfCourses");
            DropIndex("dbo.Scores", new[] { "ReservationID" });
            DropIndex("dbo.Scores", new[] { "HandicapId" });
            DropIndex("dbo.Scores", new[] { "HoleId" });
            DropIndex("dbo.Reservations", new[] { "MemberID" });
            DropIndex("dbo.Reservations", new[] { "TeeTimeID" });
            DropIndex("dbo.TeeTimes", new[] { "GolfCourseID" });
            DropTable("dbo.Users");
            DropTable("dbo.Logins");
            DropTable("dbo.Holes");
            DropTable("dbo.HandiCaps");
            DropTable("dbo.Scores");
            DropTable("dbo.Members");
            DropTable("dbo.Reservations");
            DropTable("dbo.TeeTimes");
            DropTable("dbo.GolfCourses");
            DropTable("dbo.Employees");
        }
    }
}
