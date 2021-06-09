using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Test.Models;
using System.Transactions;
using Test.Controllers;
using System.Web.Routing;
using Moq;

namespace UnitTest_LaptopNganNgo.Controllers
{
    public class MockHttpSession : HttpSessionStateBase
    {
        public Hashtable buffer = new Hashtable();
        public override object this[string key]
        {
            get
            {
                return buffer[key];
            }
            set
            {
                buffer[key] = value;
            }
        }

    }
    [TestClass]
    public class CTDHControllerTest
    {
        [TestMethod]
        public void TestIndex_ShoppingCart_Null()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new CTDHsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            session["ShoppingCart"] = null;
            var result = controller.Index() as RedirectToRouteResult;

            Assert.AreEqual("GioHang_Empty", result.RouteValues["Action"]);

            var shoppingcart = session["ShoppingCart"] as List<CTDH>;
            Assert.AreEqual(0, shoppingcart.Count);
        }

        [TestMethod]
        public void TestIndex_ShoppingCart_Notnull()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new CTDHsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new CT25Team24Entities();
            var product = db.SanPhams.First();
            var shoppingcart = new List<CTDH>();
            shoppingcart.Add(new CTDH
            {
                SanPham = product,
                SL = 1
            });

            var ctdh = new CTDH();
            ctdh.SanPham = product;
            ctdh.SL = 1;
            shoppingcart.Add(ctdh);

            session["ShoppingCart"] = shoppingcart;
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<CTDH>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(product.MaSP, model.First().SanPham.MaSP);
            Assert.AreEqual(2, model.First().SL);
        }

        [TestMethod]
        public void TestCreate()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new CTDHsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new CT25Team24Entities();
            var product = db.SanPhams.First();

            var result = controller.Create(product.MaSP, 2, 17000000) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["Action"]);

            var shoppingcart = session["ShoppingCart"] as List<CTDH>;
            //Assert.IsNotNull(shoppingcart);
            Assert.AreEqual(1, shoppingcart.Count);
            Assert.AreEqual(product.MaSP, shoppingcart.First().SanPham.MaSP, shoppingcart.First().DonGia);
            Assert.AreEqual(2, shoppingcart.First().SL);
            Assert.AreEqual(34000000, shoppingcart.First().SL * shoppingcart.First().DonGia);
        }

        [TestMethod]
        public void TestEdit()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new CTDHsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new CT25Team24Entities();
            var product = new SanPham { MaSP = 10001, SL = 1, DonGiaGoc = 17000000 };
            var product1 = new SanPham { MaSP = 10003, SL = 1, DonGiaGoc = 10000000 };
            var product2 = new SanPham { MaSP = 10004, SL = 2, DonGiaGoc = 17000000 };

            int[] product_id = { 10001, 10003, 10004 };
            int[] Sl = { 1, 2, 3 };
            double[] Dongia = { 17000000, 10000000, 17000000 };

            var shoppingcart = new List<CTDH>();
            var ctdh1 = new CTDH();
            ctdh1.SanPham = product;
            var ctdh2 = new CTDH();
            ctdh2.SanPham = product1;
            var ctdh3 = new CTDH();
            ctdh3.SanPham = product2;
            shoppingcart.Add(ctdh1);
            shoppingcart.Add(ctdh2);
            shoppingcart.Add(ctdh3);

            session["ShoppingCart"] = shoppingcart;
            var result = controller.Edit(product_id, Sl, Dongia) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);

            Assert.AreEqual(3, shoppingcart.Count);
            Assert.AreEqual(6, shoppingcart.Sum(x => x.SL));
            Assert.AreEqual(88000000, shoppingcart.Sum(x => x.SL * x.DonGia));
        }
        [TestMethod]
        public void TestDelete_Product()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new CTDHsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new CT25Team24Entities();
            var product = new SanPham { MaSP = 10001, SL = 1, DonGiaGoc = 17000000 };
            var product1 = new SanPham { MaSP = 10003, SL = 1, DonGiaGoc = 15000000 };
            var product2 = new SanPham { MaSP = 10004, SL = 2, DonGiaGoc = 10000000 };

            var shoppingcart = new List<CTDH>();
            var ctdh1 = new CTDH();
            ctdh1.SanPham = product;
            var ctdh2 = new CTDH();
            ctdh2.SanPham = product1;
            var ctdh3 = new CTDH();
            ctdh3.SanPham = product2;
            shoppingcart.Add(ctdh1);
            shoppingcart.Add(ctdh2);
            shoppingcart.Add(ctdh3);

            session["ShoppingCart"] = shoppingcart;
            var result = controller.Delete(10001) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);

            Assert.AreEqual(2, shoppingcart.Count);
        }
        [TestMethod]
        public void TestDelete_Product_Final()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new CTDHsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new CT25Team24Entities();
            var product = new SanPham { MaSP = 10001, SL = 1, DonGiaGoc = 17000000 };

            var shoppingcart = new List<CTDH>();
            var ctdh1 = new CTDH();
            ctdh1.SanPham = product;
            shoppingcart.Add(ctdh1);


            session["ShoppingCart"] = shoppingcart;
            var result = controller.Delete(10001) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual(0, shoppingcart.Count);
        }

    }
}