// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;

namespace E5R.Framework.Security.Auth
{
    public class HttpBadRequestResponse : HttpAuthResponse
    {
        public HttpBadRequestResponse()
            : base((int)System.Net.HttpStatusCode.BadRequest)
        {
            Body = new Exception("Test exception to serialize object!");
        }
    }
}