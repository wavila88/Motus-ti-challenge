using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserService
{
    public interface IUserService
    {
        public Task SaveUser(UserSaveDto user); 


    }
}
