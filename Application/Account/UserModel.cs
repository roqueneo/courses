namespace Application.Account
{
    public class UserModel
    {
        public UserModel()
        { }

        public UserModel(string token, string fullName, string userName, string email)
        {
            Token = token;
            FullName = fullName;
            UserName = userName;
            Email = email;
        }

        public string Token { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }
    }
}