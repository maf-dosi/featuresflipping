using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public partial class GlobalDbContextConfigurationTests
    {
        [Trait("Category", "UnitTest")]
        public class Schema
        {
            [Fact]
            public void Calling_Schema_With_A_New_Name_Changes_The_Schema()
            {
                // Arrange
                var expectedSchema = "NewSchema";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "FeatureName";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfiguration(_ => { });

                // Act
                actual.Schema(expectedSchema);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_Schema_With_A_Null_Doesnt_Change_The_Schema()
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
                actual.Schema(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_Schema_With_An_Empty_String_Changes_The_Schema_To_Emtpy_String()
            {
                // Arrange
                var expectedSchema = "";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "FeatureName";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfiguration(_ => { });

                // Act
                actual.Schema(expectedSchema);

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