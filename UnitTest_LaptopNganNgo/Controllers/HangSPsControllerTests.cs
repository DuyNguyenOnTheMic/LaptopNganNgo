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
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace UnitTest_LaptopNganNgo.Controllers
{



    [TestClass()]
    public class HangSPsControllerTest
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

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
        public void Test_Create_Not_Null()
        {
            var controller = new HangSPsController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void Test_Create_HangSP_Success()
        {
            var controller = new HangSPsController();
            var db = new CT25Team24Entities();

            var hangSP = new HangSP()
            {

                TenHang = "Lăng LD",

            };
            
            var result = controller.Create(hangSP) as ViewResult;
            var hihi = db.HangSPs.FirstOrDefault(x=>x.TenHang == hangSP.TenHang);

            Assert.AreEqual(hangSP.MaHang, hihi.MaHang);
            Assert.AreEqual(hangSP.TenHang, hihi.TenHang);

            db.HangSPs.Remove(hihi);
            db.SaveChanges();

        }

        [TestMethod()]
        public void Test_Create_HangSP_Null()
        {
            //Arrange
            var controller = new HangSPsController();

            //Act
            var model = new HangSP()
            {
                TenHang = null
            };

            //Assert
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Bạn chưa nhập hãng sản phẩm!")).Count() > 0);

        }

        [TestMethod()]
        public void Test_Create_HangSP_MoreThan_50_Characters()
        {
            //Arrange
            var controller = new HangSPsController();

            //Act
            var model = new HangSP()
            {
                TenHang = "blablablablablablablablablablablablablablablablabla"
            };

            //Assert
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Hãng sản phẩm không được quá 50 kí tự!")).Count() > 0);

        }

        [TestMethod()]
        public void Test_Create_HangSP_Equals_50_Characters()
        {
            //Arrange
            var controller = new HangSPsController();

            //Act
            var model = new HangSP()
            {
                TenHang = "blablablablablablablablablablablablablablablablabl"
            };

            //Assert
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("")).Count() == 0);

        }


        [TestMethod()]
        public void Test_Create_HangSP_LessThan_50_Characters()
        {
            //Arrange
            var controller = new HangSPsController();

            //Act
            var model = new HangSP()
            {
                TenHang = "blablablablablablablablablablablablablablablablab"
            };

            //Assert
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("")).Count() == 0);

        }

        [TestMethod()]
        public void Test_Edit_HangSP_Success()
        {

            var db = new CT25Team24Entities();
            var controller = new HangSPsController();


            var hangsp = db.HangSPs.First(x => x.MaHang == 60);
            hangsp.TenHang = "Okela";
            db.Entry(hangsp).State = EntityState.Modified;
            db.SaveChanges();
            var result = controller.Edit(60) as ViewResult;
            var model = result.Model as HangSP;



            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(60, model.MaHang);
            Assert.AreEqual("Okela", model.TenHang);

        }
        [TestMethod()]
        public void TestDelete_HangSP()
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
        public void Test_Details_HangSP()
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
       
    }
}