using TestCompraGamer.Models;

namespace TestCompraGamer.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario_Acceso>> GetAllUsers();
        Task<Usuario_Acceso?> GetById(int id);
        Task<Usuario_Acceso?> GetByUsuario(string usuario);
        Task<bool> InsertUser(Usuario_Acceso usuario);
        Task<bool> UpdateUser(Usuario_Acceso usuario);
        Task<bool> DeleteUser(Usuario_Acceso usuario);
    }
}
