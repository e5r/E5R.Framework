// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System.Threading.Tasks;

namespace E5R.Framework.Security.Auth
{
    public interface IResourceEndPoint
    {
        string EndPoint { get; }
        Task<object> ExecAsync(string fragmentUri);
    }
}