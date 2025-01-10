namespace JwtAuth.Entities
{
    public class User
    {
        public string UserName{ get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
    }
}
