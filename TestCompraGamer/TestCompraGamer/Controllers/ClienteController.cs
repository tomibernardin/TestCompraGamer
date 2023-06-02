using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestCompraGamer.Models;

namespace TestCompraGamer.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    id = 1,
                    nombre = "Jesus"
                },
                new Cliente
                {
                    id = 2,
                    nombre = "Miguel"
                },
                new Cliente
                {
                    id = 3,
                    nombre = "Luis"
                }
            };
            return clientes;
        }

        [HttpGet]
        [Route("listarxid")]
        public dynamic listarClienteXId(int id)
        {
            return new Cliente
            {
                id = id,
                nombre = "Jesus"
            };
        }

        //[HttpGet]
        //[Route("listarxdni")]
        //public dynamic listarClienteXdni(int dni)
        //{
        //    return new Cliente();
        //}

        [HttpPost]
        [Route("guardar")]
        public dynamic guardarCliente(Cliente cliente)
        {
            cliente.id = 4;
            return new
            {
                success = true,
                message = "Cliente registrado",
                result = cliente
            };
        }

        [HttpDelete]
        [Route("eliminar")]
        public dynamic eliminarCliente(Cliente cliente)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rtaToken = Jwt.validarToken(identity);
            if (!rtaToken.success) return rtaToken;
            
            return new
            {
                success = true,
                message = "cliente eliminado",
                result = cliente
            };
        }
    }
}
