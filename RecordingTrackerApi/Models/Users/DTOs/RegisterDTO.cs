using System;
using System.ComponentModel.DataAnnotations;
namespace RecordingTrackerApi.Models.Users.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }

        public override string ToString()
        {
            return $"name: {FirstName} {LastName} email:{EmailAddress} user:{UserName} pword: {Password}";
        }
    }
}

