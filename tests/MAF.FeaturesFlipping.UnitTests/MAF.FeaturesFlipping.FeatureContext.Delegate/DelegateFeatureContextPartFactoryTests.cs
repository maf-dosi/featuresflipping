using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MAF.FeaturesFlipping.FeatureContext.Delegate
{
    public partial class DelegateFeatureContextPartFactoryTests
    {
        [Fact]
        public void The_Constructor_Throw_ArgumentNullException_When_addFeatureContextPart_Is_Null()
        {
            // Arrange
            var expectedParamName = "addFeatureContextPart";

            // Act & Assert
            var actualException =
                Assert.Throws<ArgumentNullException>(() => new DelegateFeatureContextPartFactory(null, _ => { }));
            Assert.Equal(expectedParamName, actualException.ParamName);
        }

        [Fact]
        public void The_Constructor_Throw_ArgumentNullException_When_releaseFeatureContextPart_Is_Null()
        {
            // Arrange
            var expectedParamName = "releaseFeatureContextPart";

            // Act & Assert
            var actualException =
                Assert.Throws<ArgumentNullException>(() => new DelegateFeatureContextPartFactory(_ => Task.CompletedTask, null));
            Assert.Equal(expectedParamName, actualException.ParamName);
        }
    }
}
