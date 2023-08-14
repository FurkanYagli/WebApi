using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.ToApi
{
    public class BildiriParametre
    {
        public int AltKategoriId { get; set; }
        public int KullaniciId { get; set; }
        public int KonumId { get; set; }
        public string Aciklama { get; set; }
        public int FotografId { get; set; }
    }
}