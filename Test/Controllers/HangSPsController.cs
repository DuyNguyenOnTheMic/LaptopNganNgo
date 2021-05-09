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
    public class HangSPsController : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        // GET: HangSPs
        public ActionResult Index()
        {
            return View(db.HangSPs.ToList());
        }

        // GET: HangSPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSP hangSP = db.HangSPs.Find(id);
            if (hangSP == null)
            {
                return HttpNotFound();
            }
            return View(hangSP);
        }

        // GET: HangSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HangSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,TenHang")] HangSP hangSP)
        {
            if (ModelState.IsValid)
            {
                db.HangSPs.Add(hangSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangSP);
        }

        // GET: HangSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSP hangSP = db.HangSPs.Find(id);
            if (hangSP == null)
            {
                return HttpNotFound();
            }
            return View(hangSP);
        }

        // POST: HangSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,TenHang")] HangSP hangSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangSP);
        }

        // GET: HangSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSP hangSP = db.HangSPs.Find(id);
            if (hangSP == null)
            {
                return HttpNotFound();
            }
            return View(hangSP);
        }

        // POST: HangSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangSP hangSP = db.HangSPs.Find(id);
            db.HangSPs.Remove(hangSP);
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
