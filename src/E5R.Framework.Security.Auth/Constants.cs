// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth
{
    // TODO: Move parts to E5R.Framework.Security.Auth.[NetUtils]
    public static class Constants
    {
        public static readonly string HttpAuthAppInstanceIdHeader = "X-E5RAuth-AppInstanceId";
        public static readonly string HttpAuthSealHeader = "X-E5RAuth-Seal";
        public static readonly string HttpAuthAccessTokenHeader = "X-E5RAuth-AccessToken";
        public static readonly string HttpAuthNonceHeader = "X-E5RAuth-Nonce";
        public static readonly string HttpAuthCNonceHeader = "X-E5RAuth-CNonce";
        public static readonly string HttpAuthSealedAccessTokenHeader = "X-E5RAuth-SealedAccessToken";
        public static readonly string HttpAuthOCNonceHeader = "X-E5RAuth-OCNonce";

        public static readonly string JsonMimeContentType = "application/json";
        public static readonly string XmlMimeContentType = "application/xml";
    }
}
