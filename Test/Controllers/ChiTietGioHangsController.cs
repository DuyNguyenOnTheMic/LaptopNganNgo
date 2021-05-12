using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class ChiTietGioHangsController : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        // GET: ChiTietGioHangs
        public ActionResult Index()
        {
            var chiTietGioHangs = db.ChiTietGioHangs.Include(c => c.GioHang).Include(c => c.SanPham);
            return View(chiTietGioHangs.ToList());
        }

        // GET: ChiTietGioHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaGH = new SelectList(db.GioHangs, "MaGH", "MaGH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietGioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGH,MaSP,SL,DonGia,ThanhTien")] ChiTietGioHang chiTietGioHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietGioHangs.Add(chiTietGioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGH = new SelectList(db.GioHangs, "MaGH", "MaGH", chiTietGioHang.MaGH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietGioHang.MaSP);
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGH = new SelectList(db.GioHangs, "MaGH", "MaGH", chiTietGioHang.MaGH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietGioHang.MaSP);
            return View(chiTietGioHang);
        }

        // POST: ChiTietGioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGH,MaSP,SL,DonGia,ThanhTien")] ChiTietGioHang chiTietGioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietGioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGH = new SelectList(db.GioHangs, "MaGH", "MaGH", chiTietGioHang.MaGH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietGioHang.MaSP);
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietGioHang);
        }

        // POST: ChiTietGioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            db.ChiTietGioHangs.Remove(chiTietGioHang);
            db.SaveChanges();
            return RedirectToAction("Index");
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
