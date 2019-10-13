using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace ServerCallFromApp.UnitTests.Helper
{
    internal class AutoMoqData : AutoDataAttribute
    {
        public AutoMoqData( ) : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
            
        }
    }
}
