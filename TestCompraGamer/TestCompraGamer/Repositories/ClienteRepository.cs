using Dapper;
using MySql.Data.MySqlClient;
using TestCompraGamer.Models;
using TestCompraGamer.MySql;

namespace TestCompraGamer.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        /* ---------- MYSQL Connection ---------- */
        private readonly MySQLConfiguration _connectionString;
        public ClienteRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        /* ---------- Queries ---------- */
        public async Task<bool> DeleteClient(Cliente cliente)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM cliente WHERE id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = cliente.id });
            return result > 0;
        }

        public async Task<IEnumerable<Cliente>> GetAllClients()
        {
            var db = dbConnection();
            var sql = @"SELECT id, nombre, apellido, fecha_nacimiento, direccion, telefono, email, dni, cuit FROM cliente";
            return await db.QueryAsync<Cliente>(sql, new { });
        }
        public async Task<Cliente> GetClientById(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id, nombre, apellido, fecha_nacimiento, direccion, telefono, email, dni, cuit FROM cliente WHERE id = @Id";
            return await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });

        }

        public async Task<Cliente> GetClientByDni(long dni)
        {
            var db = dbConnection();
            var sql = @"SELECT id, nombre, apellido, fecha_nacimiento, direccion, telefono, email, dni, cuit FROM cliente WHERE dni = @Dni";
            return await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { Dni = dni });
        }

        public async Task<bool> InsertClient(Cliente cliente)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO cliente(nombre, apellido, fecha_nacimiento, direccion, telefono, email, dni, cuit) 
                        VALUES(@Nombre, @Apellido, @Fecha_nacimiento, @Direccion, @Telefono, @Email, @Dni, @Cuit)";
            var result = await db.ExecuteAsync(sql, new { cliente.nombre, cliente.apellido, cliente.fecha_nacimiento, cliente.direccion, cliente.telefono, cliente.email, cliente.dni, cliente.cuit});
            return result > 0;
        }

        public async Task<bool> UpdateClient(Cliente cliente)
        {
            var db = dbConnection();
            var sql = @"UPDATE cliente 
                        SET nombre = @Nombre, apellido = @Apellido, fecha_nacimiento = @Fecha_nacimiento, direccion = @Direccion, 
                        telefono = @Telefono, email = @Email, dni = @Dni, cuit = @Cuit 
                        WHERE id = @Id";
            var result = await db.ExecuteAsync(sql, new { cliente.id, cliente.nombre, cliente.apellido, cliente.fecha_nacimiento, cliente.direccion, cliente.telefono, cliente.email, cliente.dni, cliente.cuit });
            return result > 0;
        }
    }
}
