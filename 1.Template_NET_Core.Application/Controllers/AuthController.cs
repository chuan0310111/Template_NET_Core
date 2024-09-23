using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _1.Template_NET_Core.Application.Controllers
{
    /// <summary>
    /// 登入
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            // 假設用戶驗證成功（通常是對接數據庫驗證用戶名和密碼）
            if (login.Username == "test" && login.Password == "password")
            {
                // 創建用戶的 Claims，這裡可以自定義 Claims 信息
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, login.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                // 密鑰和簽名算法
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vN8g4LjNWyVbmGRRBgIXgvGlV/nRIvYIshZw7H3vqZI=")); // 要與驗證中相同
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 創建 JWT Token
                var token = new JwtSecurityToken(
                    issuer: "https://mia1-issuer.com",
                    audience: "miaAudience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                // 返回 Token
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        /// <summary>
        /// 帳號  ex. test
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密碼  ex. password
        /// </summary>
        public string Password { get; set; }
    }
}

