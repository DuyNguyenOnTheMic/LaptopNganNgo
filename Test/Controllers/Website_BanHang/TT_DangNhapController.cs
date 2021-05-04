using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Controllers
{
    public class TT_DangNhapController : Controller
    {
        // GET: TT_DangNhap
        public ActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "TT_DiaChi");
            }
            else
                return View();
        }
    }
}