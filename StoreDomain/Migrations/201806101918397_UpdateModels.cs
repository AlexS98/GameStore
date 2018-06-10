namespace StoreDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "AdminId", newName: "Admin_AdminId");
            RenameColumn(table: "dbo.Comments", name: "GameId", newName: "Game_GameId");
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "User_UserId");
            RenameColumn(table: "dbo.UserCabinets", name: "UserId", newName: "User_UserId");
            AlterColumn("dbo.Comments", "Admin_AdminId", c => c.Int());
            AlterColumn("dbo.Games", "Addition_GameAdditionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "Developer_CompanyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "Publisher_CompanyId", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "PublisherId");
            DropColumn("dbo.Games", "DeveloperId");
            DropColumn("dbo.Games", "AdditionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "AdditionId", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "DeveloperId", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "PublisherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "Publisher_CompanyId", c => c.Int());
            AlterColumn("dbo.Games", "Developer_CompanyId", c => c.Int());
            AlterColumn("dbo.Games", "Addition_GameAdditionId", c => c.Int());
            AlterColumn("dbo.Comments", "Admin_AdminId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserCabinets", name: "User_UserId", newName: "UserId");
            RenameColumn(table: "dbo.Comments", name: "User_UserId", newName: "UserId");
            RenameColumn(table: "dbo.Comments", name: "Game_GameId", newName: "GameId");
            RenameColumn(table: "dbo.Comments", name: "Admin_AdminId", newName: "AdminId");
        }
    }
}
