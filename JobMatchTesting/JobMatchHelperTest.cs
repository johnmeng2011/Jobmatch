using Jobmatch.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobmatchTesting
{
    [TestFixture]
    public class JobMatchHelperTest
    {

        [Test]
        public void GetFebonacciInverseWorks()
        {
            var array  = JobMatchHelper.GetFebonacciInverse(10);
            Assert.AreEqual(array,new int[] { 89,55,34,21,13,8,5,3,2,1});
        }

        [Test]
        public void PurcgeDuplicateWorks()
        {
            var array = new string[] { "aa", " aa", "bb" };
            var output = JobMatchHelper.PurgeDuplicates(array);
            Assert.AreEqual(output,new string[] {"aa","bb" });
        }
    }
}
