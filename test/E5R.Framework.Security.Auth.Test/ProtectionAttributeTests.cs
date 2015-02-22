using System;
using Xunit;
using Moq;

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
        public void Allowed_Not_Accesses_IAuthenticationService_and_IAuthorizationService_If_ProtectionLevel_Is_Public()
        {
            // Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            var protectionAttribute = new ProtectionAttribute(ProtectionLevel.Public);

            // Act
            protectionAttribute.Allowed(mockAuthenticationService.Object, mockAuthorizationService.Object);

            // Assert
            mockAuthenticationService.Verify(service => service.IsAuthenticCredential, Times.Never());
            mockAuthorizationService.Verify(service => service.AllowUnsignedAction, Times.Never());
            mockAuthorizationService.Verify(service => service.HasRequiredPermissions(new string[]{}), Times.Never());
        }

        [Fact]
        public void Allowed_Not_Accesses_IAuthorizationService_If_ProtectionLevel_Is_Protected()
        {
            // Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            var protectionAttribute = new ProtectionAttribute(ProtectionLevel.Protected);

            // Act
            protectionAttribute.Allowed(mockAuthenticationService.Object, mockAuthorizationService.Object);

            // Assert
            mockAuthorizationService.Verify(service => service.AllowUnsignedAction, Times.Never());
            mockAuthorizationService.Verify(service => service.HasRequiredPermissions(new string[]{}), Times.Never());
        }

        [Fact]
        public void Allowed_Accesses_IAuthenticationService_If_ProtectionLevel_Is_Protected()
        {
            // Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            var protectionAttribute = new ProtectionAttribute(ProtectionLevel.Protected);

            // Act
            protectionAttribute.Allowed(mockAuthenticationService.Object, mockAuthorizationService.Object);

            // Assert
            mockAuthenticationService.Verify(service => service.IsAuthenticCredential, Times.AtLeastOnce());
        }

        [Fact]
        public void Not_Allowed_If_Not_Authenticated_In_ProtectionLevel_Protected()
        {
            // Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            var protectionAttribute = new ProtectionAttribute(ProtectionLevel.Protected);

            mockAuthenticationService.Setup(service => service.IsAuthenticCredential).Returns(false);

            // Act
            var result = protectionAttribute.Allowed(mockAuthenticationService.Object, mockAuthorizationService.Object);

            // Assert
            Assert.Equal(false, result);
        }
    }
}