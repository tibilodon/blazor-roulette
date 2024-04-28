using blazor_with_auth.Shared.Models;

namespace blazor_with_auth.Shared.Interfaces
{
    public interface ICookieService
    {
        public Task<UnRegisteredAppUser> SetValue(string name, int? day = 1);
        public Task<UnRegisteredAppUser> GetUnRegisteredAppUser();
    }
}
