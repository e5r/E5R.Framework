using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace E5R.Framework.Security.Auth
{
    public class AuthToken : Id<AlgorithmSHA384, UnicodeEncoding, IdSize96>
    {
        public AuthToken() : base() {}
        public AuthToken(string value) : base(value) {}
    }
}
