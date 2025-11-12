using api.Data.Models;
using api.Dto.Auth;
using api.Helpers;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly IUser _user;
        private readonly IMail _mail;
        private readonly IConfiguration _config;
        private readonly IJsonLocalizer L;
        public AuthController(IUser user, IAuth auth, IMail mail, IConfiguration config, IJsonLocalizer l)
        {
            _user = user;
            _auth = auth;
            _mail = mail;
            _config = config;
            L = l;
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Login([FromBody] Dto.Auth.LoginRequest model)
        {
            if (model.Email == null || model.Password == null)
                return BadRequest(L["EmailAndPasswordNecessary"]);

            IConfigurationSection JWT = _config.GetSection("Jwt");
            Console.WriteLine(JWT["EmailKey"]);
            Console.WriteLine(model.Email);
            string encryptedEmail = AuthHelper.EncryptString(JWT["EmailKey"]!, model.Email);
            User? user = await _user.GetUserByEmail(encryptedEmail);

            if (user == null || user.Id == null)
                return Unauthorized(L["InvalidCredentials"]);

            string computedHash = AuthHelper.HashPassword(model.Password, user.Salt!, JWT["PasswordPepper"]!);

            if (!AuthHelper.VerifyPassword(user.Password, computedHash))
                return Unauthorized(L["InvalidCredentials"]);

            if (!(user.IsEmailValidated ?? false))
                return Unauthorized(L["EmailNotVerified"]);

            user.AccessToken = await _auth.GenerateToken(user.Id.Value);
            user.RefreshToken = await _auth.GenerateRefreshToken(user.Id.Value);

            return Ok(new AuthResponse
            {
                Id = user.Id.Value,
                FirstName = AuthHelper.DecryptString(JWT["NameKey"]!, user.FirstName!),
                LastName = AuthHelper.DecryptString(JWT["SurnameKey"]!, user.LastName!),
                Email = AuthHelper.DecryptString(JWT["EmailKey"]!, user.Email!),
                AccessToken = user.AccessToken!,
                RefreshToken = user.RefreshToken!,
                IsEmailValidated = user.IsEmailValidated ?? false,
                CreatedAt = user.CreatedAt
            });

        }


        [AllowAnonymous]
        [HttpGet("RefreshToken/{RefreshToken}/{UserId}")]
        public async Task<IActionResult> RefreshToken([FromRoute] string RefreshToken, [FromRoute] Guid UserId)
        {
            string? refreshedToken = await _auth.RefreshAccessToken(RefreshToken, UserId);
            if (string.IsNullOrEmpty(refreshedToken))
            {
                return Forbid(L["InvalidRefreshToken"]);
            }
            return Ok(refreshedToken);
        }

        [HttpPost("Sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser([FromBody] SignUpDto user)
        {
            IConfigurationSection sectionJWT = _config.GetSection("Jwt");

            string userEmail = AuthHelper.EncryptString(sectionJWT["EmailKey"], user.Email);

            User? checkDuplicateUser = await _user.GetUserByEmail(userEmail);
            string lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            if (checkDuplicateUser != null)
            {
                return Conflict(L["EmailAlreadyUsed"]);
            }
            MailTemplateTranslation? mailInscription = await _mail.GetMailTemplate("ConfirmationInscription", lang);
            if (mailInscription == null)
            {
                return BadRequest(L["ServerError"]);
            }
            User newUser = new User();
            newUser.FirstName = AuthHelper.EncryptString(sectionJWT["NameKey"], user.FirstName);
            newUser.LastName = AuthHelper.EncryptString(sectionJWT["SurnameKey"], user.LastName);
            newUser.Email = userEmail;
            newUser.Salt = AuthHelper.GenerateSalt();
            newUser.Password = AuthHelper.HashPassword(user.Password, newUser.Salt, sectionJWT["PasswordPepper"]);
            newUser.RefRole = new Guid("74737d58-a69f-4df7-bf9a-777297a4d6d6");
            newUser = await _user.AddUser(newUser);
            await _mail.SendEmail(user.Email, mailInscription.Subject, mailInscription.HtmlBody
                .Replace("@@NAME", $"{user.FirstName} {user.LastName}")
                .Replace("@@LIEN_CONFIRMATION", $"{_config["FrontBaseUrl"]}/email-confirmation?email={user.Email}&token={newUser.TokenAccountCreated}")
                .Replace("@@APP_NAME", _config["APP_NAME"])
                .Replace("@@Year", DateTime.Now.ToString("yyyy"))
            );
            return CreatedAtAction(nameof(AddUser), new { id = newUser.Id }, newUser);
        }
    }
}
