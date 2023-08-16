namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AltKategoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KategoriId = c.Int(nullable: false),
                        Ad = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategoris", t => t.KategoriId, cascadeDelete: true)
                .Index(t => t.KategoriId);
            
            CreateTable(
                "dbo.Kategoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Bildiris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AltKategoriId = c.Int(),
                        KullaniciId = c.Int(nullable: false),
                        KonumId = c.Int(),
                        Aciklama = c.String(),
                        FotografId = c.Int(),
                        Aktif = c.Boolean(nullable: false),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AltKategoris", t => t.AltKategoriId)
                .ForeignKey("dbo.BildiriFotografs", t => t.FotografId)
                .ForeignKey("dbo.Konums", t => t.KonumId)
                .ForeignKey("dbo.Kullanicis", t => t.KullaniciId, cascadeDelete: true)
                .Index(t => t.AltKategoriId)
                .Index(t => t.KullaniciId)
                .Index(t => t.KonumId)
                .Index(t => t.FotografId);
            
            CreateTable(
                "dbo.Konums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        x = c.String(),
                        y = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kullanicis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Aktif = c.Boolean(nullable: false),
                        KisiId = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kisis", t => t.KisiId, cascadeDelete: true)
                .Index(t => t.KisiId);
            
            CreateTable(
                "dbo.Kisis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Aktif = c.Boolean(nullable: false),
                        Tc = c.String(),
                        Tel = c.String(),
                        Ad = c.String(),
                        Soyad = c.String(),
                        Mail = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BelBilgis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adres = c.String(),
                        Telefon = c.String(),
                        Fax = c.String(),
                        Mail = c.String(),
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
                "dbo.Gikoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BildiriId = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bildiris", t => t.BildiriId, cascadeDelete: true)
                .Index(t => t.BildiriId);
            
            CreateTable(
                "dbo.Harekets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        SayfaId = c.Int(),
                        IslemId = c.Int(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Islems", t => t.IslemId)
                .ForeignKey("dbo.Kullanicis", t => t.KullaniciId, cascadeDelete: true)
                .ForeignKey("dbo.Sayfas", t => t.SayfaId)
                .Index(t => t.KullaniciId)
                .Index(t => t.SayfaId)
                .Index(t => t.IslemId);
            
            CreateTable(
                "dbo.Islems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sayfas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KisiId = c.Int(nullable: false),
                        Text = c.String(),
                        Kod = c.String(),
                        GidenTel = c.String(),
                        KayitTarihi = c.DateTime(),
                        GuncellemeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kisis", t => t.KisiId, cascadeDelete: true)
                .Index(t => t.KisiId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sms", "KisiId", "dbo.Kisis");
            DropForeignKey("dbo.Harekets", "SayfaId", "dbo.Sayfas");
            DropForeignKey("dbo.Harekets", "KullaniciId", "dbo.Kullanicis");
            DropForeignKey("dbo.Harekets", "IslemId", "dbo.Islems");
            DropForeignKey("dbo.Gikoms", "BildiriId", "dbo.Bildiris");
            DropForeignKey("dbo.Duyurus", "FotografId", "dbo.DuyuruFotografs");
            DropForeignKey("dbo.Bildiris", "KullaniciId", "dbo.Kullanicis");
            DropForeignKey("dbo.Kullanicis", "KisiId", "dbo.Kisis");
            DropForeignKey("dbo.Bildiris", "KonumId", "dbo.Konums");
            DropForeignKey("dbo.Bildiris", "FotografId", "dbo.BildiriFotografs");
            DropForeignKey("dbo.Bildiris", "AltKategoriId", "dbo.AltKategoris");
            DropForeignKey("dbo.AltKategoris", "KategoriId", "dbo.Kategoris");
            DropIndex("dbo.Sms", new[] { "KisiId" });
            DropIndex("dbo.Harekets", new[] { "IslemId" });
            DropIndex("dbo.Harekets", new[] { "SayfaId" });
            DropIndex("dbo.Harekets", new[] { "KullaniciId" });
            DropIndex("dbo.Gikoms", new[] { "BildiriId" });
            DropIndex("dbo.Duyurus", new[] { "FotografId" });
            DropIndex("dbo.Kullanicis", new[] { "KisiId" });
            DropIndex("dbo.Bildiris", new[] { "FotografId" });
            DropIndex("dbo.Bildiris", new[] { "KonumId" });
            DropIndex("dbo.Bildiris", new[] { "KullaniciId" });
            DropIndex("dbo.Bildiris", new[] { "AltKategoriId" });
            DropIndex("dbo.AltKategoris", new[] { "KategoriId" });
            DropTable("dbo.Sms");
            DropTable("dbo.Sayfas");
            DropTable("dbo.Islems");
            DropTable("dbo.Harekets");
            DropTable("dbo.Gikoms");
            DropTable("dbo.Duyurus");
            DropTable("dbo.DuyuruFotografs");
            DropTable("dbo.BelBilgis");
            DropTable("dbo.Kisis");
            DropTable("dbo.Kullanicis");
            DropTable("dbo.Konums");
            DropTable("dbo.Bildiris");
            DropTable("dbo.BildiriFotografs");
            DropTable("dbo.Kategoris");
            DropTable("dbo.AltKategoris");
        }
    }
}
