namespace CodePulse.API.Models.Domain
{
    public static class IdentityDefaults
    {
        public const string ReaderRole = "Reader";
        public const string WriterRole = "Writer";

        public const string AdminEmail = "admin@codepulse.com";
        public const string AdminPassword = "Admin@123";

        // Fixed GUIDs for consistent seeding across environments
        public const string ReaderRoleId = "A1A1A1A1-B2B2-C3C3-D4D4-E5E5E5E5E5E5";
        public const string WriterRoleId = "B1B1B1B1-C2C2-D3D3-E4E4-F5F5F5F5F5F5";
        public const string AdminUserId = "C1C1C1C1-D2D2-E3E3-F4F4-A5A5A5A5A5A5";
    }
}
