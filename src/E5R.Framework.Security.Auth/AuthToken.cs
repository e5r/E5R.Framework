// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace E5R.Framework.Security.Auth
{
    // TODO: Move to E5R.Framework.Security.Auth.[Data|Common|Core]
    public class AuthToken : Id<AlgorithmSHA384, UnicodeEncoding, IdSize96>
    {
        public AuthToken() : base() {}
        public AuthToken(string value) : base(value) {}
    }
}
