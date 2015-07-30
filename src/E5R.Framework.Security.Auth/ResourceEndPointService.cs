// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.ResourceEndPoint;
using System;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth
{
    public class ResourceEndPointService : IResourceEndPointService
    {
        private readonly IEnumerable<IResourceEndPoint> _resourceEndPointList = new IResourceEndPoint[]
        {
            new AppInternalStorageEndPoint()
        };

        IResourceEndPoint IResourceEndPointService.Find(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}