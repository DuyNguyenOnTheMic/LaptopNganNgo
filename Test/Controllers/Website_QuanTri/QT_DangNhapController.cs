using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Models;

namespace Test.Controllers.Website_QuanTri
{
    public class QT_DangNhapController : Controller
    {
        CT25Team24Entities db;
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }
        public QT_DangNhapController()
        {
            db = new CT25Team24Entities();
        }
        [HttpPost]
        public ActionResult Index(Models.KhachHang model)
        {
            using (var context = new CT25Team24Entities())
            {
                var f_password = GetMD5(model.MatKhau);
                var account = context.KhachHangs.Where(acc => acc.Email == model.Email 
                                && acc.MatKhau.Equals(f_password)).FirstOrDefault();
                bool isValid = context.KhachHangs.Any(x => x.Email == model.Email
                                && x.MatKhau.Equals(f_password) && x.VaiTro == "Admin");
                if (isValid)
                {
                    Session["HoTen"] = account.HoTen;
                    Session["Email"] = account.Email;
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("QT_SanPham", "SanPhams");
                }
            }
            ModelState.AddModelError("", "Invalid email and password!!");
            Session["Message"] = "Sai Email hoặc mật khẩu!!";
            return View();
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

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "QT_DangNhap");
        }

    }
}