using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiScheme.Client;
using ApiScheme.Scheme;
using System.Net;
using ApiScheme;
using System.Diagnostics;

namespace ApiSchemeTest
{
    [TestClass]
    public class ApiCall
    {
        [TestMethod]
        public void Plus()
        {
            var str = "!\"#$%&'()=~|12-^\\,./;:+*[]{}`@_?><";
            var o = Api.Get<PlusOut>(new PlusIn() { a = 1, b = 2, echo = str });
            Assert.AreEqual(3, o.c);
            Assert.AreEqual(str, o.echo);
        }

        [TestMethod]
        public void InvalidIn()
        {
            try
            {
                var o = Api.Get<PlusOut>(new PlusInvalidInn() { a = 1, b = 2 });
            }
            catch (ArgumentException)
            {
                return;
            }
            Assert.Fail("ArgumentException not thrown.");
        }

        [TestMethod]
        public void InvalidOut()
        {
            try
            {
                var o = Api.Get<PlusInvalidOut>(new PlusIn() { a = 1, b = 2 });
            }
            catch (ArgumentException)
            {
                return;
            }
            Assert.Fail("ArgumentException not thrown.");
        }

        [TestMethod]
        public void GetException()
        {
            try
            {
                var o = Api.Get<GetExceptionOut>(new GetExceptionIn());
            }
            catch (TestApiException e)
            {
                Debug.WriteLine(e);
                return;
            }
            Assert.Fail("Exception not thrown.");
        }
    }
}
