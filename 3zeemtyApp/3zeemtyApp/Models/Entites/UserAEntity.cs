namespace ProductApi.Models.Entites
{
    public class UserAEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
        public bool IsAdmin { get; set; }

        private UserAEntity()
        {

        }
        public static UserAEntity Create(string username, string password, bool isAdmin)
        {
            return new UserAEntity
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                IsAdmin = isAdmin,
            };
        }
    }
}
