using System;
using System.Collections.Generic;
using System.Linq;
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
        // GET: HoaDon
        public ActionResult Index()
        {
            var model = db.DonHangs.ToList();
            return View(model);
        }

        public ActionResult TT_ThanhCong( )
        {
            
            return View();
        }
        public ActionResult Create()
        {
            GetShoppingCart();
            ViewBag.Cart = ShoppingCart; 

            return View();
        }
        [HttpPost]
        
        public ActionResult Create1 (int maKH, double Tongtien )
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
                    }) ;
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
    }
}