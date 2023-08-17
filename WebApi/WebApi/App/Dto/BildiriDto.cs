using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Dto
{
    public class BildiriDto
    {
        public int AltKategoriId { get; set; }
        public int KullaniciId { get; set; }
        public int KonumId { get; set; }
        public string Aciklama { get; set; }
        public int FotografId { get; set; }
    }
}