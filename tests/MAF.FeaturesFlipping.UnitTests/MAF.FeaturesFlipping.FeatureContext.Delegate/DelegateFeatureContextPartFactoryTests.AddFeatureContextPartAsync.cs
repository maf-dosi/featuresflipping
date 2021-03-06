﻿using System;
using System.Threading.Tasks;
using Xunit;

namespace MAF.FeaturesFlipping.FeatureContext.Delegate
{
    public partial class DelegateFeatureContextPartFactoryTests
    {
        public class AddFeatureContextPartAsync
        {
            [Fact]
            public async Task The_Context_Is_Correclty_Passed_To_The_Provided_Part_Adder_Factory_Delegate()
            {
                // Arrange
                var featureContext = Factory.FeatureContext();
                var delegateFeatureContextPartFactory = new DelegateFeatureContextPartFactory(context =>
                {
                    if (featureContext != context)
                    {
                        throw new ArgumentException();
                    }
                    return Task.CompletedTask;
                }, _ => { });

                // Act & Assert
                await delegateFeatureContextPartFactory.AddFeatureContextPartAsync(featureContext);
            }

            [Fact]
            public async Task The_Provided_Part_Adder_Factory_Delegate_Are_Called()
            {
                // Arrange
                var paramToChange = false;
                var delegateFeatureContextPartFactory = new DelegateFeatureContextPartFactory(context =>
                {
                    paramToChange = true;
                    return Task.CompletedTask;
                }, _ => { });

                // Act
                await delegateFeatureContextPartFactory.AddFeatureContextPartAsync(Factory.FeatureContext());

                // Assert
                Assert.True(paramToChange);
            }

            [Fact]
            public void The_Returned_Task_Is_The_One_Returned_By_The_Provided_Part_Adder_Factory_Delegate()
            {
                // Arrange
                var task = new Task(() => { });
                var delegateFeatureContextPartFactory =
                    new DelegateFeatureContextPartFactory(context => task, _ => { });

                // Act & Assert
                var actual = delegateFeatureContextPartFactory.AddFeatureContextPartAsync(Factory.FeatureContext());

                // Assert
                Assert.Equal(task, actual);
            }
        }
    }
}