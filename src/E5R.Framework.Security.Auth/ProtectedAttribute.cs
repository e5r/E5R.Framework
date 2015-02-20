namespace E5R.Framework.Security.Auth
{
    public class ProtectedAttribute: ProtectionAttribute
    {
        public ProtectedAttribute ()
            : base(ProtectionLevel.Protected)
        {
        }
    }
}