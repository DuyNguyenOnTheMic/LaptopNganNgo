using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using System.Web.Security;

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
                var account = context.KhachHangs.Where(acc => acc.Email == model.Email && acc.MatKhau == model.MatKhau).FirstOrDefault();
                bool isValid = context.KhachHangs.Any(x => x.Email == model.Email
                && x.MatKhau == model.MatKhau);
                if (isValid)
                {
                    Session["HoTen"] = account.HoTen;
                    Session["Email"] = account.Email;
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "TrangChu");
                }
            }
            ModelState.AddModelError("", "Invalid email and password!!");
            Session["Message"] = "Sai Email hoặc mật khẩu!!";
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "TrangChu");
        }
    }
}