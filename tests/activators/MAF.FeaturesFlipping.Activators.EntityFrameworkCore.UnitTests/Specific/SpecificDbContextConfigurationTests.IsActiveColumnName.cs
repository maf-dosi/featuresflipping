using System.Threading.Tasks;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificDbContextConfigurationTests
    {
        [Trait("Category", "UnitTest")]
        public class IsActiveColumnName
        {
            [Fact]
            public void Calling_IsActiveColumnName_With_A_New_Name_Changes_The_IsActiveColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "NewIsActiveColumn";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", (_, __) => Task.FromResult(FeatureActivationStatus.NotSet));

                // Act
                actual.IsActiveColumnName(expectedIsActiveColumnName);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_IsActiveColumnName_With_A_Null_Doesnt_Change_The_IsActiveColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", (_, __) => Task.FromResult(FeatureActivationStatus.NotSet));

                // Act
                actual.IsActiveColumnName(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_IsActiveColumnName_With_An_Empty_String_Doesnt_Change_The_IsActiveColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", (_, __) => Task.FromResult(FeatureActivationStatus.NotSet));

                // Act
                actual.IsActiveColumnName("");

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
