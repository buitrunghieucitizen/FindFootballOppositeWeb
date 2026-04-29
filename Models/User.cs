using System;
using System.Collections.Generic;

namespace FindFootballOppsite.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool IsFreeAgent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}