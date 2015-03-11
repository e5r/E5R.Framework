// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth
{
    public enum RequestFluxType
    {
        RequestAccessToken = 0,
        ConfirmTokenNonce,
        ResourceRequest,
        BadRequest
    }
}
