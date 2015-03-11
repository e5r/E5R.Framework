// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;

namespace E5R.Framework.Security.Auth.Models
{
    public class AppInstance : DataModel<AppInstance, App>
    {
        public AuthId Id { get; set; }
        public App App { get; set; }
        public string Host { get; set; }

        public AppInstance(){}

        public AppInstance(App app)
        {
            Id = new AuthId();
            App = app;
        }
    }
}
