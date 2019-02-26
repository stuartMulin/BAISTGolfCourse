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
                        EmailAddress = c.String(nullable: false, maxLength: 80),
                        Password = c.String(nullable: false, maxLength: 600),
                        PasswordSalt = c.String(nullable: false, maxLength: 10),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 70),
                        UserName = c.String(),
                        Role = c.Int(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 150),
                        Address2 = c.String(maxLength: 150),
                        Phone = c.String(nullable: false, maxLength: 15),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Province = c.String(nullable: false, maxLength: 60),
                        PCode = c.String(nullable: false, maxLength: 8),
                        AlternatePhone = c.String(maxLength: 15),
                        City = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GolfCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseName = c.String(maxLength: 300),
                        Rating = c.Double(nullable: false),
                        Slope = c.Int(nullable: false),
                        Address = c.String(maxLength: 150),
                        Country = c.String(maxLength: 150),
                        City = c.String(maxLength: 100),
                        YearFounded = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
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
                        Status = c.Int(nullable: false),
                        GolfCourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GolfCourses", t => t.GolfCourseID)
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
                        Status = c.Int(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.MemberID)
                .ForeignKey("dbo.TeeTimes", t => t.TeeTimeID)
                .Index(t => t.TeeTimeID)
                .Index(t => t.MemberID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 70),
                        EmailAddress = c.String(nullable: false, maxLength: 80),
                        Password = c.String(nullable: false, maxLength: 600),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ApplicantID = c.Int(),
                        PasswordSalt = c.String(nullable: false, maxLength: 10),
                        Address1 = c.String(nullable: false, maxLength: 150),
                        Address2 = c.String(maxLength: 150),
                        Gender = c.Int(nullable: false),
                        City = c.String(nullable: false, maxLength: 50),
                        Province = c.String(nullable: false, maxLength: 60),
                        PostalCode = c.String(nullable: false, maxLength: 8),
                        Phone = c.String(nullable: false, maxLength: 15),
                        AlternatePhone = c.String(maxLength: 15),
                        MembershipType = c.String(),
                        MembershipID = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicants", t => t.ApplicantID)
                .Index(t => t.ApplicantID);
            
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 70),
                        EmailAddress = c.String(nullable: false, maxLength: 80),
                        Status = c.Int(nullable: false),
                        RejectionReason = c.Int(),
                        Gender = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 600),
                        PasswordSalt = c.String(nullable: false, maxLength: 10),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 150),
                        Address2 = c.String(maxLength: 150),
                        City = c.String(nullable: false, maxLength: 50),
                        Province = c.String(nullable: false, maxLength: 60),
                        PostalCode = c.String(nullable: false, maxLength: 8),
                        HasShareHolderOneConfirmed = c.Boolean(),
                        HasShareHolderTwoConfirmed = c.Boolean(),
                        Phone = c.String(nullable: false, maxLength: 15),
                        AlternatePhone = c.String(maxLength: 15),
                        ShareHolder1ID = c.Int(nullable: false),
                        ShareHolder2ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.ShareHolder1ID, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.ShareHolder2ID)
                .Index(t => t.ShareHolder1ID)
                .Index(t => t.ShareHolder2ID);
            
            CreateTable(
                "dbo.PlayerScores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        HoleId = c.Int(nullable: false),
                        HandicapId = c.Int(),
                        ReservationID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                        Slope = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DatePlayed = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HandiCaps", t => t.HandicapId)
                .ForeignKey("dbo.Holes", t => t.HoleId)
                .ForeignKey("dbo.Reservations", t => t.ReservationID, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberID)
                .Index(t => t.HoleId)
                .Index(t => t.HandicapId)
                .Index(t => t.ReservationID)
                .Index(t => t.MemberID);
            
            CreateTable(
                "dbo.HandiCaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Holes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeeTimes", "GolfCourseID", "dbo.GolfCourses");
            DropForeignKey("dbo.Reservations", "TeeTimeID", "dbo.TeeTimes");
            DropForeignKey("dbo.PlayerScores", "MemberID", "dbo.Members");
            DropForeignKey("dbo.PlayerScores", "ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.PlayerScores", "HoleId", "dbo.Holes");
            DropForeignKey("dbo.PlayerScores", "HandicapId", "dbo.HandiCaps");
            DropForeignKey("dbo.Reservations", "MemberID", "dbo.Members");
            DropForeignKey("dbo.Applicants", "ShareHolder2ID", "dbo.Members");
            DropForeignKey("dbo.Applicants", "ShareHolder1ID", "dbo.Members");
            DropForeignKey("dbo.Members", "ApplicantID", "dbo.Applicants");
            DropIndex("dbo.PlayerScores", new[] { "MemberID" });
            DropIndex("dbo.PlayerScores", new[] { "ReservationID" });
            DropIndex("dbo.PlayerScores", new[] { "HandicapId" });
            DropIndex("dbo.PlayerScores", new[] { "HoleId" });
            DropIndex("dbo.Applicants", new[] { "ShareHolder2ID" });
            DropIndex("dbo.Applicants", new[] { "ShareHolder1ID" });
            DropIndex("dbo.Members", new[] { "ApplicantID" });
            DropIndex("dbo.Reservations", new[] { "MemberID" });
            DropIndex("dbo.Reservations", new[] { "TeeTimeID" });
            DropIndex("dbo.TeeTimes", new[] { "GolfCourseID" });
            DropTable("dbo.Holes");
            DropTable("dbo.HandiCaps");
            DropTable("dbo.PlayerScores");
            DropTable("dbo.Applicants");
            DropTable("dbo.Members");
            DropTable("dbo.Reservations");
            DropTable("dbo.TeeTimes");
            DropTable("dbo.GolfCourses");
            DropTable("dbo.Employees");
        }
    }
}
