using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace E5R.Framework.Security.Auth
{
    public class AuthId
    {
        private readonly string _value = null;

        public AuthId()
        {
            _value = Guid.NewGuid().ToString().Replace("-",string.Empty);

            using(var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.Unicode.GetBytes(_value));
                _value = string.Empty;

                foreach(var h in hash)
                {
                    _value += string.Format("{0:x2}", h);
                }
            }

            Validate();
        }

        public AuthId(string value)
        {
            _value = value;

            Validate();
        }

        private void Validate()
        {
            // Considers SHA256 algorithm
            if(_value?.Length == 64){
                if(new Regex(@"^[0-9a-fA-F]+$").IsMatch(_value)){
                    return;
                }
            }
            throw new Exception(string.Format("AuthId({0}) invalid format.", _value));
        }

        public override string ToString()
        {
            return _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public byte[] ToBytes()
        {
            return ToBytes(Encoding.Unicode);
        }

        public byte[] ToBytes(Encoding encoding)
        {
            return encoding.GetBytes(_value);
        }
    }
}
