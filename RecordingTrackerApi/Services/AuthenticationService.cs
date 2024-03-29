﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Data.Helpers;
using RecordingTrackerApi.Models.Users;
using RecordingTrackerApi.Models.Users.DTOs;

namespace RecordingTrackerApi.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UsersDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            UsersDbContext context,
             IConfiguration configuration,
             TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthResultDTO?> LoginUser(LoginDTO loginDTO)
        {
            var userExists = await _userManager.FindByEmailAsync(loginDTO.EmailAddress);
            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, loginDTO.Password))
            {
                var tokenValue = await GenerateJWTTokenAsync(userExists, null);

                return tokenValue;
            }
            return null;
        }

        public async Task<AuthResultDTO> RefreshToken(TokenRequestDTO tokenRequestDTO)
        {
            return await VerifyAndGenerateToken(tokenRequestDTO);
        }

        public async Task<IdentityResult> RegisterUser(RegisterDTO registerDTO)
        {
            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.EmailAddress,
                UserName = registerDTO.UserName,
                Custom = "Egg",

                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                switch (registerDTO.Role)
                {
                    case UserRoles.Engineer:
                        await _userManager.AddToRoleAsync(newUser, UserRoles.Engineer);
                        break;
                    case UserRoles.Admin:
                        await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
                        break;
                    default:
                        break;
                }

            }
            return result;
        }


        private async Task<AuthResultDTO> VerifyAndGenerateToken(TokenRequestDTO tokenRequestDTO)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token ==
                tokenRequestDTO.RefreshToken);
            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
            try
            {
                var tokenCheckResult = jwtTokenHandler.ValidateToken(tokenRequestDTO.Token,
                    _tokenValidationParameters, out var validatedToken);

                return await GenerateJWTTokenAsync(dbUser, storedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if (storedToken.DateExpire >= DateTime.UtcNow)
                {
                    return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
        }

        private async Task<AuthResultDTO> GenerateJWTTokenAsync(ApplicationUser user, RefreshToken rToken)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.Sub, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Add User Role Claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }


            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(60),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            if (rToken != null)
            {
                var rTokenResponse = new AuthResultDTO()
                {
                    Token = jwtToken,
                    RefreshToken = rToken.Token,
                    ExpiresAt = token.ValidTo

                };
                return rTokenResponse;
            }
            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();


            var response = new AuthResultDTO()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo

            };

            return response;

        }

        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            return (await _userManager.FindByEmailAsync(email) != null);
        }
    }
}

