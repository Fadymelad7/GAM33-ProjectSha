using GAM33.Dtos;
using GAM33.Exceptions;
using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Interfaces.IdentityServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GAM33.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region constructor 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToken _token;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IToken token, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
            this._configuration = configuration;
        }
        #endregion

        #region Login EndPoint

        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) return BadRequest("Invalid Login");

            var PasswordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (!PasswordCheck.Succeeded) return BadRequest("Invalid Login");

            var returnedValue = new UserDto()
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await _token.GetTokenAsync(user),
            };
            return Ok(returnedValue);

        }
        #endregion

        #region Register EndPoint

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    DisplayName = model.DisplayName,
                    UserName = model.Email.Split('@')[0],
                    PhoneNumber = model.PhoneNumber,

                };

                var CreateUser = await _userManager.CreateAsync(user, model.Password);

                if (!CreateUser.Succeeded) throw new ValidationException(CreateUser.Errors.Select(E => E.Description));

                var returnedValue = new UserDto()
                {

                    DisplayName = model.DisplayName,
                    Email = model.Email,
                    Token = await _token.GetTokenAsync(user)
                };

                return Ok(returnedValue);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "An error occurred during registration", Details = ex.Message });
            }


        }
        #endregion

        #region Forgot Password EndPoint
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user == null) return BadRequest("Invalid Data");

            var PasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (string.IsNullOrEmpty(PasswordToken)) return BadRequest("Invalid Data");

            var callbackUrl = $"{_configuration["BaseApiUrl"]}/Accounts/ResetPassword?code={PasswordToken}&email={user.Email}";

            return Ok(new
            {

                email = user.Email,

                token = PasswordToken


            });


        }
        #endregion

        #region Reset Password EndPoint

        [HttpPost("ResetPassword")]

        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return BadRequest("Invalid Data");

            var Result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

            if (Result.Succeeded) return Ok("Password Reset Successful");
            return BadRequest("Invalid Data");
        } 
        #endregion



    }
}
