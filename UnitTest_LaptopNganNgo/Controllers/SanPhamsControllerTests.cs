using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using Test.Models;
using System.Linq;

namespace Test.Controllers.Website_QuanTri.Tests
{
    [TestClass()]
    public class SanPhamsControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new SanPhamsController();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(model);

            var db = new CT25Team24Entities();
            Assert.AreEqual(db.SanPhams.Count(), model.Count);

        }
        [TestMethod()]
        public void DetailTest()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Details(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team24Entities();
            var product = db.SanPhams.First();
            var result1 = controller.Details(product.MaSP) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreNotEqual(product.TenSP, model.TenSP);
            Assert.AreNotEqual(product.DonGiaGoc, model.DonGiaGoc);
            Assert.AreNotEqual(product.TenSP, model.DonGiaKM);
            Assert.AreNotEqual(product.HangSP, model.HangSP);
            Assert.AreNotEqual(product.DongSP, model.DongSP);
            Assert.AreNotEqual(product.ThongTinChiTietSP, model.ThongTinChiTietSP);
            Assert.AreNotEqual(product.TrangThaiSP, model.TrangThaiSP);
        }
    }
}