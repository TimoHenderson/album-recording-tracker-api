using System;
using Microsoft.AspNetCore.Identity;
using RecordingTrackerApi.Models.Users.DTOs;

namespace RecordingTrackerApi.Services
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(RegisterDTO registerVM);
        Task<AuthResultDTO?> LoginUser(LoginDTO loginVM);
        Task<AuthResultDTO?> RefreshToken(TokenRequestDTO tokenRequestVM);
        Task<bool> IsEmailAlreadyRegistered(String email);
    }
}

