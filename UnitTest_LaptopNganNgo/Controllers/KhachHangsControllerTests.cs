using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;
using System.Net;


namespace Test.Controllers.Website_QuanTri.Tests
{
    [TestClass()]
    public class KhachHangsControllerTests
    {
        public ViewResult HttpNotFound { get; private set; }

        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            var controller = new KhachHangsController();
            var db = new CT25Team24Entities();

            //Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<KhachHang>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(db.KhachHangs.Count(), model.Count);
        }

        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_NotNull()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Dky_QT() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_AreEqual()
        {
            //Arrange
            var rand = new Random();
            var controller = new KhachHangsController();

            //Act
            var khachHang = new KhachHang
            {
                HoTen = rand.NextDouble().ToString(),
                DiaChi = rand.NextDouble().ToString(),
                DienThoai = rand.Next().ToString(),
                Email = rand.NextDouble().ToString(),
                GioiTinh = rand.NextDouble().ToString(),
                MatKhau = rand.NextDouble().ToString(),
                NgaySinh = DateTime.Today,
                VaiTro = rand.NextDouble().ToString()
            };
            var khachhang = khachHang;

            var result0 = controller.Dky_QT() as ViewResult;

            //Assert
            Assert.IsNotNull(result0);
        }

        //False
        [TestMethod()]
        public void Test_Details_View_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Details(3) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("Hoàng Minh Thuận", khachhang.HoTen);
        }
        //True
        [TestMethod()]
        public void Test_Details_View_Data_KhachHang2()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Details(3) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("Nguyễn Tân Duy", khachhang.HoTen);
        }
    }
}