using System;
using System.ComponentModel.DataAnnotations;

namespace RecordingTrackerApi.Models.Users.DTOs
{
    public class TokenRequestDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}

