// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth
{
    public class PrivateAttribute: ProtectionAttribute
    {
        public PrivateAttribute (string[] requiredPermissions)
            : base(ProtectionLevel.Private, requiredPermissions)
        {
        }
    }
}
