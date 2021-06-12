using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers.Website_QuanTri.Tests
{
    [TestClass()]
    public class SanPhamsControllerTests
    {

        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            var controller = new SanPhamsController();
            var db = new CT25Team24Entities();

            //Act
            var result = controller.Index(1) as ViewResult;
            var model = result.Model as PagedList<SanPham>;
            int pageSize = 6;

            int pageNumber = 1;
            var links = (from l in db.SanPhams
                         select l).OrderBy(x => x.MaSP).ToPagedList(pageNumber, pageSize);

            //Assert
            Assert.AreEqual(links.Count, model.Count);
            Assert.AreEqual(pageSize, model.Count);
        }

        [TestMethod()]
        public void QT_SanPham_Test()
        {
            //Arrange
            var controller = new SanPhamsController();
            var db = new CT25Team24Entities();

            //Act
            var result = controller.QT_SanPham(null, null) as ViewResult;
            var model = result.Model as PagedList<SanPham>;
            int pageSize = 4;

            int pageNumber = 1;
            var links = db.SanPhams.ToList().OrderByDescending(x => x.MaSP).ToPagedList(pageNumber, pageSize);

            //Assert
            Assert.AreEqual(links.Count, model.Count);
            Assert.AreEqual(pageSize, model.Count);
        }

        [TestMethod()]
        public void Test_Get_Dispose_SanPham()
        {
            using (var controller = new SanPhamsController()) { }
        }

        [TestMethod()]
        public void Test_Get_Create_SanPham()
        {
            var controller = new SanPhamsController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Test_Post_Create_SanPham()
        {
            var sanpham = new SanPham
            {
                TenSP = "Tlinh",
                DonGiaGoc = -2,
                DonGiaKM = 8000000,
                DongSP = "MCK",
                SL = 1,
                TrangThaiSP = "Còn hàng",
                ThongTinChiTietSP = "BlaBla",
                MaHangSP = 1

            };
            var controller = new SanPhamsController();

            var picture = new Mock<HttpPostedFileBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Server).Returns(server.Object);
            controller.ControllerContext = new ControllerContext(context.Object, 
                new System.Web.Routing.RouteData(), controller);

            var filename = "wowwwww.png";
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(s => s);
            picture.Setup(p => p.SaveAs(It.IsAny<string>())).Callback<string>(s => filename = s);

            using (var scope = new TransactionScope())
            {
                controller.ModelState.Clear();
                var result1 = controller.Create(picture.Object, sanpham) as RedirectToRouteResult;
                Assert.IsNotNull(result1);
                Assert.AreEqual("QT_SanPham", result1.RouteValues["SanPhams"]);

                var db = new CT25Team24Entities();
                var entity = db.SanPhams.SingleOrDefault(p => p.TenSP == sanpham.TenSP
                && p.DonGiaGoc == sanpham.DonGiaGoc && p.DonGiaKM == sanpham.DonGiaKM
                && p.DongSP == sanpham.DongSP && p.SL == sanpham.SL
                && p.TrangThaiSP == sanpham.TrangThaiSP && p.ThongTinChiTietSP == sanpham.ThongTinChiTietSP
                && p.MaHangSP == sanpham.MaHangSP);
                Assert.IsNotNull(entity);
                Assert.AreEqual(sanpham.DonGiaGoc, entity.DonGiaGoc);

                Assert.IsTrue(filename.StartsWith("~/Images/"));

            }

            var result0 = controller.Create(picture.Object, sanpham) as ViewResult;
            //Assert.IsNotNull(result0);
        }

        [TestMethod()]
        public void Test_Get_Edit_SanPham()
        {

            var db = new CT25Team24Entities();
            var controller = new SanPhamsController();
            var product = db.SanPhams.First();
            var result = controller.Edit(product.MaSP) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.DonGiaKM, model.DonGiaKM);
            Assert.AreEqual(product.DonGiaGoc, model.DonGiaGoc);
            Assert.AreEqual(product.HinhAnhSP, model.HinhAnhSP);
            Assert.AreEqual(product.SL, model.SL);
            Assert.AreEqual(product.DongSP, model.DongSP);
            Assert.AreEqual(product.TrangThaiSP, model.TrangThaiSP);
            Assert.AreEqual(product.ThongTinChiTietSP, model.ThongTinChiTietSP);
        }

        [TestMethod()]
        public void Test_Post_Edit_SanPham()
        {
            {
                var random = new Random();
                var db = new CT25Team24Entities();
                var product = db.SanPhams.Find(10101);
                product.TenSP = random.NextDouble().ToString();
                product.DonGiaGoc = -random.Next();
                product.DonGiaKM = -random.Next();

                product.DongSP = random.NextDouble().ToString();
                product.SL = -random.Next();
                product.TrangThaiSP = random.NextDouble().ToString();
                product.ThongTinChiTietSP = random.NextDouble().ToString();

                var controller = new SanPhamsController();
                var result0 = controller.Edit(10101) as ViewResult;
                Assert.IsNotNull(result0);
            }
        }

        [TestMethod()]
        public void Details_SP_Test()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Details(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team24Entities();
            var product = db.SanPhams.Find(10101);
            var result1 = controller.Details(product.MaSP) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaSP, 10101);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.DonGiaKM, model.DonGiaKM);
            Assert.AreEqual(product.DonGiaGoc, model.DonGiaGoc);
            Assert.AreEqual(product.HinhAnhSP, model.HinhAnhSP);
            Assert.AreEqual(product.SL, model.SL);
            Assert.AreEqual(product.DongSP, model.DongSP);
            Assert.AreEqual(product.TrangThaiSP, model.TrangThaiSP);
            Assert.AreEqual(product.ThongTinChiTietSP, model.ThongTinChiTietSP);

        }

        [TestMethod()]
        public void Test_Get_Delete_SanPham()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Delete(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team24Entities();
            var product = db.SanPhams.First();
            var result1 = controller.Delete(product.MaSP) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.DonGiaKM, model.DonGiaKM);
            Assert.AreEqual(product.DonGiaGoc, model.DonGiaGoc);
            Assert.AreEqual(product.HinhAnhSP, model.HinhAnhSP);
            Assert.AreEqual(product.SL, model.SL);
            Assert.AreEqual(product.DongSP, model.DongSP);
            Assert.AreEqual(product.TrangThaiSP, model.TrangThaiSP);
            Assert.AreEqual(product.ThongTinChiTietSP, model.ThongTinChiTietSP);
        }

        [TestMethod()]
        public void Test_Post_Delete_SanPham()
        {
            var db = new CT25Team24Entities();
            var product = db.SanPhams.AsNoTracking().First();
        }

        [TestMethod()]
        public void Search_By_ID_KhachHang()
        {
            var db = new CT25Team24Entities();           
            string keyword = "10101";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(1 , 6);
            
            var controller = new SanPhamsController();
            var result = controller.Search(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, "10101");
            Assert.AreEqual(sanphams.Count, model.Count);
        }

        [TestMethod()]
        public void Search_By_Name_KhachHang()
        {
            var db = new CT25Team24Entities();
            string keyword = "Asus";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(1, 6);

            var controller = new SanPhamsController();
            var result = controller.Search(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(sanphams.Count, model.Count);
            Assert.AreEqual(keyword, "Asus");
        }

        [TestMethod()]
        public void Search_By_Name_With_Upper_Char_KhachHang()
        {
            var db = new CT25Team24Entities();
            string keyword = "ACER";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(1, 6);

            var controller = new SanPhamsController();
            var result = controller.Search(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(sanphams.Count, model.Count);
            Assert.AreEqual(keyword, "ACER");
        }

        [TestMethod()]
        public void Search_By_Name_With_Lower_Char_KhachHang()
        {
            var db = new CT25Team24Entities();
            string keyword = "hiếu";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(1, 6);

            var controller = new SanPhamsController();
            var result = controller.Search(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(sanphams.Count, model.Count);
            Assert.AreEqual(keyword, "hiếu");
        }

        [TestMethod()]
        public void Search_By_Name_With_Both_Upper_And_Lower_Char_KhachHang()
        {
            var db = new CT25Team24Entities();
            string keyword = "LaPTop";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(1, 6);

            var controller = new SanPhamsController();
            var result = controller.Search(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(sanphams.Count, model.Count);
            Assert.AreEqual(keyword, "LaPTop");
        }

        [TestMethod()]
        public void Search_Null_KhachHang()
        {
            var db = new CT25Team24Entities();
            string keyword = null;
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(1, 6);

            var controller = new SanPhamsController();
            var result = controller.Search(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(sanphams.Count, model.Count);
            Assert.AreEqual(keyword, null);
        }

        [TestMethod()]
        public void Search_Not_Found_KhachHang()
        {
            var db = new CT25Team24Entities();
            string keyword = "Alababa Trap";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(1, 6);

            var controller = new SanPhamsController();

            Assert.AreEqual(keyword, "Alababa Trap");
            Assert.AreEqual(sanphams.Count, 0);
        }

        [TestMethod()]
        public void Search_By_ID_QuanTri()
        {
            var db = new CT25Team24Entities();
            string keyword = "10074";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().
            OrderByDescending(x => x.MaSP).ToPagedList(1, 4);

            var controller = new SanPhamsController();
            var result = controller.QT_SanPham(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, "10074");
            Assert.AreEqual(sanphams.Count, model.Count);
        }

        [TestMethod()]
        public void Search_By_Name_QuanTri()
        {
            var db = new CT25Team24Entities();
            string keyword = "Acer";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().
            OrderByDescending(x => x.MaSP).ToPagedList(1, 4);

            var controller = new SanPhamsController();
            var result = controller.QT_SanPham(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, "Acer");
            Assert.AreEqual(sanphams.Count, model.Count);
        }

        [TestMethod()]
        public void Search_By_Name_With_Upper_Char_QuanTri()
        {
            var db = new CT25Team24Entities();
            string keyword = "DELL";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().
            OrderByDescending(x => x.MaSP).ToPagedList(1, 4);

            var controller = new SanPhamsController();
            var result = controller.QT_SanPham(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, "DELL");
            Assert.AreEqual(sanphams.Count, model.Count);
        }

        [TestMethod()]
        public void Search_By_Name_With_Lower_Char_QuanTri()
        {
            var db = new CT25Team24Entities();
            string keyword = "msi";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().
            OrderByDescending(x => x.MaSP).ToPagedList(1, 4);

            var controller = new SanPhamsController();
            var result = controller.QT_SanPham(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, "msi");
            Assert.AreEqual(sanphams.Count, model.Count);
        }

        [TestMethod()]
        public void Search_By_Name_With_Both_Upper_And_Lower_Char_QuanTri()
        {
            var db = new CT25Team24Entities();
            string keyword = "hIếU";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().
            OrderByDescending(x => x.MaSP).ToPagedList(1, 4);

            var controller = new SanPhamsController();
            var result = controller.QT_SanPham(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, "hIếU");
            Assert.AreEqual(sanphams.Count, model.Count);
        }

        [TestMethod()]
        public void Search_Null_QuanTri()
        {
            var db = new CT25Team24Entities();
            string keyword = null;
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().
            OrderByDescending(x => x.MaSP).ToPagedList(1, 4);

            var controller = new SanPhamsController();
            var result = controller.QT_SanPham(keyword, 1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PagedList<SanPham>;
            Assert.IsNotNull(model);
            Assert.AreEqual(keyword, null);
            Assert.AreEqual(sanphams.Count, model.Count);
        }

        [TestMethod()]
        public void Search_Not_Found_QuanTri()
        {
            var db = new CT25Team24Entities();
            string keyword = "Úm ba la xì bùa";
            var sanphams = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().
            OrderByDescending(x => x.MaSP).ToPagedList(1, 4);

            var controller = new SanPhamsController();

            Assert.AreEqual(keyword, "Úm ba la xì bùa");
            Assert.AreEqual(sanphams.Count, 0);
        }

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
        public void Test_Create_TenSP_MoreThan_200_Characters()
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
        public void Test_Create_DongSP_MoreThan_100_Characters()
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
        public void Test_Create_TrangThaiSP_MoreThan_50_Characters()
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

    }
}