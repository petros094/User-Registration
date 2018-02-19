namespace Task_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creatdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Lastname = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(),
                        Age = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Login, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Login" });
            DropTable("dbo.Users");
        }
    }
}
