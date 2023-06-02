using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCompraGamer.Models;

namespace TestCompraGamer.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioAccesoController : ControllerBase
    {
        public IConfiguration _configuration;
        public UsuarioAccesoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion([FromBody] Object userData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(userData.ToString());

            string user = data.usuario.ToString();
            string password = data.password.ToString();

            Usuario_Acceso usuario = Usuario_Acceso.DB().Where(x => x.usuario == user && x.password == password).FirstOrDefault();

            if (usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.id.ToString()),
                new Claim("usuario", usuario.usuario),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                //expires: DateTime.Now.AddMinutes(60),
                signingCredentials: singIn
                );

            return new
            {
                success = true,
                message = "Login exitoso",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
