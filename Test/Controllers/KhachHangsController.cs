using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers.Website_QuanTri
{
    public class KhachHangsController : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        // GET: KhachHangs
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        public ActionResult TaiKhoan()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            return View(db.KhachHangs.Where(x=>x.VaiTro == "Admin").ToList());
        }


        // GET: KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public ActionResult Dky_QT()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Redirect("~/QT_DangNhap/Index");
            //}
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dky_QT([Bind(Include = "MaKH,HoTen,GioiTinh,DienThoai,DiaChi,NgaySinh,Email,MatKhau,VaiTro,XacNhanMK")] KhachHang khachHang)
        {
            ModelState.Remove("MaKH");
            ModelState.Remove("HoTen");
            ModelState.Remove("GioiTinh");
            ModelState.Remove("DienThoai");
            ModelState.Remove("DiaChi");
            ModelState.Remove("NgaySinh");

            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.Email == khachHang.Email);
                if (check == null)
                {
                    khachHang.MatKhau = GetMD5(khachHang.MatKhau);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("TaiKhoan");
                }
                else
                {
                    ViewBag.error = "Địa chỉ email này đã được đăng ký rồi";
                    return View();
                }
            }

            return View(khachHang);
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }



        // GET: KhachHangs/Create
        public ActionResult Dky_KH()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dky_KH([Bind(Include = "MaKH,HoTen,GioiTinh,DienThoai,DiaChi,NgaySinh,Email,MatKhau,VaiTro,XacNhanMK")] KhachHang khachHang)
        {
            ModelState.Remove("MaKH");
            ModelState.Remove("HoTen");
            ModelState.Remove("GioiTinh");
            ModelState.Remove("DienThoai");
            ModelState.Remove("DiaChi");
            ModelState.Remove("NgaySinh");

            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.Email == khachHang.Email);
                if (check == null)
                {
                    khachHang.MatKhau = GetMD5(khachHang.MatKhau);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "DangNhap");
                }
                else
                {
                    ViewBag.error = "Địa chỉ email này đã được đăng ký rồi";
                    return View();
                }

            }
            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public ActionResult Dky_TT_KH()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dky_TT_KH([Bind(Include = "MaKH,HoTen,GioiTinh,DienThoai,DiaChi,NgaySinh,Email,MatKhau,VaiTro,XacNhanMK")] KhachHang khachHang)
        {
            ModelState.Remove("MaKH");
            ModelState.Remove("HoTen");
            ModelState.Remove("GioiTinh");
            ModelState.Remove("DienThoai");
            ModelState.Remove("DiaChi");
            ModelState.Remove("NgaySinh");

            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.Email == khachHang.Email);
                if (check == null)
                {
                    khachHang.MatKhau = GetMD5(khachHang.MatKhau);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "TT_DangNhap");
                }
                else
                {
                    ViewBag.error = "Địa chỉ email này đã được đăng ký rồi";
                    return View();
                }
            }

            return View(khachHang);
        }


        // GET: KhachHangs/Edit/5
        public ActionResult CapNhat_TT_KH(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhat_TT_KH([Bind(Include = "MaKH,HoTen,GioiTinh,DienThoai,DiaChi,NgaySinh,Email,MatKhau,VaiTro,XacNhanMK")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create", "DonHangs");
            }
            return View(khachHang);
        }

        

        // GET: KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
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
