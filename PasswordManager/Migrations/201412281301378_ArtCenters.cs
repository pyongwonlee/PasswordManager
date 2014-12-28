namespace PasswordManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtCenters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Centers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        WebAddress = c.String(maxLength: 250),
                        Description = c.String(),
                        Note = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Abbreviation = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Centers", "CityId", "dbo.Cities");
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.Centers", new[] { "CityId" });
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.Centers");
        }
    }
}
