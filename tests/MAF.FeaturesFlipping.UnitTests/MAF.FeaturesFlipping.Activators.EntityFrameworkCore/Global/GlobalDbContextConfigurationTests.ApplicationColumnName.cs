﻿using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public partial class GlobalDbContextConfigurationTests
    {
        [Trait("Category", "UnitTest")]
        public class ApplicationColumnName
        {
            [Fact]
            public void Calling_ApplicationColumnName_With_A_New_Name_Changes_The_ApplicationColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "GlobalFeature";
                var expectedApplicationColumnName = "NewApplicationColumn";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "FeatureName";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new GlobalDbContextConfiguration(_ => { });

                // Act
                actual.ApplicationColumnName(expectedApplicationColumnName);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_ApplicationColumnName_With_A_Null_Doesnt_Change_The_ApplicationColumnName()
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
                actual.ApplicationColumnName(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_ApplicationColumnName_With_An_Empty_String_Doesnt_Change_The_ApplicationColumnName()
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
                actual.ApplicationColumnName("");

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
