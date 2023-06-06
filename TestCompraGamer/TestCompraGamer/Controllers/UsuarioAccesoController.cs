using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCompraGamer.Models;
using TestCompraGamer.Repositories;
using TestCompraGamer.Utilities;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _usuarioRepository.DeleteUser(new Usuario_Acceso { id = id });
            return NoContent();
        }

        /* -------------------------------------------------------------------------------------------------------------------------------- */

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario_Acceso usuario)
        {
            var authService = new PersonalAuthenticationService(_configuration.GetConnectionString("MySqlConnection"));

            if (authService.Authenticate(usuario.usuario, usuario.password))
            {
                var token = GenerateJwtToken(usuario.usuario);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
        private string GenerateJwtToken(string username)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Issuer,
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}