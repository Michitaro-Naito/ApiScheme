using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiScheme.Scheme;
using System.Diagnostics;
using ApiScheme;

namespace ApiSchemeTest
{
    [TestClass]
    public class Internal
    {
        [TestMethod]
        public void DummyException()
        {
            try
            {
                var e = ApiException.Create(new Out() { exception = "ApiScheme.TestApiException", message = "foo" });
                if (e != null)
                    throw e;
            }
            catch (TestApiException e)
            {
                Debug.WriteLine(e);
                return;
            }
            Assert.Fail("Exception not thrown.");
        }

        [TestMethod]
        public void DummyException2()
        {
            try
            {
                var e = ApiException.Create(new Out() { exception = "ApiScheme.SomeInvalidName", message = "bar" });
                if (e != null)
                    throw e;
            }
            catch (ApiException e)
            {
                Debug.WriteLine(e);
                return;
            }
            Assert.Fail("Exception not thrown.");
        }

        [TestMethod]
        public void DummyException3()
        {
            try
            {
                // System.ArgumentException exists but invalid for ApiException
                var e = ApiException.Create(new Out() { exception = "System.ArgumentException", message = "foo" });
                if (e != null)
                    throw e;
            }
            catch (ArgumentException e)
            {
                Assert.Fail("ArgumentException thrown.");
            }
            catch (ApiException e)
            {
                Debug.WriteLine(e);
                return;
            }
            Assert.Fail("Exception not thrown.");
        }
    }
}
