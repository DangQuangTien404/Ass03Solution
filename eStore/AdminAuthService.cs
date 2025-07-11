using DataAccess.Repositories;

namespace eStore
{
    public class AdminAuthService
    {
        private readonly IMemberRepository _memberRepository;

        public AdminAuthService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public bool IsLoggedIn { get; private set; }
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public bool Login(string email, string password, AdminAccount account)
        {
            var trimmedEmail = email.Trim();
            var trimmedPassword = password.Trim();

            if (string.Equals(trimmedEmail, account.Email, System.StringComparison.OrdinalIgnoreCase)
                && trimmedPassword == account.Password)
            {
                IsLoggedIn = true;
                NotifyStateChanged();
                return true;
            }

            var member = _memberRepository.GetByEmailAndPassword(trimmedEmail, trimmedPassword);
            if (member != null)
            {
                IsLoggedIn = true;
                NotifyStateChanged();
                return true;
            }

            return false;
        }

        public void SignOut()
        {
            IsLoggedIn = false;
            NotifyStateChanged();
        }
    }
}
