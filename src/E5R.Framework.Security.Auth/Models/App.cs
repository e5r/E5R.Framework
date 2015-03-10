using System;

namespace E5R.Framework.Security.Auth.Models
{
    public class App
    {
        public AuthId Id { get; set; }
        public string Name { get; set; }
        public AuthToken PrivateKey { get; set; }
    }
}
