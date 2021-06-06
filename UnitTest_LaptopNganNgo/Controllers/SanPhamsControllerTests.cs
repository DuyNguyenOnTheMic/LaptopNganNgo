using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers.Website_QuanTri.Tests
{
    [TestClass()]
    public class SanPhamsControllerTests
    {

        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            var controller = new SanPhamsController();
            var db = new CT25Team24Entities();

            //Act
            var result = controller.Index(1) as ViewResult;
            var model = result.Model as List<SanPham>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(db.SanPhams.Count(), model.Count);
        }

        [TestMethod()]
        public void Test_Get_Dispose_SanPham()
        {
            using (var controller = new SanPhamsController()) { }
        }

        [TestMethod()]
        public void Test_Get_Create_SanPham()
        {
            var controller = new SanPhamsController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Test_Post_Create_SanPham()
        {
            var random = new Random();
            var sanpham = new SanPham
            {
                TenSP = random.NextDouble().ToString(),
                DonGiaGoc = random.Next(),
                DonGiaKM = random.Next(),

                DongSP = random.NextDouble().ToString(),
                SL = random.Next(),
                TrangThaiSP = random.NextDouble().ToString(),
                ThongTinChiTietSP = random.NextDouble().ToString(),

            };
            var controller = new SanPhamsController();
            var result0 = controller.Create() as ViewResult;
            Assert.IsNotNull(result0);
        }

        [TestMethod()]
        public void Test_Get_Edit_SanPham()
        {

            var db = new CT25Team24Entities();
            var controller = new SanPhamsController();
            var product = db.SanPhams.First();
            var result = controller.Edit(product.MaSP) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SanPham;
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
        public void Test_Post_Edit_SanPham()
        {
            {
                var random = new Random();
                var db = new CT25Team24Entities();
                var product = db.SanPhams.First();
                product.TenSP = random.NextDouble().ToString();
                product.DonGiaGoc = -random.Next();
                product.DonGiaKM = -random.Next();

                product.DongSP = random.NextDouble().ToString();
                product.SL = -random.Next();
                product.TrangThaiSP = random.NextDouble().ToString();
                product.ThongTinChiTietSP = random.NextDouble().ToString();

                var controller = new SanPhamsController();
                var result0 = controller.Edit(product.MaSP) as ViewResult;
                Assert.IsNotNull(result0);
            }
        }

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
        public void Test_Get_Delete_SanPham()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Delete(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team24Entities();
            var product = db.SanPhams.First();
            var result1 = controller.Delete(product.MaSP) as ViewResult;
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
        public void SearchTest()
        {
            var db = new CT25Team24Entities();
            var sanphams = db.SanPhams.ToList();
            var keyword = sanphams.First().TenSP.Split().First();
            sanphams = sanphams.Where(p => p.TenSP.ToLower().Contains(keyword.ToLower())).ToList();
            var controller = new SanPhamsController();
            var result = controller.Search() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.AreEqual("Dell", sanphams);

        }

    }
}