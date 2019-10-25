using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using Objektinis;
using System.Runtime.CompilerServices;
using ServerCallFromApp;
using ExternalDependencies;

namespace AppUnitTests
{
    [TestClass]
    public class SignUpUnitTests
    {

        SignUpFormManager sign = new SignUpFormManager(new DataManipulations(new HttpInternalClient()), new FormManagerData());

        [TestMethod]
        public void TestIsEmailValid_EscapedRoute()
        {
            Assert.IsTrue(sign.IsEmailValid("user%example.com@example.org"));
        }

        [TestMethod]
        public void TestIsEmailValid_Simple()
        {
            Assert.IsTrue(sign.IsEmailValid("simple@example.com"));
        }

        [TestMethod]
        public void TestIsEmailValid_OneLetter()
        {
            Assert.IsTrue(sign.IsEmailValid("x@example.com"));
        }

        [TestMethod]
        public void TestIsEmailValid_UucpMail()
        {
            Assert.IsTrue(sign.IsEmailValid("mailhost!username@example.org"));
        }


        // INVALIDS 

        [TestMethod]
        public void TestIsEmailValid_NoAtChar()
        {
            Assert.IsFalse(sign.IsEmailValid("Abc.example.com"));
        }

        [TestMethod]
        public void TestIsEmailValid_MultipleAtChars()
        {
            Assert.IsFalse(sign.IsEmailValid("A@b@c@example.com"));
        }

        [TestMethod]
        public void TestIsEmailValid_InvalidChars()
        {
            Assert.IsFalse(sign.IsEmailValid("a\"b(c)d, e: f; g<h> i[j\\k]l @example.com"));
        }

        [TestMethod]
        public void TestIsEmailValid_NoDotSeparator()
        {
            Assert.IsFalse(sign.IsEmailValid("just\"not\"right@example.com"));
        }

    }
}
