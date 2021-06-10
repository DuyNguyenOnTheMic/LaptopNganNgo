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

namespace Test.Controllers.Tests
{
    [TestClass()]
    public class HangSPsControllerTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [TestMethod()]
        public void Test_HangSP_Null()
        {
            var controller = new HangSPsController();
            var model = new HangSP()
            {
                TenHang = null
            };
            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Bạn chưa nhập hãng sản phẩm!")).Count() > 0);
        }

        [TestMethod()]
        public void Test_HangSP_Qua_50_Ky_Tu()
        {
            var controller = new HangSPsController();
            var model = new HangSP()
            {
                TenHang = "aiihihihihihihiihihihihihihihihihiihhihiihihihihihihihihiiiihihihihihiihihihi"
            };
            var result0 = controller.Create() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result0.ViewName));
            Assert.IsTrue(ValidateModel(model).Where(x => x.ErrorMessage.Contains("Hãng sản phẩm không được quá 50 kí tự!")).Count() > 0);
        }
    }
}