using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Duyuru : Entity
    {
        public string Baslik { get; set; }
        public string Text { get; set; }
        public int FotografId { get; set; }
        [ForeignKey("FotografId")]
        public DuyuruFotograf fotograf { get; set; }
    }
}