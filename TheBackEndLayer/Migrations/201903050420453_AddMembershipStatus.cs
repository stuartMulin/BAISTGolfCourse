namespace TheBackEndLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "MembershipType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "MembershipType", c => c.String());
        }
    }
}
