namespace GameStore.StoreDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Nickname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        NewText = c.String(),
                        NewTime = c.DateTime(nullable: false),
                        Admin_AdminId = c.Int(),
                        Game_GameId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Admins", t => t.Admin_AdminId)
                .ForeignKey("dbo.Games", t => t.Game_GameId)
                .ForeignKey("dbo.Users", t => t.User_UserId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Version = c.String(nullable: false),
                        GameType = c.Int(nullable: false),
                        GamePlatforms = c.Int(nullable: false),
                        PosterImageData = c.Binary(),
                        PosterImageMimeType = c.String(),
                        Addition_GameAdditionId = c.Int(nullable: false),
                        Developer_CompanyId = c.Int(nullable: false),
                        Publisher_CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.GameAdditions", t => t.Addition_GameAdditionId)
                .ForeignKey("dbo.Companies", t => t.Developer_CompanyId)
                .ForeignKey("dbo.Companies", t => t.Publisher_CompanyId);
            
            CreateTable(
                "dbo.GameAdditions",
                c => new
                    {
                        GameAdditionId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Url = c.String(),
                        Screen1ImageData = c.Binary(),
                        Screen1ImageMimeType = c.String(),
                        Screen2ImageData = c.Binary(),
                        Screen2ImageMimeType = c.String(),
                        Screen3ImageData = c.Binary(),
                        Screen3ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.GameAdditionId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        FullName = c.String(nullable: false),
                        LogoImageData = c.Binary(),
                        LogoImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Nickname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserCabinets",
                c => new
                    {
                        UserCabinetId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        AvatarImageData = c.Binary(),
                        AvatarImageMimeType = c.String(),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserCabinetId)
                .ForeignKey("dbo.Users", t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCabinets", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "Game_GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "Publisher_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Games", "Developer_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Games", "Addition_GameAdditionId", "dbo.GameAdditions");
            DropForeignKey("dbo.Comments", "Admin_AdminId", "dbo.Admins");
            DropTable("dbo.UserCabinets");
            DropTable("dbo.Users");
            DropTable("dbo.Companies");
            DropTable("dbo.GameAdditions");
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
            DropTable("dbo.Admins");
        }
    }
}
