using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Global
{
    [Trait("Category", "UnitTest")]
    public partial class GlobalDbContextConfigurerTests
    {
        [Fact]
        public void The_Constructor_Sets_The_Default_Value()
        {
            // Arrange
            var expectedSchema = "Feature";
            var expectedTableName = "GlobalFeature";
            var expectedApplicationColumnName = "Application";
            var expectedScopeColumnName = "Scope";
            var expectedFeatureColumnName = "Feature";
            var expectedIsActiveColumnName = "IsActive";

            // Act
            var actual = new GlobalDbContextConfigurer(_ => { });

            // Assert
            Assert.Equal(expectedSchema, actual.Schema());
            Assert.Equal(expectedTableName, actual.TableName());
            Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
            Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
            Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
            Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
        }
    }
}
