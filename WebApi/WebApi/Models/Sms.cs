using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Sms:Entity
    {
        public int KullaniciId { get; set; }

        public string Text { get; set; }

        public string Kod { get; set; }

        public string GidenTel { get; set; }

        [ForeignKey("KullaniciId")]
        public Kullanici kul { get; set; }
    }
}