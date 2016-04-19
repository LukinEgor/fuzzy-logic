using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ExstensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<int> list = new List<int> {1,2,3};

            int sum = 0;
            list.ForEach(i => sum += i);

            Assert.AreEqual(sum, 6);
        }
    }
}
