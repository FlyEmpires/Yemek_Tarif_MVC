using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yemek_Tarif.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc.Ajax;
using System.Net;
using System.Web.Security;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using System.IO;
using System.Data.Entity;
using System.Web;

namespace Yemek_Tarif.Controllers
{

    public class SabitlerController : Controller
    {
        // GET: Sabitler
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hakkımızda()
        {
            return View();
        }
   [Route("Kategoriler")]
        //[Route("YemekKategori/{kategoriad}")]
        public ActionResult YemekKategori()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            var degerler = ent.tbl_Kategoriler.ToList();
            return View(degerler);

        }
        //[Route("KategoriDetay")]
        //[Route("Kategoriler/{KategoriAd}/{id:int}")]

        public ActionResult YemekKategoriDetay(int? id)
        {

            YemekTarifEntities ent = new YemekTarifEntities();
            tbl_Kategoriler kategori = new tbl_Kategoriler();
            var degerler = ent.tbl_Yemekler.Where(x => x.Kategoriid == id).ToList();


            return View(degerler);


        }
        //[Route("Sabitler/Yemekler/YenekDetay/{YemekAd}-{id:int}")]
        //[Route("Sabitler/tbl_Yemekler/{YemekAd}-{id:int}")]       
        //[Route("/Sabitler/YemekDetay/{yemekad}-{id:int}")]


        //[Route("Yemekler/{yemekad}-{id:int}")]
       [Route("Yemekler/{yemekad}-{id:int}")]

