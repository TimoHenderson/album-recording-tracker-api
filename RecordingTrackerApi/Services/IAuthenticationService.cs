using System;
using Microsoft.AspNetCore.Identity;
using RecordingTrackerApi.Models.ViewModels;

namespace RecordingTrackerApi.Services
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(RegisterVM registerVM);
        Task<AuthResultVM> LoginUser(LoginVM loginVM);
        Task<AuthResultVM> RefreshToken(TokenRequestVM tokenRequestVM);
        Task<bool> IsEmailAlreadyRegistered(String email);
    }
}

