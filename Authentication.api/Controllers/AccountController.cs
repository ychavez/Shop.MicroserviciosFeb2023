using Authentication.api.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpPost("Roles")]
        public async Task<ActionResult> CreateRole(string role)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            return NoContent();
        }

        [HttpPost("AddUserToRol")]
        public async Task<ActionResult> CreateRole(string role, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            await userManager.AddToRoleAsync(user, role);
            return NoContent();
        }

        private async Task<string> GetToken(IdentityUser user)
        {
            var now = DateTime.UtcNow;
            var key = configuration.GetValue<string>("Identity:Key");


            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub,user.UserName!),
              new Claim(JwtRegisteredClaimNames.Jti,user.Id),
              new Claim(JwtRegisteredClaimNames.Iat,now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
              new Claim(JwtRegisteredClaimNames.Email,user.Email!)
            };
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var encodedJWT = new JwtSecurityTokenHandler();

            var token = encodedJWT.CreateToken(tokenDescriptor);

            return encodedJWT.WriteToken(token);

        }

        private async Task<bool> UserExists(string userName)
            => await userManager.Users.AnyAsync(u => u.UserName == userName);


        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO) 
        {
            if (!await UserExists(loginDTO.UserName.ToLower()))
                return Unauthorized();

            var user = await userManager.Users.SingleAsync(x =>
              loginDTO.UserName.ToLower() == x.UserName!.ToLower());

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            return Ok(new UserDTO { UserName = user.UserName!, Token = await GetToken(user) });
        
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO) 
        {
            if (await UserExists(registerDTO.UserName.ToLower()))
                return BadRequest("User already exists");

            var user = new IdentityUser
            {
                UserName = registerDTO.UserName.ToLower(),
                Email = registerDTO.Email
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new UserDTO { UserName = user.UserName!, Token = await GetToken(user) });

        }
    }
}
