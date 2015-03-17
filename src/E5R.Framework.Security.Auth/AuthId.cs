// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using System;
using System.Text;
using System.Security.Cryptography;

namespace E5R.Framework.Security.Auth
{
    // TODO: Move to E5R.Framework.Security.Auth.[Data|Common|Core]
    public class AuthId : Id<AlgorithmMD5, UnicodeEncoding, IdSize32>
    {
        public AuthId() : base() { }
        public AuthId(string value) : base(value) { }
    }
}
