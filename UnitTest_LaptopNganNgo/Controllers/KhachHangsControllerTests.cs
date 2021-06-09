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

        [TestMethod()]
        public void Test_Details_View_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Details(3) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("Nguyễn Tân Duy", khachhang.HoTen);
        }

        [TestMethod()]
        public void Test_Details_GioiTinhKH_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Details(2) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("Nữ", khachhang.GioiTinh);
        }

        [TestMethod()]
        public void Test_Details_DiaChiKH_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Details(3) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("HCM", khachhang.DiaChi);
        }

        [TestMethod()]
        public void Test_Details_SDTKH_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Details(3) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("0705653375", khachhang.DienThoai);
        }

        [TestMethod()]
        public void Test_Delete_HoTenKH_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Delete(4) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("Trần Quốc Nam", khachhang.HoTen);
        }

        [TestMethod()]
        public void Test_Delete_DiaChiKH_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.Delete(3) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("HCM", khachhang.DiaChi);
        }


        [TestMethod()]
        public void Test_CapNhat_TT_MatKhauKH_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.CapNhat_TT_KH(6) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("467329d70768629b6948b530e2cdcb51", khachhang.MatKhau);
        }


        [TestMethod()]
        public void Test_CapNhat_TT_Email_Data_KhachHang()
        {
            //Arrange
            var controller = new KhachHangsController();

            //Act
            var result = controller.CapNhat_TT_KH(6) as ViewResult;
            var khachhang = (KhachHang)result.ViewData.Model;

            //Assert
            Assert.AreEqual("ahihi123@gmail.com", khachhang.Email);
        }

        [TestMethod()]
        public void Test_Get_Dispose_KhachHang()
        {
            using (var controller = new KhachHangsController()) { }
        }
    }
}