using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Kisi:Entity
    {
        public string Tc { get; set; }
        public string Tel { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Mail { get; set; }


    }

    /*public class Kullanici
    {
        [Key]
        public int Id { get; set; }
        public bool Aktif { get; set; }
        public int KisiId { get; set; }

        [ForeignKey("KisiId")]
        public Kisi KisiKim { get; set; }

    }*/
}