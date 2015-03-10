using System;

namespace E5R.Framework.Security.Auth.Models
{
    public class AppNonceOrder
    {
        public AuthId Id { get; set; }
        public App App { get; set; }

        /// <example>
        /// {AppID}:{AppPrivateKey}:{AppInstanceHost}:{X-Auth-SealedSessionToken}:{X-Auth-Nonce}
        /// </example>
        public string Template { get; set; }
    }
}
