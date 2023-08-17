namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ban : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bans", "BitisTatihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bans", "BanSuresi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bans", "BanSuresi", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bans", "BitisTatihi");
        }
    }
}
