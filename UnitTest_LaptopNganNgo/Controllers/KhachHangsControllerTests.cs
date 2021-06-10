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

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [TestMethod()]
        public void Test_Create_TT_KH_HoTenKH_Null()
        {
            var controller = new KhachHangsController();
            var model = new KhachHang()
            {
                HoTen = null,
                DiaChi = "Hà Nội",
                DienThoai = "1234567899",
                Email = null,
                GioiTinh = "Nữ",
                MatKhau = null,
                XacNhanMK = null,
                VaiTro = "Khách lẻ"
            };

            var result0 = controller.Dky_TT_KH(model) as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập họ và tên!")).Count() > 0);     
        }

        [TestMethod()]
        public void Test_Create_TT_KH_HoTenKH_NhapSo()
        {
            var controller = new KhachHangsController();
            var model = new KhachHang()
            {
                HoTen = "Anh123",
                DiaChi = "Hà Nội",
                DienThoai = "1234567899",
                Email = null,
                GioiTinh = "Nữ",
                MatKhau = null,
                XacNhanMK = null,
                VaiTro = "Khách lẻ"
            };

            var result0 = controller.Dky_TT_KH(model) as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Họ và tên chỉ được nhập chữ!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TT_KH_HoTenKH_Qua_100_Ky_Tu()
        {
            var controller = new KhachHangsController();
            var model = new KhachHang()
            {
                HoTen = "Không có kính không phải vì xe không có kính Bom giật, bom rung kính vỡ đi rồi. Ung dung buồng lái ta ngồi, Nhìn đất, nhìn trời, nhìn thẳng. Nhìn thấy gió vào xoa mắt đắng. Nhìn thấy con đường chạy thẳng vào tim. Thấy sao trời và đột ngột cánh chimNhư sa,như ùa vào buồng lái",
                DiaChi = "Hà Nội",
                DienThoai = "1234567899",
                Email = null,
                GioiTinh = "Nữ",
                MatKhau = null,
                XacNhanMK = null,
                VaiTro = "Khách lẻ"
            };

            var result0 = controller.Dky_TT_KH(model) as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Họ và tên không được quá 100 kí tự!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TT_KH_GioiTinhKH_Null()
        {
            var controller = new KhachHangsController();
            var model = new KhachHang()
            {
                HoTen = "Anh",
                DiaChi = "Hà Nội",
                DienThoai = "1234567899",
                Email = null,
                GioiTinh = null,
                MatKhau = null,
                XacNhanMK = null,
                VaiTro = "Khách lẻ"
            };

            var result0 = controller.Dky_TT_KH(model) as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa chọn giới tính!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TT_KH_SDT_Null()
        {
            var controller = new KhachHangsController();
            var model = new KhachHang()
            {
                HoTen = "Anh",
                DiaChi = "Hà Nội",
                DienThoai = null,
                Email = null,
                GioiTinh = "Nam",
                MatKhau = null,
                XacNhanMK = null,
                VaiTro = "Khách lẻ"
            };

            var result0 = controller.Dky_TT_KH(model) as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập số điện thoại!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TT_KH_SDT_KhongDungDinhDang()
        {
            var controller = new KhachHangsController();
            var model = new KhachHang()
            {
                HoTen = "Anh",
                DiaChi = "Hà Nội",
                DienThoai = "0123456789123456789",
                Email = null,
                GioiTinh = "Nam",
                MatKhau = null,
                XacNhanMK = null,
                VaiTro = "Khách lẻ"
            };

            var result0 = controller.Dky_TT_KH(model) as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Số điện thoại không đúng định dạng!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TT_KH_DiaChiKH_Null()
        {
            var controller = new KhachHangsController();
            var model = new KhachHang()
            {
                HoTen = "Anh",
                DiaChi = null,
                DienThoai = "0123456789",
                Email = null,
                GioiTinh = "Nam",
                MatKhau = null,
                XacNhanMK = null,
                VaiTro = "Khách lẻ",
                
            };

            var result0 = controller.Dky_TT_KH(model) as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập địa chỉ!")).Count() > 0);
        }

    }
}