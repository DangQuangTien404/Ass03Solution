namespace eStore
{
    public class AdminAuthService
    {
        public bool IsLoggedIn { get; private set; }

        public bool Login(string email, string password, AdminAccount account)
        {
            if (string.Equals(email.Trim(), account.Email, System.StringComparison.OrdinalIgnoreCase)
                && password == account.Password)
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
