// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Threading.Tasks;

namespace E5R.Framework.Security.Auth.ResourceEndPoint
{
    public class AppInternalStorageEndPoint : IResourceEndPoint
    {
        string IResourceEndPoint.EndPoint { get; } = "/internal/data/app";

        Task<object> IResourceEndPoint.ExecAsync(string fragmentUri)
        {
            throw new NotImplementedException();
        }
    }
}