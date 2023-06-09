using System;
using Microsoft.AspNetCore.Identity;
namespace RecordingTrackerApi.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Custom { get; set; }
    }
}


