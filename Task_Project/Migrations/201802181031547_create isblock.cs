namespace Task_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createisblock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsBlocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsBlocked");
        }
    }
}
