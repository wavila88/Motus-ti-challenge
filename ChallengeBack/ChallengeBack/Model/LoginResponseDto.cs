using Microsoft.AspNetCore.Authentication;

namespace ChallengeBack.Model
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string UserName { get; set; }
        public string email { get; set; }
        public string RolName { get; set; }
        public int LevelAccess { get; set; }

    }
}
