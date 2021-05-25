using PagedList;
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
    public class DonHangsController : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        private List<CTDH> ShoppingCart = null;
        public void GetShoppingCart()
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

        public ActionResult TT_ThanhCong(DonHang model)
        {

            return View();
        }

        // GET: DonHangs

        public ActionResult Index(int? keyword, int? page, int? category)
        {
            db = new CT25Team24Entities();

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            if (category != null)
            {
                ViewBag.category = category;
                return View(db.DonHangs.Where(x => x.MaDH.ToString().Contains(keyword.ToString()) || keyword == null)
                .Where(x => x.TrangThai == category).ToList().OrderByDescending(x => x.MaDH).ToPagedList(page ?? 1, 6));
            }
            else
            {
                return View(db.DonHangs.Where(x => x.MaDH.ToString().Contains(keyword.ToString()) || keyword == null)
                .ToList().OrderByDescending(x => x.MaDH).ToPagedList(page ?? 1, 6));
            }
        }

        // GET: DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: DonHangs/Create
        public ActionResult Create()
        {
            GetShoppingCart();
            ViewBag.Cart = ShoppingCart;
            return View();
        }


        [HttpPost]
        public ActionResult Create1(int maKH, double Tongtien)
        {
            var session = System.Web.HttpContext.Current.Session;
            GetShoppingCart();
            if (ModelState.IsValid)
            {
                var model = new Test.Models.DonHang()
                {
                    NgayBan = DateTime.Now,
                    MaKH = maKH,
                    TongTien = Tongtien,
                    TrangThai = 1,
                };
                db.DonHangs.Add(model);
                db.SaveChanges();

                foreach (var item in ShoppingCart)
                {
                    db.CTDHs.Add(new CTDH
                    {
                        MaDH = model.MaDH,
                        MaSP = item.SanPham.MaSP,
                        DonGia = item.DonGia,
                        SL = item.SL,
                        ThanhTien = item.DonGia * item.SL
                    });
                }
                db.SaveChanges();
                session["ShoppingCart"] = null;

                return RedirectToAction("TT_ThanhCong", "DonHangs");
            }
            ViewBag.Cart = ShoppingCart;
            return View();
        }
        private void ValidataDonHang(DonHang model)
        {
            GetShoppingCart();
            if (ShoppingCart.Count == 0)
            {
                ModelState.AddModelError("", "Không có sản phẩm trong giỏ hàng!!!");
            }

        }   

        // GET: DonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donHang.MaKH);
            ViewBag.TrangThai = new SelectList(db.TrangThaiDHs, "MaTrangThai", "TrangThai", donHang.TrangThai);
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,NgayBan,MaKH,TongTien,TrangThai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "DonHangs", new { id = donHang.MaDH });
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donHang.MaKH);
            ViewBag.TrangThai = new SelectList(db.TrangThaiDHs, "MaTrangThai", "TrangThai", donHang.TrangThai);
            return View(donHang);
        }


        // GET: DonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
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
