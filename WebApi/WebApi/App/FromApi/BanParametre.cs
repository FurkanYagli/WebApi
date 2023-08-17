using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.App.FromApi
{
    public class BanParametre
    {
        public string BanSebebi { get; set; }
        public int KisiId { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Aktif { get; set; }
    }
}