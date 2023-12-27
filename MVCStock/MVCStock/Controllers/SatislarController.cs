using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStock.Models.Entity;

namespace MVCStock.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DBMVCStockEntities db=new DBMVCStockEntities();
        public ActionResult Index()
        {
            var satislar=db.tblsatislar.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            // Ürünler
            List<SelectListItem> urun = (from x in db.tblurunler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString(),
                                        }).ToList();
            ViewBag.drop1 = urun;
            //Personel
            List<SelectListItem> per = (from x in db.tblpersonel.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad + " " + x.soyad,
                                             Value = x.id.ToString(),
                                         }).ToList();
            ViewBag.drop2 = per;
            //Müşteriler 
            List<SelectListItem> must = (from x in db.tblmusteri.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString(),
                                        }).ToList();
            ViewBag.drop3 = must;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(tblsatislar t)
        {
            var urun = db.tblurunler.Where(x => x.id == t.tblurunler.id).FirstOrDefault();
            var musteri = db.tblmusteri.Where(x => x.id == t.tblmusteri.id).FirstOrDefault();
            var personel = db.tblpersonel.Where(x => x.id == t.tblpersonel.id).FirstOrDefault();

            t.tblurunler = urun;
            t.tblmusteri = musteri;
            t.tblpersonel = personel;
            t.tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            db.tblsatislar.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}