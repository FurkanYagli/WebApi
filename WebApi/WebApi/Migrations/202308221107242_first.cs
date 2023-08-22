namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GuidIds", "KisiId", "dbo.Kisis");
            DropIndex("dbo.GuidIds", new[] { "KisiId" });
            DropTable("dbo.GuidIds");
            DropTable("dbo.GuidTurs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GuidTurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidFlag = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GuidIds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidTuru = c.Int(nullable: false),
                        guidId = c.Guid(nullable: false),
                        KisiId = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.GuidIds", "KisiId");
            AddForeignKey("dbo.GuidIds", "KisiId", "dbo.Kisis", "Id", cascadeDelete: true);
        }
    }
}
