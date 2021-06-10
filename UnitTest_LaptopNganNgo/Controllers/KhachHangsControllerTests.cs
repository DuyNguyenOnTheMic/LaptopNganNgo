﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;
using System.Net;
using Moq;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

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

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_Email_Null()
        {
            //Arrange
            var controller = new KhachHangsController();
            //var db = new CT25Team24Entities();

            //Act
            var model = new KhachHang()
            {
                HoTen = null,
                DiaChi = null,
                DienThoai = null,
                Email = null,
                GioiTinh = null,
                MatKhau = "123456",
                XacNhanMK = "123456",
                NgaySinh = null,
                VaiTro = "Admin"
            };
           

            var result0 = controller.Dky_QT(model) as ViewResult;

            //Assert

            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập Email")).Count() > 0);

        }

        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_Password_Null()
        {
            //Arrange
            var controller = new KhachHangsController();
            //var db = new CT25Team24Entities();

            //Act
            var model = new KhachHang()
            {
                HoTen = null,
                DiaChi = null,
                DienThoai = null,
                Email = "duy567890@gmail.com",
                GioiTinh = null,
                MatKhau = "",
                XacNhanMK = "blabla",
                NgaySinh = null,
                VaiTro = "Admin"
            };


            var result0 = controller.Dky_QT(model) as ViewResult;

            //Assert

            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập mật khẩu")).Count() > 0);

        }


        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_Password_Not_Match()
        {
            //Arrange
            var controller = new KhachHangsController();
            //var db = new CT25Team24Entities();

            //Act
            var model = new KhachHang()
            {
                HoTen = null,
                DiaChi = null,
                DienThoai = null,
                Email = "duy567890@gmail.com",
                GioiTinh = null,
                MatKhau = "hehe",
                XacNhanMK = "blabla",
                NgaySinh = null,
                VaiTro = "Admin"
            };


            var result0 = controller.Dky_QT(model) as ViewResult;

            //Assert

            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Xác nhận mật khẩu không trùng!")).Count() > 0);

        }

        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_Email_And_Password_Null()
        {
            //Arrange
            var controller = new KhachHangsController();
            //var db = new CT25Team24Entities();

            //Act
            var model = new KhachHang()
            {
                HoTen = null,
                DiaChi = null,
                DienThoai = null,
                Email = null,
                GioiTinh = null,
                MatKhau = "",
                XacNhanMK = "blabla",
                NgaySinh = null,
                VaiTro = "Admin"
            };


            var result0 = controller.Dky_QT(model) as ViewResult;

            //Assert

            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập Email")).Count() > 0);
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập mật khẩu")).Count() > 0);
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Xác nhận mật khẩu không trùng!")).Count() > 0);

        }

        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_Password_Less_Than_6_Character()
        {
            //Arrange
            var controller = new KhachHangsController();
            //var db = new CT25Team24Entities();

            //Act
            var model = new KhachHang()
            {
                HoTen = null,
                DiaChi = null,
                DienThoai = null,
                Email = "duy567890@gmail.com",
                GioiTinh = null,
                MatKhau = "123",
                XacNhanMK = "123",
                NgaySinh = null,
                VaiTro = "Admin"
            };


            var result0 = controller.Dky_QT(model) as ViewResult;

            //Assert

            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Mật khẩu phải dài ít nhất 6 kí tự!")).Count() > 0);

        }

        [TestMethod()]
        public void Test_Create_TaiKhoan_QuanTri_Email_Not_Have_AtSign()
        {
            //Arrange
            var controller = new KhachHangsController();
            //var db = new CT25Team24Entities();

            //Act
            var model = new KhachHang()
            {
                HoTen = null,
                DiaChi = null,
                DienThoai = null,
                Email = "duy7890@gmail.com",
                GioiTinh = null,
                MatKhau = "123456",
                XacNhanMK = "123456",
                NgaySinh = null,
                VaiTro = "Admin"
            };


            var result0 = controller.Dky_QT(model) as ViewResult;

            //Assert

            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("E-mail không đúng định dạng!")).Count() > 0);

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