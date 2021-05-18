using Moq;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers.Website_QuanTri;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Test.Models;
using System.Transactions;
using Test.Controllers;
using System.Web.Routing;

namespace UnitTest_LaptopNganNgo.Controllers
{
    public class MockHttpSession : HttpSessionStateBase
    {
        public Hashtable buffer = new Hashtable();
        public override object this[string key]
        {
            get
            {
                return buffer[key];
            }
            set
            {
                buffer[key] = value;
            }
        }
       
    }
    [TestClass]
    public class CTDHsControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new CTDHsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            session["ShoppingCart"] = null;
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<CTDH>;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);

            var db = new CT25Team24Entities();
            Assert.AreEqual(0, model.Count);
        }
    
    }
}
