using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PermissionService
{
    public interface IPermissionService
    {
        Task<bool> ValidatePermission(string userEmail, string permissionName);
    }
}
