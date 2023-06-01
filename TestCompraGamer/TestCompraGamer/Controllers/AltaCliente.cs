using Microsoft.AspNetCore.Mvc;
using TestCompraGamer.Models;

namespace TestCompraGamer.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class AltaCliente : ControllerBase
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
            string token = Request.Headers.Where(x => x.Key == "token").FirstOrDefault().Value;
            if (token != "testtoken")
            {
                return new
                {
                    success = false,
                    message = "token incorrecto",
                    result = ""
                };
            }
            return new
            {
                success = true,
                message = "cliente eliminado",
                result = cliente
            };
        }
    }
}
