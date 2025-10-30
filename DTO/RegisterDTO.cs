
using System;

namespace StudTeacher.DTO
{
    public class RegisterDto
    {
        public string Name { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Designation { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
