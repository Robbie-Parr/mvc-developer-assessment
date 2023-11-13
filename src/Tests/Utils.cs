using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils;
using System;

namespace Tests
{
    [TestClass]
    public class Test_DateFormatting
    {
        [TestMethod]
        public void Test_Date_Invariable()
        {
            DateTime now = DateTime.Now;
            string result = Utils.ToDate(now);
            Assert.AreEqual(Utils.ToDate(now), result);
        }

        [TestMethod]
        public void Test_Date_FormatCorrect()
        {
            string result = Utils.ToDate(new DateTime());
            Assert.AreEqual("January 01, 0001", result);
        }
    }
}
