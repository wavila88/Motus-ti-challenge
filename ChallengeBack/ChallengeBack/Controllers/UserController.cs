using ChallengeBack.Model;
using Domain.DTO;
using Domain.Repository;
using Domain.UserService;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TokenAuth]
    public class UserController : ControllerBase
    {
      
        private readonly ILogger<UserController> _logger;

        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
   

        public UserController(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            IUserService userService
            )
        {
            _logger = logger;
            _userRepository = userRepository;
            _userService = userService;
        }


        /// <summary>
        /// Gets the list of users.
        /// </summary>
        /// <returns>Returns a list of users on success, or an error message on failure.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDTO<List<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserList()
        {
            try
            {
               List<UserDto> list= await _userRepository.GetUserList();

                return Ok(new ApiResponseDTO<List<UserDto>>(true, "Success", list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(500, new ApiResponseDTO<object>(false, "An unexpected error occurred."));
            }
        }

        /// <summary>
        /// Creates or updates a user.
        /// </summary>
        /// <param name="user">User data to save.</param>
        /// <returns>Returns success message or validation error.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Save(UserSaveDto user)
        {
            IActionResult response;
            try
            {
                await _userService.SaveUser(user);
                if (user.UserId > 0)
                {
                    response = Ok(new ApiResponseDTO<object>(true, "User Updated!"));
                }
                else 
                { 
                    response = Ok(new ApiResponseDTO<object>(true, "User Created!"));
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.ToString());
                return BadRequest(new ApiResponseDTO<object>(false, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                response = StatusCode(500, new ApiResponseDTO<object>(false, "An unexpected error occurred."));
            }
            return response;
        }


        

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="userId">User ID to delete.</param>
        /// <returns>Returns success message or error.</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                await _userRepository.DeleteUser(userId);

                return Ok(new ApiResponseDTO<object>(true, "User Removed!"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(500, new ApiResponseDTO<object>(false, "An unexpected error occurred."));
            }
        }
    }
}
