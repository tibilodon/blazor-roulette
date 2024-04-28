using blazor_with_auth.Shared.Interfaces;
using blazor_with_auth.Shared.Models;
using Microsoft.JSInterop;
using System.Web;

namespace blazor_with_auth.Shared.Services
{
    public class CookieService : ICookieService
    {
        private readonly IJSRuntime _jSRuntime;
        string expires = "";

        //  bring in JSRuntime
        public CookieService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
            ExpireDays = 1;
        }

        public int ExpireDays
        {
            set => expires = DateToUTC(value);
        }

        //  set (optional) expiry day
        private static string DateToUTC(int days) => DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");

        //  set cookie with jsruntime
        private async Task SetCookie(string value)
        {
            await _jSRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
        }

        //  get cookie with jsruntime
        private async Task<string> GetCookie()
        {
            return await _jSRuntime.InvokeAsync<string>("eval", $"document.cookie");
        }
        public async Task<UnRegisteredAppUser> SetValue(string name, int? days)
        {
            var isUser = await GetUnRegisteredAppUser();
            //  "update"
            if (isUser != null)
            {
                string encodeCurName = $"user_{HttpUtility.UrlEncode(isUser.Name)}";
                //  delete cookie
                await SetCookie($"{encodeCurName}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/");
                var curExp = (days != null) ? (days > 0 ? DateToUTC(days.Value) : "") : expires;

                //  set new cookie
                string encodeKeyName = $"user_{HttpUtility.UrlEncode(name)}";
                await SetCookie($"{encodeKeyName}={isUser.Id}; expires={curExp}; path=/");
                return new UnRegisteredAppUser
                {
                    Id = isUser.Id,
                    Name = name,
                };
            }
            else
            {
                var curExp = (days != null) ? (days > 0 ? DateToUTC(days.Value) : "") : expires;
                var userId = Guid.NewGuid().ToString();
                string encodeKeyName = $"user_{HttpUtility.UrlEncode(name)}";
                await SetCookie($"{encodeKeyName}={userId}; expires={curExp}; path=/");
                return new UnRegisteredAppUser
                {
                    Id = userId,
                    Name = name,
                };
            }
        }

        public async Task<UnRegisteredAppUser> GetUnRegisteredAppUser()
        {
            var cValue = await GetCookie();

            if (string.IsNullOrEmpty(cValue))
            {
                return null;
            }

            var vals = cValue.Split(";");
            foreach (var val in vals)
            {
                if (!string.IsNullOrEmpty(val) && val.IndexOf("=") > 0)
                {
                    var splitIndex = val.IndexOf("=");
                    var key = val.Substring(0, splitIndex).Trim();
                    //  get user
                    if (key.StartsWith("user_", StringComparison.OrdinalIgnoreCase))
                    {
                        var value = val.Substring(splitIndex + 1);
                        var name = key.Replace("user_", "");

                        return new UnRegisteredAppUser
                        {
                            Name = HttpUtility.UrlDecode(name),
                            Id = value
                        };
                    }
                }
            }
            return null;
        }

    }
}
