using Xunit;

namespace E5R.Framework.Security.Auth.Tests
{
    public class ProtectedAttributeTests
    {
        [Fact]
        public void ProtectedAttribute_Should_Inherit_ProtectionAttribute() 
        {
            // Arrange
            var parentType = typeof(ProtectionAttribute);
            var childType = typeof(ProtectedAttribute);

            // Act
            var result = childType.IsSubclassOf(parentType);
        
            // Assert
            Assert.True(result);
        }
    }
}