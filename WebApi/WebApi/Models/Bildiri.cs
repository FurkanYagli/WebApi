using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Bildiri:Entity
    {
        public int AltKategoriId { get; set; }
        public int KullaniciId { get; set; }
        public int KonumId { get; set; }
        public string Aciklama { get; set; }
        public int FotografId { get; set; }

        [ForeignKey("AltKategoriId")]
        public Bildiri bildiri { get; set; }

        [ForeignKey("KullaniciId")]
        public Kullanici kullanici { get; set; }

        [ForeignKey("KonumId")]
        public Konum konum { get; set; }

        [ForeignKey("FotografId")]
        public BildiriFotograf fotograf { get; set; }

    }
}