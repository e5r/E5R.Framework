// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth
{
    public static class Constants
    {
        public static readonly string HttpAuthPrefixHeader = "X-EhAuth-";
        public static readonly string HttpAuthAppInstanceIdHeader = string.Concat(HttpAuthPrefixHeader, "AppInstanceId");
        public static readonly string HttpAuthSealHeader = string.Concat(HttpAuthPrefixHeader, "Seal");
        public static readonly string HttpAuthAccessTokenHeader = string.Concat(HttpAuthPrefixHeader, "AccessToken");
        public static readonly string HttpAuthNonceHeader = string.Concat(HttpAuthPrefixHeader, "Nonce");
        public static readonly string HttpAuthCNonceHeader = string.Concat(HttpAuthPrefixHeader, "CNonce");
        public static readonly string HttpAuthSealedAccessTokenHeader = string.Concat(HttpAuthPrefixHeader, "SealedAccessToken");
        public static readonly string HttpAuthOCNonceHeader = string.Concat(HttpAuthPrefixHeader, "OCNonce");

        public static readonly string JsonMimeContentType = "application/json";
        public static readonly string XmlMimeContentType = "application/xml";
    }
}
