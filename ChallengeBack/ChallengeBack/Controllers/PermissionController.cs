using ChallengeBack.AuthManagement;
using ChallengeBack.Model;
using Domain.DTO;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositorySQL.DBContext;
using RepositorySQL.Queries;
using System.Security.Claims;

namespace ChallengeBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {

        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly string _secretKey;

        public PermissionController(
            ILogger<PermissionController> logger,
            IPermissionRepository permissionRepository,
            IUserRepository userRepository,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
            _secretKey = configuration["Jwt:Secret"];
        }

        [HttpPost("validate")]
        [TokenAuth] // Requires authentication
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ValidatePermission([FromBody] PermissionRequest permission)
        {
            var isValid = await _permissionRepository.UserHasPermission(permission.Email, permission.Permission);
            if (isValid)
            {
                return Ok(new ApiResponseDTO<bool>(true, null, true));
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponseDTO<string>(false, "Permission denied.", "Permission denied."));
            }
        }


        [HttpPost("login")]
        [AllowAnonymous] // Permite acceso sin token
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(login.Email);
                if (user == null)
                    return Unauthorized("Username or password is incorrect.");

                var hasher = new PasswordHasher<UserDto>();

                var result = hasher.VerifyHashedPassword(user, user.Passsword, login.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    var token = JWTGenerator.GenerateJwtToken(user, _secretKey);

                    var response = new LoginResponseDto() {
                        Token = token,
                        UserName = $"{user.FirstName} {user.LastName}",
                        LevelAccess = user.Role.Level,
                        RolName = user.Role.Name,
                        email  = user.Email
                    };

                    //using proxy in front que can use httponly
                    //Response.Cookies.Append("jwt_token", token, new CookieOptions
                    //{
                    //    HttpOnly = true,
                    //    Secure = true, // Solo se envía por HTTPS
                    //    SameSite = SameSiteMode.Strict,
                    //    Expires = DateTimeOffset.UtcNow.AddHours(2)
                    //});

                    return Ok(new ApiResponseDTO<LoginResponseDto>(true,"user logged",response));
                }
                else
                {
                    return Unauthorized(new ApiResponseDTO<bool>(false, "user is not authorized"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(500, new ApiResponseDTO<object>(false, "An unexpected error occurred."));
            }
        }
    }
}