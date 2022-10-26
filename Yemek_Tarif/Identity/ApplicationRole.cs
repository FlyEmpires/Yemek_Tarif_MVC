using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemek_Tarif.Identity
{
    public class ApplicationRole:IdentityRole
    {
        public string Aciklama { get; set; }
    }
}