using Dapper;
using MySql.Data.MySqlClient;
using TestCompraGamer.Models;
using TestCompraGamer.MySql;

namespace TestCompraGamer.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        /* ---------- MYSQL Connection ---------- */


        private readonly MySQLConfiguration _connectionString;
        public UsuarioRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
        /* ---------- Queries ---------- */
        public async Task<bool> DeleteUser(Usuario_Acceso usuario)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM usuario_acceso WHERE id = @Id";
            var result = await db.ExecuteAsync(sql, new {Id = usuario.id});
            return result > 0;
        }

        public async Task<IEnumerable<Usuario_Acceso>> GetAllUsers()
        {
            var db = dbConnection();
            var sql = @"SELECT id, usuario, password FROM usuario_acceso";
            return await db.QueryAsync<Usuario_Acceso>(sql, new { });
        }

        public async Task<Usuario_Acceso> GetById(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id, usuario, password FROM usuario_acceso WHERE id = @Id";
            return await db.QueryFirstOrDefaultAsync<Usuario_Acceso>(sql, new { Id = id });
        }

        public async Task<Usuario_Acceso> GetByUsuario(string usuario)
        {
            var db = dbConnection();
            var sql = @"SELECT id, usuario, password FROM usuario_acceso WHERE usuario = @Usuario";
            return await db.QueryFirstOrDefaultAsync<Usuario_Acceso>(sql, new { Usuario = usuario });
        }

        public async Task<bool> InsertUser(Usuario_Acceso usuario)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO usuario_acceso(usuario, password) VALUES(@Usuario, @Password)";
            var result = await db.ExecuteAsync(sql, new {usuario.usuario , usuario.password});
            return result > 0;
        }

        public async Task<bool> UpdateUser(Usuario_Acceso usuario)
        {
            var db = dbConnection();
            var sql = @"UPDATE usuario_acceso SET usuario = @Usuario, password = @Password WHERE id = @Id";
            var result = await db.ExecuteAsync(sql, new { usuario.id, usuario.usuario, usuario.password });
            return result > 0;
        }
    }
}
