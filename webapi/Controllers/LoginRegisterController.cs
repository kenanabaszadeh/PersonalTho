using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using webapi;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegister request)
    {
        try
        {
            // Check if the username or email is already taken
            if (await _context.UserRegister.AnyAsync(u => u.Username == request.Username || u.Email == request.Email))
            {
                return BadRequest("Username or email already in use.");
            }

            // Hash the password
            string hashedPassword = HashPassword(request.Password);

            // Create a new user
            var user = new UserRegister
            {
                Id = new Guid(),
                Username = request.Username,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = hashedPassword,
                Age = request.Age,

            };

            // Add the user to the database
            _context.UserRegister.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }
        catch (Exception ex)
        {
            // Log the error
            return StatusCode(500, ex);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin request)
    {
        try
        {
            // Find the user by username or email
            var user = await _context.UserRegister.FirstOrDefaultAsync(u => u.Email == request.Email || u.Email == request.Email);

            // Check if the user exists
            if (user == null)
            {
                return BadRequest("Invalid username or password.");
            }

            // Verify the password
            if (!VerifyPassword(request.Password, user.Password))
            {
                return BadRequest("Invalid username or password.");
            }

            // TODO: Generate and return a JWT token for authentication

            return Ok("Login successful.");
        }
        catch (Exception ex)
        {
            // Log the error
            return StatusCode(500, "An error occurred while logging in.");
        }
    }

    // Utility method to hash the password
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    // Utility method to verify the password
    private bool VerifyPassword(string password, string hashedPassword)
    {
        return string.Equals(hashedPassword, HashPassword(password));
    }
}
