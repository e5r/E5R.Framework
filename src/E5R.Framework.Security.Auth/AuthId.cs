using System;

namespace E5R.Framework.Security.Auth
{
    public class AuthId
    {
        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}