using System;

namespace Forum.Models
{
    public class User : BaseUser
    {
        public Role Role { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public string Signature { get; set; }
        public bool Banned { get; set; }
        public string BanReason { get; set; }
    }

    public class Login
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime LoginDate { get; set; }
        public bool Deleted { get; set; }
    }

    public class PasswordResetViewModel
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string Confirm { get; set; }
    }

    public class UserLoginViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}