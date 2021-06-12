using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;
using System.ComponentModel.DataAnnotations;

namespace Test.Controllers.Website_QuanTri.Tests
{
    [TestClass()]
    public class SanPhamsControllerTests
    {

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [TestMethod()]
        public void Test_Create_TenSP_Null()
        {
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = null,
                DongSP = "Dell",
                ThongTinChiTietSP = "ahh",
                TrangThaiSP = "Còn hàng",
                SL = 12,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Bạn chưa nhập tên sản phẩm!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TenSP_Qua_200_Ky_Tu()
        {
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = "Hồi nhỏ sống với đồng với sông rồi với bể. Hồi chiến tranh ở rừng vầng trăng thành tri kỷ. Trần trụi với thiên nhiên hồn nhiên như cây cỏ ngỡ không bao giờ quên cái vầng trăng tình nghĩa. Từ hồi về thành phố quen ánh điện cửa gương vầng trăng đi qua ngõ như người dưng qua đường",
                DongSP = "Dell",
                ThongTinChiTietSP = "ahh",
                TrangThaiSP = "Còn hàng",
                SL = 12,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Tên sản phẩm không được quá 200 kí tự!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_DongSP_Null()
        {
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = "Ahihi",
                DongSP = null,
                ThongTinChiTietSP = "ahh",
                TrangThaiSP = "Còn hàng",
                SL = 12,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Bạn chưa nhập dòng sản phẩm!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_DongSP_Qua_100_Ky_Tu()
        {
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = "Ahihi",
                DongSP = "Một bếp lửa chờn vờn sương sớm. Một bếp lửa ấp iu nồng đượm. Cháu thương bà biết mấy nắng mưa! Lên bốn tuổi cháu đã quen mùi khói. Năm ấy là năm đói mòn đói mỏi, Bố đi đánh xe, khô rạc ngựa gầy, Chỉ nhớ khói hun nhèm mắt cháu Nghĩ lại đến giờ sống mũi còn cay!",
                ThongTinChiTietSP = "ahh",
                TrangThaiSP = "Còn hàng",
                SL = 12,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Dòng sản phẩm không được quá 100 kí tự!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_ThongTinChiTietSP_Null()
        {
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = "Ahihi",
                DongSP = "Ahuhu",
                ThongTinChiTietSP = null,
                TrangThaiSP = "Còn hàng",
                SL = 12,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Bạn chưa nhập thông tin chi tiết sản phẩm!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TrangThaiSP_Null()
        {
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = "Ahihi",
                DongSP = "Ahuhu",
                ThongTinChiTietSP = "Aha",
                TrangThaiSP = null,
                SL = 12,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Bạn chưa nhập tình trạng sản phẩm!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_TrangThaiSP_Qua_50_Ky_Tu()
        {
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = "Ahihi",
                DongSP = "Ahuhu",
                ThongTinChiTietSP = "Aha",
                TrangThaiSP = "Quê hương anh nước mặn, đồng chua. Làng tôi nghèo đất cày lên sỏi đá. Anh với tôi đôi người xa lạ. Tự phương trời chẳng hẹn quen nhau. Súng bên súng, đầu sát bên đầu. Đêm rét chung chăn thành đôi tri kỷ. Đồng chí!",
                SL = 12,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Tình trạng không được quá 50 kí tự!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_Create_SoLuongNhoHon1()
        {
            
            var controller = new SanPhamsController();
            var model = new SanPham()
            {
                TenSP = "Ahihi",
                DongSP = "Ahuhu",
                ThongTinChiTietSP = "Aha",
                TrangThaiSP = "Hết Hàng",
                SL = -5,
                DonGiaGoc = 1200000,
                DonGiaKM = 50000
            };

            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Equals("Bạn không thể nhập giá trị nhỏ hơn {1}")).Count() > 0);
            
        }

        [TestMethod()]
        public void Test_Create_SanPham_Successs()
        {
            var controller = new SanPhamsController();
            var db = new CT25Team24Entities();
            var model = new SanPham()
            {
                TenSP = "Ahihi",

            };

            var result = controller.Create() as ViewResult;
            var haha = db.SanPhams.FirstOrDefault(x => x.TenSP == model.TenSP);

            Assert.AreEqual(model.TenSP, haha.TenSP);

            db.SanPhams.Remove(haha);
            db.SaveChanges();


        }

    }
}