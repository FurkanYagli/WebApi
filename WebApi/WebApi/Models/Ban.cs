using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Ban : Entity
    {
        public string BanSebebi { get; set; }
        public int KisiId { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Aktif { get; set; }

        [ForeignKey("KisiId")]
        public Kisi kisi { get; set; }
    }
}