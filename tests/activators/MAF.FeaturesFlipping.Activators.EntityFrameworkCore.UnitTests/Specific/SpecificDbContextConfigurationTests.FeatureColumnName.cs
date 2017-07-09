using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificDbContextConfigurationTests
    {
        [Trait("Category", "UnitTest")]
        public class FeatureColumnName
        {
            [Fact]
            public void Calling_FeatureColumnName_With_A_New_Name_Changes_The_FeatureColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "NewFeatureColumn";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", _ => feature => false);

                // Act
                actual.FeatureColumnName(expectedFeatureColumnName);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_FeatureColumnName_With_A_Null_Doesnt_Change_The_FeatureColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", _ => feature => false);

                // Act
                actual.FeatureColumnName(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_FeatureColumnName_With_An_Empty_String_Doesnt_Change_The_FeatureColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", _ => feature => false);

                // Act
                actual.FeatureColumnName("");

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
