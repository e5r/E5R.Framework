// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;

namespace E5R.Framework.Security.Auth
{
    public interface IResourceEndPointService
    {
        IResourceEndPoint Find(Uri uri);
    }
}