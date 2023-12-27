using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStock.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MVCStock.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DBMVCStockEntities db= new DBMVCStockEntities();
        [Authorize]
        public ActionResult Index(int sayfa= 1)
        {
            // 1.sayfadan başlanarak ilk 3 değer şeklinde sayfalama işlemi.
            var musteriliste = db.tblmusteri.Where(x=>x.durum==true).ToList().ToPagedList(sayfa, 3); 
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(tblmusteri p)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum= true;
            db.tblmusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(tblmusteri p)
        {

                var mstrbul = db.tblmusteri.Find(p.id);
                mstrbul.durum = false;
                db.SaveChanges();
                return RedirectToAction("Index");

        }
        public ActionResult MusteriGetir(int id)
        {
            var mstr = db.tblmusteri.Find(id);
            return View("MusteriGetir", mstr);
        }
        public ActionResult MusteriGuncelle(tblmusteri m) 
        {
            var mus = db.tblmusteri.Find(m.id);
            mus.ad = m.ad;
            mus.soyad=m.soyad;
            mus.sehir= m.sehir;
            mus.bakiye=m.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}