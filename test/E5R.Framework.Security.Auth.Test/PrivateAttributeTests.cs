using Xunit;
using System.Reflection;

namespace E5R.Framework.Security.Auth.Tests
{
    public class PrivateAttributeTests
    {
        [Fact]
        public void PrivateAttribute_Should_Inherit_ProtectionAttribute() 
        {
            // Arrange
            var parentType = typeof(ProtectionAttribute);
            var childType = typeof(PrivateAttribute);

            // Act
            var result = childType.GetTypeInfo().IsSubclassOf(parentType);
        
            // Assert
            Assert.True(result);
        }
    }
}