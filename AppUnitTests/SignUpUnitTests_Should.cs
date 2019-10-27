using AppUnitTests.TestHelpers;
using App;
using Xunit;

namespace AppUnitTests
{
    public class SignUpUnitTests_Should
    {
        [Theory]
        [AutoMoqData]
        public void TestIsEmailValid(SignUpFormManager signUpFormManager)
        {
            Assert.True(signUpFormManager.IsEmailValid("user%example.com@example.org"));
            Assert.True(signUpFormManager.IsEmailValid("simple@example.com"));
            Assert.True(signUpFormManager.IsEmailValid("x@example.com"));
            Assert.True(signUpFormManager.IsEmailValid("mailhost!username@example.org"));
            Assert.False(signUpFormManager.IsEmailValid("Abc.example.com"));
            Assert.False(signUpFormManager.IsEmailValid("A@b@c@example.com"));
            Assert.False(signUpFormManager.IsEmailValid("a\"b(c)d, e: f; g<h> i[j\\k]l @example.com"));
            Assert.False(signUpFormManager.IsEmailValid("just\"not\"right@example.com"));
        }
    }
}
