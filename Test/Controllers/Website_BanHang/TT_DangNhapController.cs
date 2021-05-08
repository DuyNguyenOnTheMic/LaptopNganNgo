using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Models;

namespace Test.Controllers
{
    public class TT_DangNhapController : Controller
    {
        CT25Team24Entities db;
        public TT_DangNhapController()
        {
            db = new CT25Team24Entities();
        }

        // GET: TT_DangNhap
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "TT_DiaChi");
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
                    return RedirectToAction("Index", "TT_DiaChi");
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