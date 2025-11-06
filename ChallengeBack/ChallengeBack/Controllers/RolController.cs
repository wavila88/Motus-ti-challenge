using ChallengeBack.Model;
using Domain.DTO;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using RepositorySQL.Queries;

namespace ChallengeBack.Controllers
{
    [ApiController]
    //[TokenAuth]
    [Route("[controller]")]
    public class RolController : ControllerBase
    {
        private readonly ILogger<RolController> _logger;
        private readonly IRolRepository _rolRepository;

        public RolController(
            ILogger<RolController> logger,
            IRolRepository rolRepository
            )
        {
            _logger = logger;
            _rolRepository = rolRepository;
        }

        /// <summary>
        /// Get roles list based on access level.
        /// </summary>
        /// <returns>Returns a list of users on success, or an error message on failure.</returns>
        [HttpGet("{levelAcces}")]
        [ProducesResponseType(typeof(ApiResponseDTO<List<RolDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDTO<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles(int levelAcces)
        {
            try
            {
                List<RolDto> list = await _rolRepository.GetRolList(levelAcces);

                return Ok(new ApiResponseDTO<List<RolDto>>(true, "Success", list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(500, new ApiResponseDTO<object>(false, "An unexpected error occurred."));
            }
        }

    }
}
