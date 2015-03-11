// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Xunit;

namespace E5R.Framework.Security.Auth.Test
{
    public class ProtectionLevelTests
    {
        [Fact]
        public void ProtectionLevel_PublicIndexIs0()
        {
            // Arrange
            var expected = 0;

            // Act
            var index = (int)ProtectionLevel.Public;

            // Assert
            Assert.Equal(expected, index);
        }

        [Fact]
        public void ProtectionLevel_ProtectedIndexIs1()
        {
            // Arrange
            var expected = 1;

            // Act
            var index = (int)ProtectionLevel.Protected;

            // Assert
            Assert.Equal(expected, index);
        }

        [Fact]
        public void ProtectionLevel_PrivateIndexIs2()
        {
            // Arrange
            var expected = 2;

            // Act
            var index = (int)ProtectionLevel.Private;

            // Assert
            Assert.Equal(expected, index);
        }
    }
}
