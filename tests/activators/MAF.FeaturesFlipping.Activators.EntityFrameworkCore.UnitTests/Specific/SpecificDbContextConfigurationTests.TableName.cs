using System.Threading.Tasks;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificDbContextConfigurationTests
    {
        [Trait("Category", "UnitTest")]
        public class TableName
        {
            [Fact]
            public void Calling_TableName_With_A_New_Name_Changes_The_TableName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "NewTableName";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureColumnName = "Feature";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", (_, __) => Task.FromResult(FeatureActivationStatus.NotSet));

                // Act
                actual.TableName(expectedTableName);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_TableName_With_A_Null_Doesnt_Change_The_TableName()
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
                actual.TableName(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureColumnName, actual.FeatureColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_TableName_With_An_Empty_String_Doesnt_Change_The_TableName()
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
                actual.TableName("");

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
