using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace AcceptanceTests.TestHelpers
{
    internal class AutoMoqData : AutoDataAttribute
    {
        public AutoMoqData( ) : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
            
        }
    }
}
