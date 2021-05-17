using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Controllers.Website_QuanTri
{
    public class QT_XemCTDHController : Controller
    {
        // GET: QT_XemCTDH
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            return View();
        }
    }
}