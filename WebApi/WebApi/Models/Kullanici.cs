﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApi;

namespace WebApi.Models
{
    public class Kullanici:Entity
    {
        public bool Aktif { get; set; }
        public int KisiId { get; set; }
        [ForeignKey("KisiId")]
        public Kisi kisip { get; set; }

    }
}