using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Aktiflik : Entity
    {
        public int PasifTuru { get; set; }
        public int KisiId { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Aktif { get; set; }

        [ForeignKey("KisiId")]
        public Kisi kisi { get; set; }

        [ForeignKey("PasifTuru")]
        public SilinmeTurleri silinmeTur { get; set; }
    }
}