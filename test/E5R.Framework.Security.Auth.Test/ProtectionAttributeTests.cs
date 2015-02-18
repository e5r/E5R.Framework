using System;
using Xunit;

namespace E5R.Framework.Security.Auth.Test
{
    public class ProtectionAttributeTests
    {
        [Fact]
        public void ProtectionAttribute_Fails_If_RequiredPermissions_Used_With_ProtectionLevel_Public_or_Protected()
        {
            // Arrange
            var paramNameExpected = "requiredPermissions";
            var messageExpected = "Only used if protectionLevel is Private";

            // Act
            var ex1 = Assert.Throws<ArgumentException>(() =>
            {
                new ProtectionAttribute(ProtectionLevel.Public, new[]{""});
            }) as ArgumentException;

            var ex2 = Assert.Throws<ArgumentException>(() =>
            {
                new ProtectionAttribute(ProtectionLevel.Protected, new[]{""});
            }) as ArgumentException;

            // Assert
            Assert.Equal(paramNameExpected, ex1.ParamName);
            Assert.StartsWith(messageExpected, ex2.Message);

            Assert.Equal(paramNameExpected, ex1.ParamName);
            Assert.StartsWith(messageExpected, ex2.Message);
        }

        [Fact]
        public void ProtectionAttribute_Fails_If_RequiredPermissions_Is_Empty()
        {
            // Arrange
            var paramNameExpected = "requiredPermissions";
            var messageExpected = "At least one permission must be informed";

            // Act
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                new ProtectionAttribute(ProtectionLevel.Private, new string[]{});
            }) as ArgumentException;

            // Assert
            Assert.Equal(paramNameExpected, ex.ParamName);
            Assert.StartsWith(messageExpected, ex.Message);
        }

        [Fact]
        public void Allowed_Not_Accesses_IAuthenticationService_If_ProtectionLevel_Is_Public()
        {
            // Arrange

            // Act
            throw new NotImplementedException();

            // Assert
        }

        [Fact]
        public void Allowed_Not_Accesses_IAuthorizationService_If_ProtectionLevel_Is_Public()
        {
            // Arrange

            // Act
            throw new NotImplementedException();

            // Assert
        }

        [Fact]
        public void Allowed_Not_Accesses_IAuthorizationService_If_ProtectionLevel_Is_Protected()
        {
            // Arrange

            // Act
            throw new NotImplementedException();

            // Assert
        }

        [Fact]
        public void Allowed_Accesses_IAuthenticationService_If_ProtectionLevel_Is_Protected()
        {
            // Arrange

            // Act
            throw new NotImplementedException();

            // Assert
        }

        [Fact]
        public void Not_Allowed_If_Not_Authenticated_In_ProtectionLevel_Protected()
        {
            // Arrange

            // Act
            throw new NotImplementedException();

            // Assert
        }

        [Fact]
        public void Not_Allowed_If_Does_Not_Contain_All_Permissions_In_ProtectionLevel_Private()
        {
            // Arrange

            // Act
            throw new NotImplementedException();

            // Assert
        }

        [Fact]
        public void Allowed_If_Contain_All_Permissions_In_ProtectionLevel_Private()
        {
            // Arrange

            // Act
            throw new NotImplementedException();

            // Assert
        }
    }
}