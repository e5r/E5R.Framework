using System;

namespace E5R.Framework.Security.Auth.Models
{
    public class AccessToken
    {
        public AuthToken Token { get; set; }
        public AppInstance AppInstance { get; set; }
        public string Nonce { get; set; }
        public bool NonceConfirmed { get; set; }

        public AccessToken() : this(null) {}

        public AccessToken(AppInstance appInstance)
        {
            Token = new AuthToken();
            AppInstance = appInstance;
        }
    }
}
