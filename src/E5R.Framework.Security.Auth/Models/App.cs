// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using System;

namespace E5R.Framework.Security.Auth.Models
{
    public class App : DataModel<App>
    {
        public AuthId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AuthToken PrivateKey { get; set; }

        public static App Create()
        {
            return new App()
            {
                Id = new AuthId(),
                PrivateKey = new AuthToken()
            };
        }
    }
}
