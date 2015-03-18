// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth
{
    // TODO: Refactory
    public interface IAuthorizationService
    {
        bool AllowUnsignedAction { get; set; }

        bool HasRequiredPermissions(string[] requiredPermissions);
    }
}
