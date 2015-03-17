// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth.NetUtils
{
    public class HttpUnauthorizedResponse : HttpAuthResponse
    {
        public HttpUnauthorizedResponse()
            : base((int)System.Net.HttpStatusCode.Unauthorized)
        { }
    }
}
