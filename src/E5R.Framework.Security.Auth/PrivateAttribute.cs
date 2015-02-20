namespace E5R.Framework.Security.Auth
{
    public class PrivateAttribute: ProtectionAttribute
    {
        public PrivateAttribute (string[] requiredPermissions)
            : base(ProtectionLevel.Private, requiredPermissions)
        {
        }
    }
}