using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCompraGamer.Models;
using TestCompraGamer.Repositories;
using TestCompraGamer.Utilities;

namespace TestCompraGamer.Controllers
{
    [ApiController]
    [Route("cliente")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly CuitValidador _cuitValidador;
        public ClienteController(IClienteRepository clienteRepository, CuitValidador cuitValidador)
        {
            _clienteRepository = clienteRepository;
            _cuitValidador = cuitValidador;

        }

        [HttpGet]
        public async Task<IActionResult> listarClientes()
        {
            return Ok(await _clienteRepository.GetAllClients());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> listarClientesXId(int id)
        {
            return Ok(await _clienteRepository.GetClientById(id));
        }

        [HttpGet("dni/{dni}")]
        public async Task<IActionResult> listarClienteXdni(long dni)
        {
            return Ok(await _clienteRepository.GetClientByDni(dni));
        }

        [HttpPost]
        public async Task<IActionResult> crearCliente([FromBody] Cliente cliente)

        {
            if (cliente == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ------------------------------------- VALIDACION DE INFORMACION -------------------------------------

            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(cliente.nombre))
            {
                errors.Add("El campo 'nombre' no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(cliente.apellido))
            {
                errors.Add("El campo 'apellido' no puede estar vacío.");
            }
            if (cliente.fecha_nacimiento == default)
            {
                errors.Add("El campo 'fecha_nacimiento' no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(cliente.direccion))
            {
                errors.Add("El campo direccion no puede estar vacío.");
            }
            if (cliente.telefono == 0)
            {
                errors.Add("El campo 'telefono' no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(cliente.email))
            {
                errors.Add("El campo 'email' no puede estar vacío.");
            }
            if (cliente.dni == 0)
            {
                errors.Add("El campo 'DNI' no puede estar vacío.");
            }
            if (!_cuitValidador.validacion(cliente.cuit.ToString()))
            {
                errors.Add("Ingrese un CUIT valido.");
            }
            if (errors.Count > 0)
            {
                string finalMessage = string.Join(" ", errors);
                return BadRequest(finalMessage);
            }
            var created = await _clienteRepository.InsertClient(cliente);
            return Created("Cliente creado ", created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> eliminarCliente(int id)
        {
            await _clienteRepository.DeleteClient(new Cliente { id = id });
            return NoContent();
        }
    }
}
