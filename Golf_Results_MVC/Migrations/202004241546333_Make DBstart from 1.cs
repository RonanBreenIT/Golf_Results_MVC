namespace Golf_Results_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDBstartfrom1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comp_Result", "CompetitionID", "dbo.Competition");
            DropForeignKey("dbo.Comp_Result", "GolferID", "dbo.Golfer");
            DropPrimaryKey("dbo.Competition");
            DropPrimaryKey("dbo.Golfer");
            AlterColumn("dbo.Competition", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Golfer", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Competition", "ID");
            AddPrimaryKey("dbo.Golfer", "ID");
            AddForeignKey("dbo.Comp_Result", "CompetitionID", "dbo.Competition", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Comp_Result", "GolferID", "dbo.Golfer", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comp_Result", "GolferID", "dbo.Golfer");
            DropForeignKey("dbo.Comp_Result", "CompetitionID", "dbo.Competition");
            DropPrimaryKey("dbo.Golfer");
            DropPrimaryKey("dbo.Competition");
            AlterColumn("dbo.Golfer", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Competition", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Golfer", "ID");
            AddPrimaryKey("dbo.Competition", "ID");
            AddForeignKey("dbo.Comp_Result", "GolferID", "dbo.Golfer", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Comp_Result", "CompetitionID", "dbo.Competition", "ID", cascadeDelete: true);
        }
    }
}
