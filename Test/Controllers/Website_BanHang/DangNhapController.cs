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
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Models.Membership model)
        {
            using (var context = new CT25Team24Entities())
            {
                bool isValid = context.KhachHangs.Any(x => x.TenDangNhap == model.TenDangNhap
                && x.MatKhau == model.MatKhau);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.TenDangNhap, false);
                    return RedirectToAction("Index", "TrangChu");
                }
            }
            ModelState.AddModelError("", "Invalid username and password!!");
            return View();
        }
    }
}