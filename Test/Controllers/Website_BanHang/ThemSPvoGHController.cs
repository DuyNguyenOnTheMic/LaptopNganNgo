using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers.Website_BanHang
{
    public class ThemSPvoGHController : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        private List<CTDH> Shoppinglaptop = null;

        public ThemSPvoGHController()
        {
            var session = System.Web.HttpContext.Current.Session;
            if (Session["ShoppingLaptop"] != null)
                Shoppinglaptop = session["ShoppingLaptop"] as List<CTDH>;
            else
            {
                Shoppinglaptop = new List<CTDH>();
                session["ShoppingLaptop"] = Shoppinglaptop;
            }
        }
        // GET: ThemSPvoGH
        public ActionResult Index()
        {
            return View(Shoppinglaptop);
       
        }       
        // GET: ThemSPvoGH/Create
        [HttpPost]
        public ActionResult Create(int MaSP, int sl)
        {
            var sanpham = db.SanPhams.Find(MaSP);
            Shoppinglaptop.Add(new CTDH
            {
                SanPham = sanpham,
                SL = sl
            }) ; 
            return RedirectToAction("Index");
        }

        // GET: ThemSPvoGH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDH cTDH = db.CTDHs.Find(id);
            if (cTDH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH", cTDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTDH.MaSP);
            return View(cTDH);
        }

        // GET: ThemSPvoGH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDH cTDH = db.CTDHs.Find(id);
            if (cTDH == null)
            {
                return HttpNotFound();
            }
            return View(cTDH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
