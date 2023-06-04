using TestCompraGamer.Models;

namespace TestCompraGamer.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllClients();
        Task<Cliente> GetClientById(int id);
        Task<Cliente> GetClientByDni(long dni);
        Task<bool> InsertClient(Cliente cliente);
        Task<bool> UpdateClient(Cliente cliente);
        Task<bool> DeleteClient(Cliente cliente);
    }
}
