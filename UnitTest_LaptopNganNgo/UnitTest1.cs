using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest_LaptopNganNgo
{
    [TestClass]
    public class UnitTest1
    {
        private double CongHaiSo(double so1, double so2)
        {
            return so1 + so2;
        }

        [TestMethod]
        public void TestMethod1()
        {
            double soThuNhat = 10;
            double soThuHai = 20;
            double kq = CongHaiSo(soThuNhat, soThuHai);
            Assert.AreEqual(30, kq);
        }
    }
}
