using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public partial class GlobalDbContextConfigurationTests
    {
        [Trait("Category", "UnitTest")]
        public class FeatureNameColumnName
        {
            [Fact]
            public void Calling_FeatureNameColumnName_With_A_New_Name_Changes_The_FeatureNameColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "NewFeatureColumn";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfiguration(_ => { });

                // Act
                actual.FeatureNameColumnName(expectedFeatureNameColumnName);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_FeatureNameColumnName_With_A_Null_Doesnt_Change_The_FeatureNameColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "FeatureName";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfiguration(_ => { });

                // Act
                actual.FeatureNameColumnName(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_FeatureNameColumnName_With_An_Empty_String_Doesnt_Change_The_FeatureNameColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "FeatureName";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfiguration(_ => { });

                // Act
                actual.FeatureNameColumnName("");

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }
        }
    }
}
