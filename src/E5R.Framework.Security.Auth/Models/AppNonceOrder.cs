using System;

namespace E5R.Framework.Security.Auth.Models
{
    public class AppNonceOrder : DataModel<AppNonceOrder, App>
    {
        public AuthId Id { get; set; }
        public App App { get; set; }

        /// <example>
        /// {AppID}:{AppPrivateKey}:{AppInstanceHost}:{X-Auth-SealedSessionToken}:{X-Auth-Nonce}
        /// </example>
        public string Template { get; set; }

        public AppNonceOrder(){}

        public AppNonceOrder(App app)
        {
            Id = new AuthId();
            App = app;
        }
    }
}
