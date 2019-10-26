using AppUnitTests.TestHelpers;
using App;
using Xunit;

namespace AppUnitTests
{
	public class SignUpUnitTests_Should
	{
		[Theory]
		[AutoMoqData]
        public void TestIsEmailValid(SignUpFormManager sign)
        {
            Assert.True(sign.IsEmailValid("user%example.com@example.org"));
			Assert.True(sign.IsEmailValid("simple@example.com"));
			Assert.True(sign.IsEmailValid("x@example.com"));
			Assert.True(sign.IsEmailValid("mailhost!username@example.org"));
			Assert.False(sign.IsEmailValid("Abc.example.com"));
			Assert.False(sign.IsEmailValid("A@b@c@example.com"));
			Assert.False(sign.IsEmailValid("a\"b(c)d, e: f; g<h> i[j\\k]l @example.com"));
			Assert.False(sign.IsEmailValid("just\"not\"right@example.com"));
		}

    }
}
