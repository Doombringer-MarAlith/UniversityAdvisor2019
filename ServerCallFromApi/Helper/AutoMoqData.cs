using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Dbo.UnitTests.Helper
{
    internal class AutoMoqData : AutoDataAttribute
    {
        public AutoMoqData( ) : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
            
        }
    }
}
