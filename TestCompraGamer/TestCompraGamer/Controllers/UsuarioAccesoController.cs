using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCompraGamer.Models;
using TestCompraGamer.Repositories;

namespace TestCompraGamer.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioAccesoController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAccesoController(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _usuarioRepository.GetAllUsers());
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _usuarioRepository.GetById(id));
        }

        [HttpGet("{usuario}")]
        public async Task<IActionResult> GetUserByUser(string usuario)
        {
            return Ok(await _usuarioRepository.GetByUsuario(usuario));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Usuario_Acceso usuario)
        {
            if (usuario == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _usuarioRepository.InsertUser(usuario);
            return Created("Usuario creado ", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] Usuario_Acceso usuario)
        {
            if (usuario == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _usuarioRepository.UpdateUser(usuario);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _usuarioRepository.DeleteUser(new Usuario_Acceso { id = id});
            return NoContent();
        }

        /* -------------------------------------------------------------------------------------------------------------------------------- */

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
