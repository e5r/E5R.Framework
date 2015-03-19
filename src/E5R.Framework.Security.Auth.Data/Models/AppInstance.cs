// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System.Text;

namespace E5R.Framework.Security.Auth.Data.Models
{
    using static System.StringComparison;

    public class AppInstance : DataModel<AppInstance, AuthId, App>
    {
        public App App { get; set; }
        public string Host { get; set; }

        public bool IsOriginalSeal(string seal)
        {
            var originalSeal = Id<AlgorithmSHA1, UnicodeEncoding>
                .GenerateHash($"{App.Id}:{App.PrivateKey}:{Host}");

            return string.Equals(seal, originalSeal, OrdinalIgnoreCase);
        }

        public AppInstance(){}

        public AppInstance(App app)
        {
            Id = new AuthId();
            App = app;
        }
    }
}
