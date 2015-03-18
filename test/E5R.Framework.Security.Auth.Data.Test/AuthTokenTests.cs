// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using Xunit;

namespace E5R.Framework.Security.Auth.Data.Tests
{
    public class AuthTokenTests
    {
        [Fact]
        public void Is_Unique()
        {
            // Act
            var id1 = new AuthToken();
            var id2 = new AuthToken();
            var id3 = new AuthToken();
            var id4 = new AuthToken();

            // Assert
            Assert.NotEqual(id1.ToString(), id2.ToString());
            Assert.NotEqual(id1.GetHashCode(), id2.GetHashCode());
            Assert.NotEqual(id1.ToBytes(), id2.ToBytes());

            Assert.NotEqual(id1.ToString(), id3.ToString());
            Assert.NotEqual(id1.GetHashCode(), id3.GetHashCode());
            Assert.NotEqual(id1.ToBytes(), id3.ToBytes());

            Assert.NotEqual(id1.ToString(), id4.ToString());
            Assert.NotEqual(id1.GetHashCode(), id4.GetHashCode());
            Assert.NotEqual(id1.ToBytes(), id4.ToBytes());


            Assert.NotEqual(id2.ToString(), id3.ToString());
            Assert.NotEqual(id2.GetHashCode(), id3.GetHashCode());
            Assert.NotEqual(id2.ToBytes(), id3.ToBytes());

            Assert.NotEqual(id2.ToString(), id4.ToString());
            Assert.NotEqual(id2.GetHashCode(), id4.GetHashCode());
            Assert.NotEqual(id2.ToBytes(), id4.ToBytes());


            Assert.NotEqual(id3.ToString(), id4.ToString());
            Assert.NotEqual(id3.GetHashCode(), id4.GetHashCode());
            Assert.NotEqual(id3.ToBytes(), id4.ToBytes());
        }

        [Theory]
        [InlineData("d49854716b0fe3d83e7c6828ffcd6f44354b2879095011eac66e8a446c9c9b46cadb0c6c84e038ff3cec84b662797d7f")]
        [InlineData("e7148f9a30d99e612c949cedaddeb9d2afd40e1f7868d202c0f766fcc349318d1b03897dfdb9443d95b6e15ad03120d2")]
        [InlineData("71df9c5eccba8e1433195ef0104b5cb22687b55b427016366f11cdfcecab2a8d94d6000482bcf7a331cf4e1b322ee3b8")]
        [InlineData("2e100e1eae7bac7e75f49eb6b9083db8e0a3bbd73b3897c868367322e834fbfe5107f765f19c65c20c9b2a8309e6432e")]
        [InlineData("f8553ac7abc5734358eca51addd4f063fdf08c2f879761b35b9ddba8ac88955abaac686e35a2dbf14d1fc3e811a95f35")]
        public void Is_Bootable(string startValue)
        {
            // Act
            var id = new AuthToken(startValue);

            // Assert
            Assert.Equal(id.ToString(), startValue);
        }

        [Theory]
        [InlineData("e72c6d43c82d8bfd0d9758121ab48bG9b16823b6136eae759641826fd200c4ed23dedca4ca26b101c82656974355e5c6")]
        [InlineData("6d7a5385cdf1e6715d8b92803d4484 871d724463fec077cc57ba07d9ecf697f44ecacfb95d4b7f1abec7adf551449dcb")]
        [InlineData("Invalid string value")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("e7148f9a30d99e612c949cedaddeb9d2afd40e1f7868d202c0f766fcc349318d1b03897dfdb9443d95b6e15ad03120d26d7a5385cdf1e6715d8b92803d4484871d724463fec077cc57ba07d9ecf697f44ecacfb95d4b7f1abec7adf551449dcb")]
        public void Rejects_Invalid_Formats(string startValue)
        {
            // Assert
            Assert.Throws<FormatException>(()=>new AuthToken(startValue));
        }
    }
}
