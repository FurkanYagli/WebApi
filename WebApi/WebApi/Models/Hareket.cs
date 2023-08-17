using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Hareket:Entity
    {
        public int KullaniciId { get; set; }
        public int? SayfaId{ get; set; }
        public int? IslemId { get; set; }

        [ForeignKey("KullaniciId")]
        public Kullanici kullanici{ get; set; }

        [ForeignKey("SayfaId")]
        public Sayfa sayfa { get; set; }

        [ForeignKey("IslemId")]
        public Islem islem { get; set; }
    }
}