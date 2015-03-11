// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using static E5R.Framework.Security.Auth.Constants;
using static E5R.Framework.Security.Auth.RequestFluxType;
using static System.StringComparison;

namespace E5R.Framework.Security.Auth
{
    public class HttpAuthUtils
    {
        private static readonly Dictionary<RequestFluxType, IList<string>> _httpAuthFluxHeaders
        = new Dictionary<RequestFluxType, IList<string>>() {
            { RequestAccessToken, new List<string>() {
                HttpAuthAppInstanceIdHeader,
                HttpAuthSealHeader } },
            { ConfirmTokenNonce, new List<string>() {
                HttpAuthAppInstanceIdHeader,
                HttpAuthAccessTokenHeader,
                HttpAuthCNonceHeader } },
            { ResourceRequest, new List<string>() {
                HttpAuthAppInstanceIdHeader,
                HttpAuthSealedAccessTokenHeader,
                HttpAuthCNonceHeader } } };

        public static RequestFluxType GetRequestFluxType(HttpContext context)
        {
            foreach(var pair in _httpAuthFluxHeaders){
                var founds = pair.Value
                    .Where(where => context.Request.Headers.Keys.Contains(where));

                if(pair.Value.Count() == founds.Count()){
                    var others = context.Request.Headers.Keys.Where(where => !founds.Contains(where));
                    if(others.Count(x => x.StartsWith(HttpAuthPrefixHeader, OrdinalIgnoreCase)) > 0)
                        return BadRequest;
                    return pair.Key;
                }
            }

            return BadRequest;
        }
    }
}
