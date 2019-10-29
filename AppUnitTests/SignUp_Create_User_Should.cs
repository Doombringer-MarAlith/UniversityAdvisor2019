using System;
using System.Collections.Generic;
using System.Text;
using App;
using App.FormManagers;
using AppUnitTests.TestHelpers;
using Xunit;

namespace AppUnitTests
{
    public class SignUp_Create_User_Should
    { 
        [Theory]
        [AutoMoqData]
         public async void TestUserIsCreated(SignUpFormManager signUpFormManager, string name)
        {
           string email = StringGenerator.RandomEmail();
                  
           Assert.Equal((int)CreateUserReturn.SUCCESS, await signUpFormManager.CreateUser(name, email, StringGenerator.RandomString(6)));                            //successful
           Assert.Equal((int)CreateUserReturn.EMAIL_TAKEN, await signUpFormManager.CreateUser(StringGenerator.RandomString(4), email, StringGenerator.RandomString(6))); //existing email
           Assert.Equal((int)CreateUserReturn.USERNAME_TAKEN, await signUpFormManager.CreateUser(name, StringGenerator.RandomEmail(), StringGenerator.RandomString(6)));    //existing username
           // BUG IN CreateUser!!! 
        }
      
    }
}
