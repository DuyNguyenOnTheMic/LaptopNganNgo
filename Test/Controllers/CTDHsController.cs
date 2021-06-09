﻿using System;
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

        public void GetCTDHsController()
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
            if ((Session["ShoppingCart"] as List<CTDH>)?.Count == null || (Session["ShoppingCart"] as List<CTDH>)?.Count == 0)
            {
                return RedirectToAction("GioHang_Empty");
            }
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
                DonGia = dongia
            });

            return RedirectToAction("Index");
        }


        // GET: CTDHs/Edit/5
        [HttpPost]
        public ActionResult Edit(int[] id, int[] sl, double[] dongia)
        {
            GetCTDHsController();
            //var session = System.Web.HttpContext.Current.Session;
            ShoppingCart.Clear();
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
            Session["ShoppingCart"] = ShoppingCart;

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int masp)
        {
            GetCTDHsController();
            //var session = System.Web.HttpContext.Current.Session;
            ShoppingCart.RemoveAll(x => x.SanPham.MaSP == masp);
            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        }

        // GET: CTDHs1/Delete/5
        public ActionResult DeleteOrderDetails(int madh, int masp, int count)
        {
            GetCTDHsController();
            CTDH cTDH = db.CTDHs.Find(madh, masp);
            if (cTDH == null)
            {
                return HttpNotFound();
            }
            if (count == 1)
            {
                DonHang donhang = db.DonHangs.Find(madh);
                donhang.TrangThai = 4;
                donhang.TongTien = 0;
                db.Entry(donhang).State = EntityState.Modified;
                db.CTDHs.Remove(cTDH);
                db.SaveChanges();
                return RedirectToAction("Edit", "DonHangs", new { id = madh.ToString() });
            }
            db.CTDHs.Remove(cTDH);
            db.SaveChanges();
            return RedirectToAction("Edit", "DonHangs", new { id = madh.ToString() });
        }


        public ActionResult GioHang_Empty()
        {
            return View();
        }
    }
}