using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStock.Models.Entity;

namespace MVCStock.Controllers
{
    public class AdminController : Controller
    {
        DBMVCStockEntities db= new DBMVCStockEntities();
       
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(tbladmin a)
        {
            db.tbladmin.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}