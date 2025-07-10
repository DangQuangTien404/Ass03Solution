namespace eStore
{
    public class AdminAuthService
    {
        public bool IsLoggedIn { get; private set; }

        public void SignIn()
        {
            IsLoggedIn = true;
        }

        public void SignOut()
        {
            IsLoggedIn = false;
        }
    }
}
