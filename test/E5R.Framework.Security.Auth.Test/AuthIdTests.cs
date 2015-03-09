using System;
using Xunit;

namespace E5R.Framework.Security.Auth.Tests
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
        [InlineData("6702197000cae7fff3a45a63a36c53e115d8cc3293b95ece8cf96afd31e517ed")]
        [InlineData("85b6ef8179e8ad6b6bd9fd602d5bcc88d35657ac06c5d3dd1fc4b88cbd9b68fe")]
        [InlineData("823301d71e31aa14da0bdec224925039cd460fcde098150272ae3a21603bbed1")]
        [InlineData("3b2546b5e40ebf0d943f5de5e8f754bda4d914995fb400ad55f14e810abd3210")]
        [InlineData("095b053d2778941277dbea3786b024031b4b855ce6be2a2a44b32a2822dba050")]
        public void Is_Bootable(string startValue)
        {
            // Act
            var id = new AuthId(startValue);

            // Assert
            Assert.Equal(id.ToString(), startValue);
        }

        [Theory]
        [InlineData("6702197000cae7fff3a45a63a36c53e115d8cc3293b95ece8cf96afd31e517eg")]
        [InlineData("85b6ef8179e8ad6b6bd9 fe")]
        [InlineData("Invalid string value")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("85b6ef8179e8ad6b6bd9fd602d5bcc88d35657ac06c5d3dd1fc4b88cbd9b68fe095b053d2778941277dbea3786b024031b4b855ce6be2a2a44b32a2822dba050")]
        public void Rejects_Invalid_Formats(string startValue)
        {
            // Assert
            Assert.Throws<Exception>(()=>new AuthId(startValue));
        }
    }
}