        public ActionResult YemekDetay(int id)
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<tbl_Yemekler> yemekler = ent.tbl_Yemekler.Where(x => x.Yemekid == id).ToList();
            return View(yemekler);
        }
        public ActionResult Silme(int id)
        {

            YemekTarifEntities ent = new YemekTarifEntities();
            var yemek = ent.tbl_Yemekler.Find(id);
            ent.tbl_Yemekler.Remove(yemek);
            ent.SaveChanges();
            return RedirectToAction("Admin", "Sabitler");
        }
        [Route("Yemekler")]
        public ActionResult Yemekler(int? sayfano, bool? YemekDurum)
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            tbl_Yemekler Yemekler = new tbl_Yemekler();
            Yemekler.YemekDurum = YemekDurum;
            YemekDurum = Yemekler.YemekDurum;
            //List<tbl_Yemekler> yemekListesi = ent.tbl_Yemekler.ToList();

            //Sayfalama - Paging
            int _sayfaNo = sayfano ?? 1;
            var Yemeklerr = ent.tbl_Yemekler.OrderBy(m => m.Yemekid).ToPagedList<tbl_Yemekler>(_sayfaNo, 8);


            return View(Yemeklerr);

            //if (Yemekler.YemekDurum==true)
            //{
            //    //List<tbl_Yemekler> yemekListesi = ent.tbl_Yemekler.Where(x=>x.YemekDurum==true).ToList();

            //    //var Yemeklerr = ent.tbl_Yemekler.OrderByDescending(m => m.Yemekid).ToPagedList<tbl_Yemekler>(_sayfaNo, 8);
            //    return View(yemekListesi);
            //}
            ////return View(yemekListesi);
            //else
            //{
            //    //var Yemeklerr = ent.tbl_Yemekler.OrderByDescending(m => m.Yemekid).ToPagedList(_sayfaNo, 8);

            //    return View(Yemekler);
            //}



        }

        //Burası Çalışmıyor
        public ActionResult ResimEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ResimEkle(tbl_Yemekler y)
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                //string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(Request.Files[0].FileName));
                Request.Files[0].SaveAs(yol);
                //string yol = "~/images/" + dosyaadi + uzanti;

                //Request.Files[0].SaveAs(Server.MapPath(yol));

                y.YemekResim = "/images/" + dosyaadi;
            }

            ent.tbl_Yemekler.Add(y);
            ent.SaveChanges();
            return View();
        }
        //Context c = new Context();
        [Route("YemekEkle")]
        public ActionResult YemekEkle()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<SelectListItem> degerler = (from i in ent.tbl_Kategoriler.ToList()
                                             select new SelectListItem { Text = i.KategoriAd, Value = i.Kategoriid.ToString() }).ToList();
            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost]
        public ActionResult YemekEkle(tbl_Yemekler y, string YemekAd, string YemekTarif, string YemekResim, string YemekMalzeme, string YemekIcerik, bool? YemekDurum, string KullanıcıAdı)
        {

            YemekTarifEntities ent = new YemekTarifEntities();

            tbl_Tarifler tarifler = new tbl_Tarifler();
            tbl_Kategoriler kategori = new tbl_Kategoriler();
            tbl_kullanicilar kullanici = new tbl_kullanicilar();
            tbl_Yemekler Yemekler = new tbl_Yemekler();
            var ktg = ent.tbl_Kategoriler.Where(m => m.Kategoriid == y.tbl_Kategoriler.Kategoriid).FirstOrDefault();
            y.tbl_Kategoriler = ktg;
            Yemekler.KullanıcıAdı = KullanıcıAdı;
            //kullanici.Adi = KullanıcıAdı;
            Yemekler.YemekIcerik = YemekIcerik;
            Yemekler.YemekAd = YemekAd;
            Yemekler.YemekTarif = YemekTarif;
            Yemekler.YemekMalzeme = YemekMalzeme;
            Yemekler.KullanıcıAdı = KullanıcıAdı;
            Yemekler.YemekResim = YemekResim;

            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                //string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(Request.Files[0].FileName));
                Request.Files[0].SaveAs(yol);
                //string yol = "~/images/" + dosyaadi + uzanti;

                //Request.Files[0].SaveAs(Server.MapPath(yol));

                y.YemekResim = "/images/" + dosyaadi;
            }
            ent.tbl_Yemekler.Add(y);
            try
            {
                ent.SaveChanges();
                ViewBag.sonuc = 1;

            }
            catch (Exception ee)
            {

                ViewBag.sonuc = 0;
                ViewBag.mesaj = ee.Message.ToString();


            }
            return RedirectToAction("YemekEkle");

        }
        [Authorize(Roles = "Admin")] // OTURUM YÖNETİMİ --> Yetkilendirmenin nerede olacağını belirtiyoruz
        public ActionResult Admin()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<tbl_Kategoriler> kat = ent.tbl_Kategoriler.ToList();
            List<tbl_Yemekler> yemekListesi = ent.tbl_Yemekler.ToList();
            return View(yemekListesi);
        }

        public ActionResult YemekGuncelle(string YemekNo)
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<SelectListItem> degerler = (from i in ent.tbl_Kategoriler.ToList()
                                             select new SelectListItem { Text = i.KategoriAd, Value = i.Kategoriid.ToString() }).ToList();
            ViewBag.dgr = degerler;
            int yNo = Convert.ToInt32(YemekNo);
            tbl_Yemekler yemekler = ent.tbl_Yemekler.Where(i => i.Yemekid == yNo).FirstOrDefault();
            // return View("YemekGuncelle","Sabitler");
            return View(yemekler);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult YemekGuncelle(HttpPostedFileBase YemekResim, tbl_Yemekler y, int? ID, string YemekAd, string YemekMalzeme, string YemekTarif, string YemekIcerik, string confirm_value, bool? YemekDurum)
        {
            YemekTarifEntities ent = new YemekTarifEntities();


            tbl_Yemekler yemekler = ent.tbl_Yemekler.Where(i => i.Yemekid == ID).FirstOrDefault();

            tbl_Kategoriler kategori = new tbl_Kategoriler();
            yemekler.YemekAd = YemekAd;
            yemekler.YemekMalzeme = YemekMalzeme;
            yemekler.YemekTarif = YemekTarif;
            yemekler.YemekIcerik = YemekIcerik;
            yemekler.YemekDurum = YemekDurum;
            //yemekler.YemekResim = YemekResim;
            var ktg = ent.tbl_Kategoriler.Where(m => m.Kategoriid == y.tbl_Kategoriler.Kategoriid).FirstOrDefault();
            yemekler.Kategoriid = ktg.Kategoriid;
            //if (Request.FilePath!=null)
            //{
            //    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
            //    string yol = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(Request.Files[0].FileName));
            //    Request.Files[0].SaveAs(yol);
            //    y.YemekResim = "/images/" + dosyaadi;

            //}
            if (ModelState.IsValid)
            {
                string oldfilePath = yemekler.YemekResim;
                if (YemekResim != null && YemekResim.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(YemekResim.FileName);
                    string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/"), fileName);
                    YemekResim.SaveAs(path);
                    yemekler.YemekResim = "/images/" + YemekResim.FileName;
                    string fullPath = Request.MapPath("~" + oldfilePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            




                ent.SaveChanges();



                return RedirectToAction("Admin");
                //return View(yemekler);

            }
            return View(yemekler);

        }
    }
}