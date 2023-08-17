namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bans", "Aktif", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Bans", "KisiId");
            AddForeignKey("dbo.Bans", "KisiId", "dbo.Kisis", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bans", "KisiId", "dbo.Kisis");
            DropIndex("dbo.Bans", new[] { "KisiId" });
            DropColumn("dbo.Bans", "Aktif");
        }
    }
}
