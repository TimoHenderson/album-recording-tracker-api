﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecordingTrackerApi.Models;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.Users.DTOs;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data.Helpers;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(AuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please provide all the required fields");


            if (await _service.IsEmailAlreadyRegistered(registerDTO.EmailAddress))
                return BadRequest("Email address is already registered!");

            var result = await _service.RegisterUser(registerDTO);

            if (result.Succeeded) return Ok(result);
            return BadRequest(result.ToString());
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            var loginResult = await _service.LoginUser(loginDTO);

            if (loginResult != null)
                return Ok(loginResult);

            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDTO tokenRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            var result = await _service.RefreshToken(tokenRequestDTO);
            return Ok(result);
        }


    }
}

