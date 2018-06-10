namespace StoreDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
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
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                        NewText = c.String(),
                        NewTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Users", t => t.UserId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Version = c.String(nullable: false),
                        GameType = c.Int(nullable: false),
                        GamePlatforms = c.Int(nullable: false),
                        AdditionId = c.Int(nullable: false),
                        Addition_GameAdditionId = c.Int(),
                        Developer_CompanyId = c.Int(),
                        Publisher_CompanyId = c.Int(),
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
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        AvatarImageData = c.Binary(),
                        AvatarImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.UserCabinetId)
                .ForeignKey("dbo.Users", t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCabinets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Games", "Publisher_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Games", "Developer_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Comments", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "Addition_GameAdditionId", "dbo.GameAdditions");
            DropForeignKey("dbo.Comments", "AdminId", "dbo.Admins");
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
