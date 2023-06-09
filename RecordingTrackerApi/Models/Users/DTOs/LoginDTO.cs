using System;
using System.ComponentModel.DataAnnotations;
namespace RecordingTrackerApi.Models.Users.DTOs
{
    public class LoginDTO
    {

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }


    }
}

