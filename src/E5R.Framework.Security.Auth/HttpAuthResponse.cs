// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Microsoft.AspNet.Http;
using System;

namespace E5R.Framework.Security.Auth
{
    // TODO: Move to E5R.Framework.Security.Auth.[NetUtils]
    public class HttpAuthResponse
    {
        public int StatusCode { get; private set; }
        public object Body { get; protected set; } = null;
        public string AccessToken { get; protected set; } = null;
        public string SealedAccessToken { get; protected set; } = null;
        public string Nonce { get; protected set; } = null;
        public string CNonce { get; protected set; } = null;
        public string OCNonce { get; protected set; } = null;

        public HttpAuthResponse(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}