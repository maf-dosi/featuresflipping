﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    [Trait("Category", "UnitTest")]
    public partial class ConfigurationFeatureTests
    {
        [Fact]
        public void A_Null_ConfigurationSection_Throw_A_ArgumentNullException()
        {
            // Arrange
            var expectedParamName = "configurationSection";

            // Act & Assert
            var actual = Assert.Throws<ArgumentNullException>(() => new ConfigurationFeature(null));
            Assert.Equal(expectedParamName, actual.ParamName);
        }
        [Fact]
        public void A_NotNull_ConfigurationSection_Do_Not_Throw_An_Exception()
        {
            // Arrange
            var configurationSection = new ConfigurationSection(
                new ConfigurationRoot(
                    new List<IConfigurationProvider>()),
                "somePath");
            // Act & Assert
            var feature = new ConfigurationFeature(configurationSection);
            Assert.NotNull(feature);
        }
    }
}