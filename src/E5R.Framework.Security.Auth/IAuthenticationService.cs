// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using E5R.Framework.Security.Auth.Models;
using Microsoft.AspNet.Http;
using System;

namespace E5R.Framework.Security.Auth
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Tells you whether a credential authentic logged
        ///
        /// TODO: Necessary to differentiate applications and user?
        /// </summary>
        bool IsAuthenticCredential { get; }

        /// <summary>
        /// Validate a Application Token
        /// </summary>
        /// <param name="applicationToken">Application Token</param>
        /// <param name="context">HTTP context</param>
        /// <returns>True if valid</returns>
        bool ValidateApplicationToken(string applicationToken, HttpContext context);
    }
}
