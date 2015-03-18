// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using Xunit;

namespace E5R.Framework.Security.Auth.Data.Tests
{
    public class AuthIdTests
    {
        [Fact]
        public void Is_Unique()
        {
            // Act
            var id1 = new AuthId();
            var id2 = new AuthId();
            var id3 = new AuthId();
            var id4 = new AuthId();

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
        [InlineData("91af911a4a4782893a7f710100b11bd6")]
        [InlineData("27ba8a09740a531061af51a78d62f9ee")]
        [InlineData("ac0317d7508af58215e4f5915482c850")]
        [InlineData("6bae241e83757b61fac9c62044aea43d")]
        [InlineData("0703ef76909e1b6ce3559933a9f3be04")]
        [InlineData("123373ee2e3c6bd1b9e79a06e40ad290")]
        public void Is_Bootable(string startValue)
        {
            // Act
            var id = new AuthId(startValue);

            // Assert
            Assert.Equal(id.ToString(), startValue);
        }

        [Theory]
        [InlineData("ac0317d7508af58215e4f5915482c850g")]
        [InlineData("0703ef76909e1b6ce3559933a9f3be04 fe")]
        [InlineData("Invalid string value")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("6bae241e83757b61fac9c62044aea43d123373ee2e3c6bd1b9e79a06e40ad290")]
        public void Rejects_Invalid_Formats(string startValue)
        {
            // Assert
            Assert.Throws<FormatException>(()=>new AuthId(startValue));
        }
    }
}
