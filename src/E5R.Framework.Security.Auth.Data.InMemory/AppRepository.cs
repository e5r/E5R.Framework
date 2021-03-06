﻿// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.Data.Models;
using Microsoft.Framework.Logging;

namespace E5R.Framework.Security.Auth.Data.InMemory
{
    public class AppRepository : InMemoryRepository<App>, IDataStorage<App>
    {
        public AppRepository(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        { }
    }
}