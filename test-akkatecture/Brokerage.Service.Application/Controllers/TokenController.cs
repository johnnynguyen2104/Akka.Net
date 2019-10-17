using Akka.Actor;
using Brokerage.Service.Domain.Aggregates.ForexAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Controllers
{
    [Route("/token")]
    public class TokenController : ControllerBase
    {
        public class SignInResultViewModel
        {
            public string UserId { get; set; }

            public string AuthToken { get; set; }
        }

        [AllowAnonymous]
        [HttpPost(nameof(SignIn))]
        public IActionResult SignIn(string email, string password)
        {
            //ActorTests.TestForex(_actorSystem, _forexAccountRepository).Wait();

            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("email", "Email address cannot be empty.");
                return ValidationProblem(ModelState);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("password", "Password cannot be empty.");
                return ValidationProblem(ModelState);
            }

            var signInResult = new SignInResultViewModel
            {
                UserId = "12345678",
                AuthToken = GenerateJwtSecurityToken("12345678"),
            };

            return Ok(signInResult);
        }

        [Authorize]
        [HttpPost(nameof(ValidateToken))]
        public IActionResult ValidateToken()
        {
            var claim = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                ModelState.AddModelError("token", "Could not derive UserId from token.");
                return ValidationProblem(ModelState);
            }

            var userId = claim.Value;

            if (userId != "12345678")
            {
                ModelState.AddModelError("token", "UserId could not be derived from token.");
                return ValidationProblem(ModelState);
            }

            var signInResult = new SignInResultViewModel
            {
                UserId = userId,
                AuthToken = GenerateJwtSecurityToken(userId),
            };

            return Ok(signInResult);
        }

        private string GenerateJwtSecurityToken(string userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Iss, "Fraction"),
                new Claim(JwtRegisteredClaimNames.Aud, "Fraction"),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(30)).ToUnixTimeSeconds().ToString()),
            };

            var jwtPayload = new JwtPayload(claims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LocalOnlySuperSecretFractionSecurityKey2019"));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtHeader = new JwtHeader(signingCredentials);
            var jwtSecurityToken = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
