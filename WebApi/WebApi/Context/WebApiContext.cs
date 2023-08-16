using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Context
{
    public class WebApiContext : DbContext
    {
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Bildiri> Bildiriler { get; set; }
        public DbSet<BelBilgi> Bilgiler { get; set; }
        public DbSet<DuyuruFotograf> DuyuruFotograflar { get; set; }
        public DbSet<BildiriFotograf> BildiriFotograflar { get; set; }
        public DbSet<Hareket> Hareketler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<AltKategori> AltKategoriler { get; set; }
        public DbSet<Konum> Konumlar { get; set; }
        public DbSet<Sayfa> Sayfalar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Sms> Smsler { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<Gikom> GikomBilgiler { get; set; }
    }
}