// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using E5R.Framework.Security.Auth;

namespace Microsoft.AspNet.Builder
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseE5RAuthServer(this IApplicationBuilder app, string path)
        {
            return app.UseMiddleware<AuthenticationServerMiddleware>(path);
        }
    }
}
