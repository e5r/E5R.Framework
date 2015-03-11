// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Xunit;

namespace E5R.Framework.Security.Auth.Tests
{
    public class PublicAttributeTests
    {
        [Fact]
        public void PublicAttribute_Should_Inherit_ProtectionAttribute()
        {
            // Arrange
            var parentType = typeof(ProtectionAttribute);
            var childType = typeof(PublicAttribute);

            // Act
            var result = childType.IsSubclassOf(parentType);

            // Assert
            Assert.True(result);
        }
    }
}
