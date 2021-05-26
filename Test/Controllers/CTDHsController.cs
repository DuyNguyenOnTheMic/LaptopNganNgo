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

        public CTDHsController()
        {
            var session = System.Web.HttpContext.Current.Session;
            if (session["ShoppingCart"] != null)
            {
                ShoppingCart = session["ShoppingCart"] as List<CTDH>;
            }
            else
            {
                ShoppingCart = new List<CTDH>();
                session["ShoppingCart"] = ShoppingCart;
            }
        }

        // GET: CTDHs
        public ActionResult Index()
        {       
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
        [HttpPost]
        public ActionResult Edit(int[] id, int[] sl, double[] dongia)
        {
            var session = System.Web.HttpContext.Current.Session;
            ShoppingCart.Clear();
            if (id != null)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    var SP = db.SanPhams.Find(id[i]);
                    ShoppingCart.Add(new CTDH
                    {
                        SanPham = SP,
                        SL = sl[i],
                        DonGia = dongia[i]
                    });
                }
            }
            session["ShoppingCart"] = ShoppingCart;

            return RedirectToAction("Index");
        }

        

        public ActionResult Delete(int id)
        {
            var session = System.Web.HttpContext.Current.Session;
            ShoppingCart.RemoveAll(x => x.SanPham.MaSP == id);
            session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        
        }


        // GET: CTDHs1/Delete/5
        public ActionResult DeleteOrderDetails(int? id, int count)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDH cTDH = db.CTDHs.FirstOrDefault(m => m.MaDH == id);
            if (cTDH == null)
            {
                return HttpNotFound();
            }
            if (count == 1)
            {
                DonHang donhang = db.DonHangs.Find(id);
                donhang.TrangThai = 4;
                db.Entry(donhang).State = EntityState.Modified;
                db.CTDHs.Remove(cTDH);
                db.SaveChanges();
                return RedirectToAction("Edit", "DonHangs", new { id = id.ToString() });
            }           
            db.CTDHs.Remove(cTDH);
            db.SaveChanges();
            return RedirectToAction("Edit", "DonHangs", new { id = id.ToString() });
        }


        //[HttpPost, ActionName("DeleteOrderDetails")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
            
        //}
    }
}
