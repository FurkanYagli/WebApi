﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Sms : Entity
    {
        public int SmsTur { get; set; }
        public int KisiId { get; set; }

        public string Text { get; set; }

        public string Kod { get; set; }

        public string GidenTel { get; set; }

        public DateTime GecerlilikTarih { get; set; }

        [ForeignKey("KisiId")]
        public Kisi IdKisi { get; set; }
    }
}