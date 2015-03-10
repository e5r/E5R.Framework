using System;
using System.Text;
using System.Security.Cryptography;

namespace E5R.Framework.Security.Auth
{
    public class AuthId : Id<AlgorithmMD5, UnicodeEncoding, IdSize32>
    {
        public AuthId() : base() {}
        public AuthId(string value) : base(value) {}
    }
}
