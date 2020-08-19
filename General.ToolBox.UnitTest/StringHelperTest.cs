using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace General.ToolBox.UnitTest
{
    [TestClass]
    public class StringHelperTest
    {
        [TestMethod]
        public void IsAlphaNum_ParamNull()
        {
            var result = StringHelper.IsAlphaNum(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsAlphaNum_ParamEmpty()
        {
            var result = StringHelper.IsAlphaNum("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsAlphaNum_ParamSpecialCaracters()
        {
            var result = StringHelper.IsAlphaNum("\\?é&");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsAlphaNum_ParamSpace()
        {
            var result = StringHelper.IsAlphaNum("    ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsAlphaNum_ParamNumber()
        {
            var result = StringHelper.IsAlphaNum("1223434");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsAlphaNum_ParamAlphaLower()
        {
            var result = StringHelper.IsAlphaNum("azety");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsAlphaNum_ParamAlphaUpper()
        {
            var result = StringHelper.IsAlphaNum("AZERTY");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsAlphaNum_ParamAlphaNumUpperLower()
        {
            var result = StringHelper.IsAlphaNum("Aze3RtY123");
            Assert.IsTrue(result);
        }

    }
}
