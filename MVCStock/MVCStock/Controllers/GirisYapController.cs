using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStock.Models.Entity;
using System.Web.Security;

namespace MVCStock.Controllers
{
    public class GirisYapController : Controller
    {
        DBMVCStockEntities db= new DBMVCStockEntities();
        // GET: GirisYap
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(tbladmin a)
        {
            var bilgiler = db.tbladmin.FirstOrDefault(x => x.kullanici == a.kullanici && x.sifre == a.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();

            }
        }
    }
}