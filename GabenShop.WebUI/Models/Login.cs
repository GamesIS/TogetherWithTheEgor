using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GabenShop.WebUI.Models
{
    public class Login
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}