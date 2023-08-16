using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Gikom : Entity
    {
        public int BildiriId { get; set; }

        [ForeignKey("BildiriId")]
        public Bildiri bilId { get; set; }
    }
}