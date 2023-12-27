using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStock.Models.Entity;

namespace MVCStock.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DBMVCStockEntities db = new DBMVCStockEntities();
        
        public ActionResult Index(string p)
        {
            //var urunler = db.tblurunler.Where(x=>x.durum==true).ToList();
            var urunler = db.tblurunler.Where(x => x.durum == true);
            if(!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum==true) ;
            }   
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString(),
                                        }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurunler t)
        {
            var ktgr=db.tblkategori.Where(x=>x.id ==t.tblkategori.id).FirstOrDefault();
            t.tblkategori = ktgr;
            db.tblurunler.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.tblkategori.ToList() select new SelectListItem
                                            {
                                                Text = x.ad,
                                                Value = x.id.ToString()
                                            }).ToList();
            var ktgr = db.tblurunler.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", ktgr);
        }
        public ActionResult UrunGuncelle(tblurunler i)
        {
            var urn = db.tblurunler.Find(i.id);
            urn.ad = i.ad;
            urn.marka=i.marka;
            urn.stok = i.stok;
            urn.alisfiyat= i.alisfiyat;
            urn.satisfiyat= i.satisfiyat;
            var ktg = db.tblkategori.Where(x => x.id == i.tblkategori.id).FirstOrDefault();
            urn.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(tblurunler i)
        {
            var urunbul = db.tblurunler.Find(i.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");   
        }

    }
}