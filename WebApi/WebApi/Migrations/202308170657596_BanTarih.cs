namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BanTarih : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bans", "BitisTarihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bans", "BitisTatihi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bans", "BitisTatihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bans", "BitisTarihi");
        }
    }
}
