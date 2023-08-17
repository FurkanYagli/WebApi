using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class AltKategori : Entity
    {
        public int KategoriId { get; set; }
        public string Ad { get; set; }
        [ForeignKey ("KategoriId")]
        public Kategori kategori{ get; set; }
    }
}