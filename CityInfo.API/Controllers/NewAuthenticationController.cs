using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/new_authentication")]
    public class NewAuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public class NewAuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        public NewAuthenticationController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(NewAuthenticationRequestBody request)
        {
            var user = _userRepository.ValidateUserAsync(request.UserName, request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // Step 2: create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            // The claims that
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("user_id", user.Result.Id.ToString()));
            claimsForToken.Add(new Claim("organization_id", user.Result.OrganizationId.ToString()));
            claimsForToken.Add(new Claim("sub", user.Result.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.Result.FirstName));
            claimsForToken.Add(new Claim("family_name", user.Result.LastName));
            claimsForToken.Add(new Claim("city", user.Result.City));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(100),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }
    }
}
