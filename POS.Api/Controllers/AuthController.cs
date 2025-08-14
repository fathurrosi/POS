using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using POS.Domain.Entities;
using System.Security.Cryptography;

namespace POS.Api.Controllers
{
    [EnableCors("AllowSpecificMethods")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // This is a simplified example. In a real application, you would use a database
        // to store user credentials (hashed passwords and salts).
        private static Dictionary<string, (string hashedPassword, string salt)> _users = new Dictionary<string, (string, string)>
        {
            {"fathur", (HashPassword("fathur", out string salt), salt)} // Store hashed password and salt
                                                                                
        };

        // Helper method to hash a password with a randomly generated salt
        private static string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            salt = Convert.ToBase64String(saltBytes);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes for SHA256
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Helper method to verify a password against a stored hash and salt
        private static bool VerifyPassword(string password, string storedHashedPassword, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                string newHashedPassword = Convert.ToBase64String(hashBytes);
                return newHashedPassword == storedHashedPassword;
            }
        }

        [HttpPost("login")]
//        [HttpGet("{username}/{password}")]
        public IActionResult Login(User item)
        {
            if (_users.TryGetValue(item.Password, out var userData))
            {
                if (VerifyPassword(item.Password, userData.hashedPassword, userData.salt))
                {
                    // In a real application, generate and return an authentication token (e.g., JWT)
                    return Ok(new { Message = "Login successful!", Token = "your_auth_token_here" });
                }
            }
            return Unauthorized(new { Message = "Invalid username or password" });
        }
    }
}
