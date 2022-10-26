using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yemek_Tarif.Models;

namespace Yemek_Tarif.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        [Route("")]

        [Route("Anasayfa")]
        public ActionResult Index()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<tbl_Yemekler> yemekListesi = ent.tbl_Yemekler.ToList();
            return View(yemekListesi);

        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult deneme()
        {
            return View();
        }
        public ActionResult Slider()
        {
            return View();
        }

    }
}