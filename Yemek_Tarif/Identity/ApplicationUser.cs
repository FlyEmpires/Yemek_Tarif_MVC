using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemek_Tarif.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
   
          
    }
}