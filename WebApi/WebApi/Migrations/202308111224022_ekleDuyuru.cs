namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ekleDuyuru : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bildiris", "FotografId", "dbo.Fotografs");
            CreateTable(
                "dbo.BildiriFotografs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DuyuruFotografs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Duyurus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(),
                        Text = c.String(),
                        FotografId = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DuyuruFotografs", t => t.FotografId, cascadeDelete: true)
                .Index(t => t.FotografId);
            
            CreateTable(
                "dbo.Sms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        Text = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanicis", t => t.KullaniciId, cascadeDelete: true)
                .Index(t => t.KullaniciId);
            
            AddColumn("dbo.AltKategoris", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Kategoris", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Bildiris", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Konums", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Kullanicis", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Kisis", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.BelBilgis", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Harekets", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Islems", "KayitTarihi", c => c.DateTime());
            AddColumn("dbo.Sayfas", "KayitTarihi", c => c.DateTime());
            AddForeignKey("dbo.Bildiris", "FotografId", "dbo.BildiriFotografs", "Id", cascadeDelete: true);
            DropColumn("dbo.AltKategoris", "KayıtTarihi");
            DropColumn("dbo.Kategoris", "KayıtTarihi");
            DropColumn("dbo.Bildiris", "KayıtTarihi");
            DropColumn("dbo.Konums", "KayıtTarihi");
            DropColumn("dbo.Kullanicis", "KayıtTarihi");
            DropColumn("dbo.Kisis", "KayıtTarihi");
            DropColumn("dbo.BelBilgis", "KayıtTarihi");
            DropColumn("dbo.Harekets", "KayıtTarihi");
            DropColumn("dbo.Islems", "KayıtTarihi");
            DropColumn("dbo.Sayfas", "KayıtTarihi");
            DropTable("dbo.Fotografs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Fotografs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        KayıtTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sayfas", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.Islems", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.Harekets", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.BelBilgis", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.Kisis", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.Kullanicis", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.Konums", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.Bildiris", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.Kategoris", "KayıtTarihi", c => c.DateTime());
            AddColumn("dbo.AltKategoris", "KayıtTarihi", c => c.DateTime());
            DropForeignKey("dbo.Sms", "KullaniciId", "dbo.Kullanicis");
            DropForeignKey("dbo.Duyurus", "FotografId", "dbo.DuyuruFotografs");
            DropForeignKey("dbo.Bildiris", "FotografId", "dbo.BildiriFotografs");
            DropIndex("dbo.Sms", new[] { "KullaniciId" });
            DropIndex("dbo.Duyurus", new[] { "FotografId" });
            DropColumn("dbo.Sayfas", "KayitTarihi");
            DropColumn("dbo.Islems", "KayitTarihi");
            DropColumn("dbo.Harekets", "KayitTarihi");
            DropColumn("dbo.BelBilgis", "KayitTarihi");
            DropColumn("dbo.Kisis", "KayitTarihi");
            DropColumn("dbo.Kullanicis", "KayitTarihi");
            DropColumn("dbo.Konums", "KayitTarihi");
            DropColumn("dbo.Bildiris", "KayitTarihi");
            DropColumn("dbo.Kategoris", "KayitTarihi");
            DropColumn("dbo.AltKategoris", "KayitTarihi");
            DropTable("dbo.Sms");
            DropTable("dbo.Duyurus");
            DropTable("dbo.DuyuruFotografs");
            DropTable("dbo.BildiriFotografs");
            AddForeignKey("dbo.Bildiris", "FotografId", "dbo.Fotografs", "Id", cascadeDelete: true);
        }
    }
}
