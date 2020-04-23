namespace Golf_Results_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Enablingusers : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Competition", "major");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Competition", "major", c => c.Boolean(nullable: false));
        }
    }
}
