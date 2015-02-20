namespace E5R.Framework.Security.Auth
{
    public class PublicAttribute: ProtectionAttribute
    {
        public PublicAttribute ()
            : base(ProtectionLevel.Public)
        {
        }
    }
}