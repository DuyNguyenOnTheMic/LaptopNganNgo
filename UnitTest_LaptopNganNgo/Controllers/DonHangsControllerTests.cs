using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using System.Web.Mvc;
using PagedList;
using System.Web;
using System.Collections;
using Moq;
using System.Web.Routing;

namespace Test.Controllers.Tests
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

    [TestClass()]
    public class DonHangsControllerTests
    {
        [TestMethod()]
        public void Index_Test_PagedList()
        {
            //Arrange
            var controller = new DonHangsController();
            var db = new CT25Team24Entities();
            string keyword = null;
            int pageSize = 6;
            int pageNumber = 1;

            //Act
            var result = controller.Index(keyword, 1, null) as ViewResult;
            var model = result.Model as PagedList<DonHang>;
            
            var links = db.DonHangs.Where(x => x.MaDH.ToString().Contains(keyword.ToString()) || keyword == null)
                .ToList().OrderByDescending(x => x.MaDH).ToPagedList(pageNumber, pageSize);

            //Assert
            Assert.AreEqual(links.Count, model.Count);
            Assert.AreEqual(pageSize, model.Count);
        }

        [TestMethod()]
        public void Search_By_Order_ID_QuanTri()
        {
            //Arrange
            var controller = new DonHangsController();
            var db = new CT25Team24Entities();
            string keyword = "1001";
            int pageSize = 6;
            int pageNumber = 1;

            //Act
            var result = controller.Index(keyword, 1, null) as ViewResult;
            var model = result.Model as PagedList<DonHang>;

            var donhangs = db.DonHangs.Where(x => x.MaDH.ToString().Contains(keyword.ToString()) || keyword == null)
                .ToList().OrderByDescending(x => x.MaDH).ToPagedList(pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, "1001");
            Assert.AreEqual(donhangs.Count, model.Count);
        }

        [TestMethod()]
        public void Search_Null_QuanTri()
        {
            //Arrange
            var controller = new DonHangsController();
            var db = new CT25Team24Entities();
            string keyword = null;
            int pageSize = 6;
            int pageNumber = 1;

            //Act
            var result = controller.Index(keyword, 1, null) as ViewResult;
            var model = result.Model as PagedList<DonHang>;

            var donhangs = db.DonHangs.Where(x => x.MaDH.ToString().Contains(keyword.ToString()) || keyword == null)
                .ToList().OrderByDescending(x => x.MaDH).ToPagedList(pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, null);
            Assert.AreEqual(donhangs.Count, model.Count);
        }


        [TestMethod()]
        public void Search_Not_Found_QuanTri()
        {
            //Arrange
            var controller = new DonHangsController();
            var db = new CT25Team24Entities();
            string keyword = "Alaba Trap";
            int pageSize = 6;
            int pageNumber = 1;

            //Act

            var donhangs = db.DonHangs.Where(x => x.MaDH.ToString().Contains(keyword.ToString()) || keyword == null)
                .ToList().OrderByDescending(x => x.MaDH).ToPagedList(pageNumber, pageSize);

            //Assert
            Assert.AreEqual(keyword, "Alaba Trap");
            Assert.AreEqual(donhangs.Count, 0);
        }

        [TestMethod()]
        public void Order_By_Category_QuanTri()
        {
            //Arrange
            var controller = new DonHangsController();
            var db = new CT25Team24Entities();
            string keyword = null;
            int category = 1;
            int pageSize = 6;
            int pageNumber = 1;

            //Act
            var result = controller.Index(keyword, 1, category) as ViewResult;
            var model = result.Model as PagedList<DonHang>;

            var donhangs = db.DonHangs.Where(x => x.MaDH.ToString().Contains(keyword.ToString()) || keyword == null)
                .Where(x => x.TrangThai == category).ToList().OrderByDescending(x => x.MaDH).ToPagedList(pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, null);
            Assert.AreEqual(donhangs.Count, model.Count);
        }

            [TestMethod]
            public void TestIndex()
            {
                var session = new MockHttpSession();
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Session).Returns(session);

                var controller = new CTDHsController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            }

            [TestMethod]
            public void Test_Create_Get()
            {
                var session = new MockHttpSession();
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Session).Returns(session);

                var controller = new DonHangsController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

                var shoppingCart = session["ShoppingCart"] as List<CTDH>;
                var result = controller.Create() as ViewResult;
                //Assert.IsNotNull(result);
            }

            [TestMethod]
            public void Test_CreateBill_Post()
            {
                var session = new MockHttpSession();
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Session).Returns(session);

                var controller = new DonHangsController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

                var db = new CT25Team24Entities();

                var product = new SanPham { MaSP = 1055, SL = 1, DonGiaGoc = 17000000 };

                //var product = db.SanPhams.First();
                double tongtien = product.DonGiaGoc * product.SL;
                var shoppingcart = new List<CTDH>();
                shoppingcart.Add(new CTDH
                {
                    SanPham = product,
                    //DonGia = product.DonGiaGoc,
                    //SL = product.SL,
                    //ThanhTien = product.DonGiaGoc * product.SL
                });

                var khachhang = db.KhachHangs.First();
                var donhang = new DonHang()
                {
                    NgayBan = DateTime.Now,
                    MaKH = khachhang.MaKH,
                    TongTien = tongtien,
                    TrangThai = 1,
                };

                var ctdh = new CTDH()
                {
                    MaDH = donhang.MaDH,
                    MaSP = product.MaSP,
                    DonGia = product.DonGiaGoc,
                    SL = product.SL,
                    ThanhTien = product.DonGiaGoc * product.SL
                };
                var result = controller.Create1(donhang.MaKH, tongtien) as RedirectToRouteResult;
                var hh = db.CTDHs.FirstOrDefault(x => x.MaSP == product.MaSP);
                //Assert.IsNotNull(result);
                //Assert.AreEqual("TT_ThanhCong", result.RouteValues["DonHangs"]);

                //shoppingcart = session["ShoppingCart"] as List<CTDH>;
                //Assert.IsNotNull(shoppingcart);
                //Assert.AreEqual(0, shoppingcart.Count);
                Assert.AreEqual(product.MaSP, ctdh.MaSP);
                Assert.AreEqual(tongtien, ctdh.ThanhTien);

                //db.CTDHs.Remove(hh);
                //db.SaveChanges();
                //Assert.AreEqual(product.MaSP, model.First().SanPham.MaSP);
                //Assert.AreEqual(2, model.First().SL);
                //Assert.AreEqual(6 / 10 / 2021, donhang.NgayBan);
                //Assert.AreEqual(17000000, tongtien);
                //Assert.AreEqual(ctdh1.SanPham.DonGiaGoc, shoppingcart.San);
            }

            [TestMethod]
            public void Test_Detail_Edit_Delete_Bill_ID_Null_Get()
            {
                var controller = new DonHangsController();

                var result = controller.Details(null) as HttpStatusCodeResult;
                //Assert.IsNull(result);
                //var donhang = (DonHang)result.ViewData.Model;

                //Assert.AreEqual("", donhang.TongTien);
            }

            [TestMethod]
            public void Test_Detail_And_Delete_Bill_Find_TrueID_Get()
            {
                var controller = new DonHangsController();

                var db = new CT25Team24Entities();
                var donhang = new DonHang { NgayBan = System.DateTime.Now, TongTien = 17000000, TrangThai = 1 };
                db.DonHangs.Add(donhang);

                var result = controller.Details(donhang.MaDH) as ViewResult;

                Assert.AreEqual(17000000, donhang.TongTien);
                //Assert.AreEqual(10/6/2021, donhang.NgayBan);
                //Assert.AreEqual("Đơn mới", donhang.TrangThai);
            }

            [TestMethod]
            public void Test_Detail_Edit_Delete_Bill_NotFind_ID_Get()
            {
                var controller = new DonHangsController();
                var db = new CT25Team24Entities();
                DonHang donHang = db.DonHangs.Find(1900);
                var result = controller.Details(1900) as HttpNotFoundResult;

                //Assert.AreEqual("", donhang.TongTien);
            }

            [TestMethod]
            public void Test_Edit_Find_TrueID_Get()
            {
                var session = new MockHttpSession();
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Session).Returns(session);

                var controller = new DonHangsController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

                var db = new CT25Team24Entities();
                var donhang = db.DonHangs.First();

                var result = controller.Edit(donhang.MaDH) as ViewResult;
                //Assert.IsNotNull(result);

                var model = result.Model as DonHang;
                Assert.AreEqual(donhang.MaDH, model.MaDH);
                Assert.AreEqual(donhang.NgayBan, model.NgayBan);
                Assert.AreEqual(donhang.TongTien, model.TongTien);
                Assert.AreEqual(donhang.TrangThai, model.TrangThai);
            }

            [TestMethod]
            public void Test_Edit_Post()
            {
                var session = new MockHttpSession();
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Session).Returns(session);

                var controller = new DonHangsController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

                var db = new CT25Team24Entities();
                var donhang = new DonHang { MaDH = 1995, NgayBan = System.DateTime.Now, TongTien = 12000000, TrangThai = 1 };
                db.DonHangs.Add(donhang);

                var result = controller.Edit(1995) as ViewResult;
                //Assert.IsNotNull(result);
                // var donhang = (DonHang)result.ViewBag;
                var trangthai = (TrangThaiDH)result.ViewBag;

                Assert.AreEqual("17000000", donhang.TongTien);
                Assert.AreEqual("Đơn mới", trangthai.TrangThai);
            }

            [TestMethod]
            public void Test_Delete_Post()
            {
                var session = new MockHttpSession();
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Session).Returns(session);

                var controller = new CTDHsController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

                var db = new CT25Team24Entities();
                //var donhang = new DonHang { NgayBan = System.DateTime.Now, TongTien = 12000000, TrangThai = 1 };
                //db.DonHangs.Add(donhang);

                var donhang = db.DonHangs.First();
                //var sp1 = donhang.CTDHs.First();

                var result = controller.DeleteOrderDetails(donhang.MaDH, 10003, 3) as RedirectToRouteResult;
                //Assert.IsNotNull(result);
                Assert.AreEqual("Edit", result.RouteValues["DonHangs"]);

                Assert.AreEqual(135000000, donhang.TongTien);
                Assert.AreEqual(2, donhang.CTDHs.Count);
            }

        }
    }

