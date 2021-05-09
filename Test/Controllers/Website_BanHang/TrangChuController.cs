using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class TrangChuController : Controller
    {
        CT25Team24Entities db = new CT25Team24Entities();
        
        // GET: TrangChu
        public ActionResult Index2()
        {
            var model = db.SanPhams.ToList();
            return View(model);
        }
    }
}