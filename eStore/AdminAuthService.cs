namespace eStore
{
    public class AdminAuthService
    {
        public bool IsLoggedIn { get; private set; }

        public bool Login(string email, string password, AdminAccount account)
        {
            var trimmedEmail = email.Trim();
            var trimmedPassword = password.Trim();
            if (string.Equals(trimmedEmail, account.Email, System.StringComparison.OrdinalIgnoreCase)
                && trimmedPassword == account.Password)
            {
                IsLoggedIn = true;
                return true;
            }

            return false;
        }

        public void SignOut()
        {
            IsLoggedIn = false;
        }
    }
}
