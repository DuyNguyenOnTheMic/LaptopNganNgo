using System;
using System.Collections;
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
    public class CTDHsController : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        private List<CTDH> ShoppingCart = null;

        private void GetCTDHsController()
        {
            if (Session["ShoppingCart"] != null)
            {
                ShoppingCart = Session["ShoppingCart"] as List<CTDH>;
            }
            else
            {
                ShoppingCart = new List<CTDH>();
                Session["ShoppingCart"] = ShoppingCart;
            }
        }

        // GET: CTDHs
        public ActionResult Index()
        {
            GetCTDHsController();
            var hashtable = new Hashtable();
            foreach (var item in ShoppingCart)
            {
                if (hashtable[item.SanPham.MaSP] != null)
                {
                    (hashtable[item.SanPham.MaSP] as CTDH).SL += item.SL;
                }
                else
                {
                    hashtable[item.SanPham.MaSP] = item;
                }
            }
            ShoppingCart.Clear();
            foreach (CTDH cthd in hashtable.Values)
            {
                ShoppingCart.Add(cthd);
            }
            return View(ShoppingCart);
        }


        // GET: CTDHs/Create

        [HttpPost]
        public ActionResult Create(int id, int sl, int dongia)
        {
            GetCTDHsController();
            var ID = db.SanPhams.Find(id);
            ShoppingCart.Add(new CTDH
            {
                SanPham = ID,
                SL = sl,
                DonGia = dongia,


            });

            return RedirectToAction("Index");
        }


        // GET: CTDHs/Edit/5
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



        // GET: CTDHs/Delete/5
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

        // POST: CTDHs/Delete/5
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
