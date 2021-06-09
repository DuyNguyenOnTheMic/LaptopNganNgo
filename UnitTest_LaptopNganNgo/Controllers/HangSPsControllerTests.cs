using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;
using System.Net;
using Moq;
using Test.Controllers;



namespace UnitTest_LaptopNganNgo.Controllers
{



    [TestClass()]
    public class HangSPsControllerTest
    {



        [TestMethod()]
        public void IndexTest()
        {
            var controller = new HangSPsController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);



            var model = result.Model as List<HangSP>;
            Assert.IsNotNull(model);



            var db = new CT25Team24Entities();
            Assert.AreEqual(db.HangSPs.Count(), model.Count);



        }




        [TestMethod()]
        public void TestCreateG()
        {
            var controller = new HangSPsController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void TestCreateP()
        {
            var random = new Random();
            var hangSP = new HangSP
            {
                MaHang = random.Next(),



                TenHang = random.NextDouble().ToString(),





            };
            var controller = new HangSPsController();
            var result0 = controller.Create() as ViewResult;
            Assert.IsNotNull(result0);






        }
        [TestMethod()]
        public void TestEditG()
        {



            var db = new CT25Team24Entities();
            var controller = new HangSPsController();
            var product = db.HangSPs.First();
            var result = controller.Edit(product.MaHang) as ViewResult;
            Assert.IsNotNull(result);



            var model = result.Model as HangSP;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaHang, model.MaHang);
            Assert.AreEqual(product.TenHang, model.TenHang);




        }
        [TestMethod()]
        public void TestDeleteG()
        {

            var controller = new HangSPsController();
            var result0 = controller.Delete(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);



            var db = new CT25Team24Entities();
            var product = db.HangSPs.First();
            var result1 = controller.Edit(product.MaHang) as ViewResult;
            Assert.IsNotNull(result1);



            var model = result1.Model as HangSP;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaHang, model.MaHang);
            Assert.AreEqual(product.TenHang, model.TenHang);






        }
        [TestMethod()]
        public void TestDeleteP()
        {
            var db = new CT25Team24Entities();
            var product = db.SanPhams.AsNoTracking().First();
        }
        [TestMethod()]
        public void TestDetailsG()
        {
            var controller = new HangSPsController();
            var result0 = controller.Details(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);



            var db = new CT25Team24Entities();
            var product = db.HangSPs.First();
            var result1 = controller.Details(product.MaHang) as ViewResult;
            Assert.IsNotNull(result1);



            var model = result1.Model as HangSP;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaHang, model.MaHang);
            Assert.AreEqual(product.TenHang, model.TenHang);




        }
        [TestMethod()]
        public void TestDisposeG()
        {
            using (var controller = new HangSPsController()) { }
        }
    }
}