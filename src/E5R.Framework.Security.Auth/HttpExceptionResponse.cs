// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;

namespace E5R.Framework.Security.Auth
{
    public class HttpExceptionResponse : HttpAuthResponse
    {
        public HttpExceptionResponse(Exception exception)
            : base((int)System.Net.HttpStatusCode.InternalServerError)
        {
            Body = new { Exception = exception };
        }
    }
}