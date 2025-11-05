using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionService _permissionService;
        public PermissionService(
            IUserRepository userRepository,
            IPermissionService permissionService
            )
        {
            _userRepository = userRepository;
            _permissionService = permissionService;
        }

        public Task<bool> ValidatePermission(string userEmail, string permissionName)
        {
            var user = _userRepository.GetUserByEmail(userEmail);
            if (user == null) return Task.FromResult(false);

            return _permissionService.ValidatePermission(userEmail, permissionName);
        }
    }
}
