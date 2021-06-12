using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Controllers.Website_BanHang;
using Test.Models;

namespace UnitTest_LaptopNganNgo.Controllers.Website_BanHang
{
    [TestClass()]
    public class DangNhapControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            DangNhapController dangNhap = new DangNhapController();
            KhachHang kh = new KhachHang()
            {

                Email = "admin@gmail.com",
                MatKhau = "admin123"
            };

            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);


            var result = dangNhap.Index(kh) as RedirectToRouteResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

    }
}
