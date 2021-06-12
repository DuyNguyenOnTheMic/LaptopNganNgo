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

namespace Test.Controllers.Tests
{
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
    }
}