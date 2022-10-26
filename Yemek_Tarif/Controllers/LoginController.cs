using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yemek_Tarif.Models;
using System.Web.Security;
using System;
using Microsoft.AspNet.Identity;
using Yemek_Tarif.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.Owin.Security;

namespace Yemek_Tarif.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        YemekTarifEntities ent = new YemekTarifEntities();

        private UserManager<ApplicationUser> userManager;



        public LoginController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

        }
        
       
        [HttpGet]
        [Route("GirisYap")]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    return View("Error", new string[] { "Zaten Giriş Yapmıştınız..." });
            //}


            return View();
        }
        [Route("KayitOl")]
        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }

        [Route("KayitOl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KayitOl(tbl_kullanicilar model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();


                user.UserName = model.KullaniciAdi;               
                user.Adi = model.Adi;
                user.Soyadi = model.Soyadi;
                  ent.tbl_kullanicilar.Add(model);
                ent.SaveChanges();

                var result = userManager.Create(user,model.Sifre);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id,"User");
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error);
                    }
                }

               
            }
            return View(model);
        }
        //[HttpPost]
        //public ActionResult Index(string username, string Pass,int ?KullanıcıID)
        //{

        //    YemekTarifEntities ent = new YemekTarifEntities();
        //    //linq sorgusu

        //    tbl_Yemekler yemekler = new tbl_Yemekler();
        //    List<tbl_kullanicilar> kulListesi = ent.tbl_kullanicilar.Where(i => i.KullaniciAdi == username && i.Sifre == Pass).ToList();
        //    //List<tbl_kullanicilar> kulListesi = ent.AspNetUsers.Where(i => i.UserName == username && i.PasswordHash == Pass).ToList();
        //    //Lambda Notation
        //    int donenKaySay = kulListesi.Count();
        //    if (donenKaySay == 1)
        //    {
        //        //OTURUM YÖNETİMİ
        //        FormsAuthentication.SetAuthCookie(username,false);
        //        Session["username"] = username;
        //        //Session["ID"] = yemekler.KullanıcıID;

        //        //OTURUM  YÖNETİMİ

        //        return RedirectToAction("Admin", "Sabitler");
        //    }
        //    else if (donenKaySay == 0)
        //    {
        //        ViewBag.hata = "Lütfen Bilgilerinizi Kontrol Ediniz...";
        //        return View();
        //    }

        //    else return View();


        //}
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("GirisYap")]
        public ActionResult Index(LoginModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {

          
            var user = userManager.Find(model.KullaniciAdi,model.Sifre);
            if (user==null)

            {
                ModelState.AddModelError("","Yanlış Kullanıcı Adı veya Parola");
            }
            else
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = userManager.CreateIdentity(user, "ApplicationCookie");

                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent=true

                };

                authManager.SignOut();
                authManager.SignIn(authProperties,identity);
                    return Redirect(string.IsNullOrEmpty(returnUrl)?"/":returnUrl);
            }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
           

        public ActionResult LogOut()
            {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index");
            //FormsAuthentication.SignOut();
            //return RedirectToAction("Index","Login");
        }


    }
}