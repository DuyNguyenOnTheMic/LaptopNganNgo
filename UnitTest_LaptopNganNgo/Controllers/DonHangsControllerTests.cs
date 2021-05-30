using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using System.Web.Mvc;
using System.Web.Routing;
using Test.Models;

namespace Test.Controllers.Tests
{
    [TestClass()]
    public class DonHangsControllerTests
    {
        public class MockHttpSession : HttpSessionStateBase
        {
            public override object this[int key] { get => base[key]; set => base[key] = value; }
        }

        [TestMethod()]
        public void Test_GetShoppingCart()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new DonHangsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            session["ShoppingCart"] = null;
            var result = controller.Index(1,1,1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<CTDH>;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);

            var db = new CT25Team24Entities();
            var sanpham = db.SanPhams.First();
            var shoppingCart = new List<CTDH>();
            shoppingCart.Add(new CTDH
            {
                SanPham = sanpham,
                SL = 1
            });

            var ctdh = new CTDH();
            ctdh.SanPham = sanpham;
            ctdh.SL = 2;
            shoppingCart.Add(ctdh);

            session["ShoppingCart"] = shoppingCart;
            result = controller.Index(1, 1, 1) as ViewResult;
            Assert.IsNotNull(result);

            model = result.Model as List<CTDH>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(sanpham.MaSP, model.First().SanPham.MaSP);
            Assert.AreEqual(3, model.First().SL);
         
        }

        [TestMethod()]
        public void TestCreate()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new DonHangsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new CT25Team24Entities();
            var sanpham = db.SanPhams.First();
            var result = controller.Create1(sanpham.MaSP, 2) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            var shoppingCart = session["ShoppingCart"] as List<CTDH>;
            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.Count);
            Assert.AreEqual(sanpham.MaSP, shoppingCart.First().SanPham.MaSP);
            Assert.AreEqual(2, shoppingCart.First().SL);
        }

        [TestMethod()]
        public void Test_Get_Dispose_DonHang()
        {
            using (var controller = new DonHangsController()) { }
        }

        [TestMethod()]
        public void Test_Delete_View_Data_DonHang()
        {
            //Arrange
            var controller = new DonHangsController();

            //Act
            var result = controller.Delete(1) as ViewResult;
            var donhang = (DonHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("Đang xử lý", donhang.TrangThai);
        }
    }
}