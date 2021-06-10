using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace Test.Controllers.Website_BanHang
{
    public class DangNhapController : Controller
    {
        CT25Team24Entities db;
        public DangNhapController()
        {
            db = new CT25Team24Entities();
        }
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Models.KhachHang model)
        {
            using (var context = new CT25Team24Entities())
            {
                var f_password = GetMD5(model.MatKhau);
                var account = context.KhachHangs.Where(acc => acc.Email.Equals(model.Email) && acc.MatKhau.Equals(f_password)).FirstOrDefault();
                bool isValid = context.KhachHangs.Any(x => x.Email.Equals(model.Email)
                && x.MatKhau.Equals(f_password));
                if (isValid)
                {
                    Session["HoTen"] = account.HoTen;
                    Session["Email"] = account.Email;
                    Session["MaKH"] = account.MaKH;
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "SanPhams");
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
            return RedirectToAction("Index", "SanPhams");
        }
    }
}