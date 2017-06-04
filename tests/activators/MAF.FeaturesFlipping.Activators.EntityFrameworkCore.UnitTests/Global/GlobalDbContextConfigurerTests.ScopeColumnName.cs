using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Global
{
    public partial class GlobalDbContextConfigurerTests
    {
        [Trait("Category", "UnitTest")]
        public class ScopeColumnName
        {
            [Fact]
            public void Calling_ScopeColumnName_With_A_New_Name_Changes_The_ScopeColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "NewScopeColumn";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfigurer(_ => { });

                // Act
                actual.ScopeColumnName(expectedScopeColumnName);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_ScopeColumnName_With_A_Null_Doesnt_Change_The_ScopeColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfigurer(_ => { });

                // Act
                actual.ScopeColumnName(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_ScopeColumnName_With_An_Empty_String_Doesnt_Change_The_ScopeColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfigurer(_ => { });

                // Act
                actual.ScopeColumnName("");

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
}
