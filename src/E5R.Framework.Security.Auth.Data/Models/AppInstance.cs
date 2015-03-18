// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

namespace E5R.Framework.Security.Auth.Data.Models
{
    public class AppInstance : DataModel<AppInstance, AuthId, App>
    {
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
