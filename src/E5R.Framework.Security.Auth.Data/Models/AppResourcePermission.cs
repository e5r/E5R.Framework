// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System.Collections.Generic;

namespace E5R.Framework.Security.Auth.Data.Models
{
    public class AppResourcePermission : DataModel<AppResourcePermission, AuthId>
    {
        App App { get; set; }
        Resource Resource { get; set; }
        IEnumerable<string> Verbs { get; set; }
    }
}