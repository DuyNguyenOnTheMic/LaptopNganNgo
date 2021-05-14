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
    public class CTDHs1Controller : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        // GET: CTDHs1
        public ActionResult Index()
        {
            var cTDHs = db.CTDHs.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(cTDHs.ToList());
        }

        // GET: CTDHs1/Details/5
        public ActionResult Details(int? id)
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

        // GET: CTDHs1/Create
        public ActionResult Create()
        {
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: CTDHs1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaSP,SL,DonGia,ChietKhau,ThanhTien")] CTDH cTDH)
        {
            if (ModelState.IsValid)
            {
                db.CTDHs.Add(cTDH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH", cTDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTDH.MaSP);
            return View(cTDH);
        }

        // GET: CTDHs1/Edit/5
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

        // POST: CTDHs1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaSP,SL,DonGia,ChietKhau,ThanhTien")] CTDH cTDH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTDH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH", cTDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTDH.MaSP);
            return View(cTDH);
        }

        // GET: CTDHs1/Delete/5
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

        // POST: CTDHs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTDH cTDH = db.CTDHs.Find(id);
            db.CTDHs.Remove(cTDH);
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
