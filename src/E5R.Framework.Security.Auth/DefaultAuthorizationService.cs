﻿using Microsoft.AspNet.Mvc;
using System;
using System.Linq;

namespace E5R.Framework.Security.Auth
{
    public class DefaultAuthorizationService : IAuthorizationService
    {
        private readonly string[] _allPermissions = new[] {
            "permission1",
            "permission2",
            "permission3",
            "permission4",
            "permission5",
            "permission6"
        };

        bool IAuthorizationService.AllowUnsignedAction { get; set; } = false;

        bool IAuthorizationService.HasRequiredPermissions(string[] requiredPermissions)
        {
            // TODO: Implements Hash of Array for Cache

            foreach (var permission in requiredPermissions)
            {
                if (!_allPermissions.Contains(permission))
                {
                    return false;
                }
            }
            return true;
        }
    }
}