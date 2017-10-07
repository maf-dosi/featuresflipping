using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.AspNetCore
{
    public partial class FeatureTagHelperTests
    {
        [Trait("Category", "UnitTest")]
        public class ProcessAsync
        {
            private TagHelperContext MakeTagHelperContext(TagHelperAttributeList attributes = null)
            {
                attributes = attributes ?? new TagHelperAttributeList();

                return new TagHelperContext(
                    attributes,
                    new Dictionary<object, object>(),
                    Guid.NewGuid().ToString("N"));
            }

            private TagHelperOutput MakeTagHelperOutput(string tagName, TagHelperAttributeList attributes = null, string childContent = null)
            {
                attributes = attributes ?? new TagHelperAttributeList();

                return new TagHelperOutput(
                    tagName,
                    attributes,
                    (useCachedResult, encoder) =>
                    {
                        TagHelperContent tagHelperContent = new DefaultTagHelperContent();
                        tagHelperContent = tagHelperContent.SetContent(childContent);
                        return Task.FromResult(tagHelperContent);
                    });
            }

            [Fact]
            public async Task Content_Should_Be_Suppressed_When_Feature_Is_Inactive()
            {
                // Arrange
                var childContent = "content";
                var context = MakeTagHelperContext();
                var output = MakeTagHelperOutput("feature", childContent: childContent);
                var featureServiceMock = new Mock<IFeatureService>();
                featureServiceMock.Setup(_ => _.IsFeatureActiveAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(false);
                var tagHelper = new FeatureTagHelper(featureServiceMock.Object);

                // Act
                await tagHelper.ProcessAsync(context, output);

                // Assert
                Assert.Null(output.TagName);
                Assert.Empty(output.PreContent.GetContent());
                Assert.Empty(output.Content.GetContent());
                Assert.Empty(output.PostContent.GetContent());
                Assert.True(output.IsContentModified);
            }

            [Fact]
            public async Task Content_Is_Returned_When_Feature_Is_Active()
            {
                // Arrange
                var childContent = "content";
                var context = MakeTagHelperContext();
                var output = MakeTagHelperOutput("feature", childContent: childContent);
                var featureServiceMock = new Mock<IFeatureService>();
                featureServiceMock.Setup(_ => _.IsFeatureActiveAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(true);
                var tagHelper = new FeatureTagHelper(featureServiceMock.Object);

                // Act
                await tagHelper.ProcessAsync(context, output);

                // Assert
                Assert.Null(output.TagName);
                Assert.Empty(output.PreContent.GetContent());
                Assert.Empty(output.Content.GetContent());
                Assert.Equal(childContent, (await output.GetChildContentAsync()).GetContent());
                Assert.Empty(output.PostContent.GetContent());
                Assert.False(output.IsContentModified);
            }

            [Fact]
            public async Task Throws_ArgumentNullException_When_Context_Is_Null()
            {
                // Arrange
                var expectedParamName = "context";
                var featureServiceMock = new Mock<IFeatureService>();
                var featureTagHelper = new FeatureTagHelper(featureServiceMock.Object);

                // Act && Assert
                var actual =
                    await Assert.ThrowsAsync<ArgumentNullException>(() => featureTagHelper.ProcessAsync(null, null));
                Assert.Equal(expectedParamName, actual.ParamName);
            }

            [Fact]
            public async Task Throws_ArgumentNullException_When_Output_Is_Null()
            {
                // Arrange
                var expectedParamName = "output";
                var featureServiceMock = new Mock<IFeatureService>();
                var featureTagHelper = new FeatureTagHelper(featureServiceMock.Object);
                var context = MakeTagHelperContext();

                // Act && Assert
                var actual = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    featureTagHelper.ProcessAsync(context, null));
                Assert.Equal(expectedParamName, actual.ParamName);
            }
        }
    }
}