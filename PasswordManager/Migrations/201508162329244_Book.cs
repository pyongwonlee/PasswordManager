namespace PasswordManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Book : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Author = c.String(nullable: false, maxLength: 250),
                        Title = c.String(nullable: false, maxLength: 250),
                        Year = c.Int(nullable: false),
                        Publisher = c.String(maxLength: 250),
                        Location = c.String(maxLength: 250),
                        ISBN = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
