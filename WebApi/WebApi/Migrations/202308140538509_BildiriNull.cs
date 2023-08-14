namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BildiriNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bildiris", "AltKategoriId", "dbo.Bildiris");
            DropForeignKey("dbo.Bildiris", "FotografId", "dbo.BildiriFotografs");
            DropForeignKey("dbo.Bildiris", "KonumId", "dbo.Konums");
            DropIndex("dbo.Bildiris", new[] { "AltKategoriId" });
            DropIndex("dbo.Bildiris", new[] { "KonumId" });
            DropIndex("dbo.Bildiris", new[] { "FotografId" });
            AlterColumn("dbo.Bildiris", "AltKategoriId", c => c.Int());
            AlterColumn("dbo.Bildiris", "KonumId", c => c.Int());
            AlterColumn("dbo.Bildiris", "FotografId", c => c.Int());
            CreateIndex("dbo.Bildiris", "AltKategoriId");
            CreateIndex("dbo.Bildiris", "KonumId");
            CreateIndex("dbo.Bildiris", "FotografId");
            AddForeignKey("dbo.Bildiris", "AltKategoriId", "dbo.AltKategoris", "Id");
            AddForeignKey("dbo.Bildiris", "FotografId", "dbo.BildiriFotografs", "Id");
            AddForeignKey("dbo.Bildiris", "KonumId", "dbo.Konums", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bildiris", "KonumId", "dbo.Konums");
            DropForeignKey("dbo.Bildiris", "FotografId", "dbo.BildiriFotografs");
            DropForeignKey("dbo.Bildiris", "AltKategoriId", "dbo.AltKategoris");
            DropIndex("dbo.Bildiris", new[] { "FotografId" });
            DropIndex("dbo.Bildiris", new[] { "KonumId" });
            DropIndex("dbo.Bildiris", new[] { "AltKategoriId" });
            AlterColumn("dbo.Bildiris", "FotografId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bildiris", "KonumId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bildiris", "AltKategoriId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bildiris", "FotografId");
            CreateIndex("dbo.Bildiris", "KonumId");
            CreateIndex("dbo.Bildiris", "AltKategoriId");
            AddForeignKey("dbo.Bildiris", "KonumId", "dbo.Konums", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bildiris", "FotografId", "dbo.BildiriFotografs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bildiris", "AltKategoriId", "dbo.Bildiris", "Id");
        }
    }
}
