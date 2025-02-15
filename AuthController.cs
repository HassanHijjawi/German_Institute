using GermanInstitute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly MyDbContext _context;

    public AuthController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDTO userDto)
    {
        if (_context.Users.Any(u => u.Email == userDto.Email))
            return BadRequest("Email already registered.");
        if (!IsValidEmail(userDto.Email))
        {
            return BadRequest("Invalid email");

        }
        User user = new User();
        user.PasswordHash = HashPassword(userDto.Password);
        user.Email = userDto.Email;
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok("User registered.");
    }
    private bool IsValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDTO userDto)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
        if (existingUser == null || !existingUser.PasswordHash.Equals(HashPassword(userDto.Password)))
            return Unauthorized("Invalid email or password.");

        var token = GenerateJwtToken(existingUser);
        return Ok(new { Token = token , Id = existingUser.Id});
    }

    private string HashPassword(string password)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.UTF8.GetBytes("RANDOM_SALT_98765"),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[] { new Claim(ClaimTypes.Email, user.Email) };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RANDOM_SECRET_KEY_123453534523r3"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken("random_issuer_6789022535345623r345", "random_issuer_678903453453423r535345", claims, expires: DateTime.Now.AddHours(1), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}