using System;
using System.Collections.Generic;
using System.Text;
using App;
using AppUnitTests.TestHelpers;
using Xunit;

namespace AppUnitTests
{
    public class SignUp_Create_User_Should
    { 
        [Theory]
        [AutoMoqData]
         public async void TestUserIsCreated(SignUpFormManager signUpFormManager)
        {
           string name = StringGenerator.RandomString(4);
           string email = StringGenerator.RandomEmail();
                  
           Assert.Equal(2, await signUpFormManager.CreateUser(name, email, StringGenerator.RandomString(6)));                            //successful
           Assert.Equal(0, await signUpFormManager.CreateUser(StringGenerator.RandomString(4), email, StringGenerator.RandomString(6))); //existing email
           Assert.Equal(1, await signUpFormManager.CreateUser(name, StringGenerator.RandomEmail(), StringGenerator.RandomString(6)));    //existing username
           // BUG IN CreateUser!!! Allows creation with duplicate emails and returns 2 instead of 0 and duplicate user names (returns 2 instead of 1)
        }
    }
}
