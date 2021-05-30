using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using System.Web.Mvc;

namespace Test.Controllers.Website_QuanTri.Tests
{
    [TestClass()]
    public class SanPhamsControllerTests
    {
        [TestMethod()]
        public void Details_SP_Test()
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
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.DonGiaKM, model.DonGiaKM);
            Assert.AreEqual(product.DonGiaGoc, model.DonGiaGoc);
            Assert.AreEqual(product.HinhAnhSP, model.HinhAnhSP);
            Assert.AreEqual(product.SL, model.SL);
            Assert.AreEqual(product.DongSP, model.DongSP);
            Assert.AreEqual(product.TrangThaiSP, model.TrangThaiSP);
            Assert.AreEqual(product.ThongTinChiTietSP, model.ThongTinChiTietSP);

        }

        [TestMethod()]
        public void Test_Post_Delete_SanPham()
        {
            var db = new CT25Team24Entities();
            var product = db.SanPhams.AsNoTracking().First();
        }

        [TestMethod()]
        public void Test_Get_Dispose_SanPham()
        {
            using (var controller = new SanPhamsController()) { }
        }

    }
}