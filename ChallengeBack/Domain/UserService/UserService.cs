using Domain.DTO;
using Domain.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Domain.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public Task SaveUser(UserSaveDto user)
        {
            if (!IsValidEmail(user.Email))
                throw new ArgumentException("Format email not valid");

            if(IsMinior(user.DateOfBirth))
                throw new ArgumentException("User must be at least 18 years old");

            // Hash password before saving
            user.Password = HashPassword(user.Password);

            if (user.UserId > 0)
            { 
                return _userRepository.UpdateUser(user);
            }
            else
            {
                return _userRepository.SaveUser(user);
            }
        }

        private string HashPassword(string plainPassword)
        {
            var hasher = new PasswordHasher<UserSaveDto>();
            // You can pass null or the user object if you want to use it for context
            return hasher.HashPassword(null, plainPassword);
        }

        private bool IsMinior(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age < 18;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
