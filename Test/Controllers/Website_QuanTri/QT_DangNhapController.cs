using System;
using System.Collections.Generic;
using System.Linq;
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
                var account = context.KhachHangs.Where(acc => acc.Email == model.Email 
                                && acc.MatKhau == model.MatKhau ).FirstOrDefault();
                bool isValid = context.KhachHangs.Any(x => x.Email == model.Email
                                && x.MatKhau == model.MatKhau );
                if (isValid)
                {
                    Session["HoTen"] = account.HoTen;
                    Session["Email"] = account.Email;
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "QT_TrangChu");
                }
            }
            ModelState.AddModelError("", "Invalid email and password!!");
            Session["Message"] = "Sai Email hoặc mật khẩu!!";
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "QT_DangNhap");
        }

    }
}