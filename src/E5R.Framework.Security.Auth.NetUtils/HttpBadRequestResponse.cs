// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth.NetUtils
{
    public class HttpBadRequestResponse : HttpAuthResponse
    {
        public HttpBadRequestResponse()
            : base((int)System.Net.HttpStatusCode.BadRequest)
        {}
    }
}