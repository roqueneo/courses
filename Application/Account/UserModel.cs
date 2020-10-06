namespace Application.Account
{
    public class UserModel
    {
        public UserModel()
        { }

        public UserModel(string fullName, string userName, string email)
        {
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