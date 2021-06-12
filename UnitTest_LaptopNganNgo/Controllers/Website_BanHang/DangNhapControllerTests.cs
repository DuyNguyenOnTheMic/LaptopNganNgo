using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_BanHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Moq;
using System.Web.Mvc;

namespace Test.Controllers.Website_BanHang.Tests
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
    

