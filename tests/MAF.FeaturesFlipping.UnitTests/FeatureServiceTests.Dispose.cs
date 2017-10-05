using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.UnitTests
{
    public partial class FeatureServiceTests
    {
        [Trait("Category", "UnitTest")]
        public class Dispose
        {
            [Fact]
            public void Calling_Dispose_Pass_Call_To_IFeatureContextAccessor_Dispose()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.Dispose())
                    .Verifiable("Should be called once");

                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                featureService.Dispose();

                // Assert
                featureContextAccessorMock.Verify(_ => _.Dispose(), Times.Once);
            }
            [Fact]
            public void Calling_Dispose_Twice_Do_Nothing_More_Than_The_First()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.Dispose())
                    .Verifiable("Should be called only once");

                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                featureService.Dispose();
                featureService.Dispose();

                // Assert
                featureContextAccessorMock.Verify(_ => _.Dispose(), Times.Once);
            }
        }
    }
}