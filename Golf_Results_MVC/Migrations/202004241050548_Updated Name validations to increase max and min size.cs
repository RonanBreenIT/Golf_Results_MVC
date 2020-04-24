namespace Golf_Results_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedNamevalidationstoincreasemaxandminsize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Competition", "Name", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Golfer", "Firstname", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Golfer", "Surname", c => c.String(nullable: false, maxLength: 80));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Golfer", "Surname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Golfer", "Firstname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Competition", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
