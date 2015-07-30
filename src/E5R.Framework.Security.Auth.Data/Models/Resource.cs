// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth.Data.Models
{
    public class Resource : DataModel<Resource, AuthId>
    {
        string Name { get; set; }
        string Description { get; set; }
        string EndPoint { get; set; }
    }
}