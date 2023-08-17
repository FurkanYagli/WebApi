using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.App
{
    public class Return
    {
        public bool success { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
}