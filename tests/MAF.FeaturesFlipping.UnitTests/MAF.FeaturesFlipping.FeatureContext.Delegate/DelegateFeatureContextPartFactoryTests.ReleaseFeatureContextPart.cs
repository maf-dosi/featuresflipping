using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.FeatureContext.Delegate
{
    public partial class DelegateFeatureContextPartFactoryTests
    {
        public class ReleaseFeatureContextPart
        {
            [Fact]
            public void The_Context_Is_Correclty_Passed_To_The_Provided_Part_Releaser_Factory_Delegate()
            {
                // Arrange
                var featureContext = Factory.FeatureContext();
                var delegateFeatureContextPartFactory = new DelegateFeatureContextPartFactory(_ => Task.CompletedTask,
                    context =>
                    {
                        if (featureContext != context)
                        {
                            throw new ArgumentException();
                        }
                    });

                // Act & Assert
                delegateFeatureContextPartFactory.ReleaseFeatureContextPart(featureContext);
            }

            [Fact]
            public void The_Provided_Part_Releaser_Factory_Delegate_Are_Called()
            {
                // Arrange
                var paramToChange = false;
                var delegateFeatureContextPartFactory =
                    new DelegateFeatureContextPartFactory(_ => Task.CompletedTask, _ => { paramToChange = true; });

                // Act
                delegateFeatureContextPartFactory.ReleaseFeatureContextPart(Factory.FeatureContext());

                // Assert
                Assert.True(paramToChange);
            }
        }
    }
}