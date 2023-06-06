using Dapper;
using MySql.Data.MySqlClient;
using TestCompraGamer.Models;

namespace TestCompraGamer.Utilities
{
    public class PersonalAuthenticationService
    {
        private readonly string _connectionString;

        public PersonalAuthenticationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Authenticate(string username, string password)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var user = connection.QueryFirstOrDefault<Usuario_Acceso>("SELECT * FROM usuario_acceso WHERE usuario = @Username", new { Username = username });

                if (user != null)
                {
                    if (user.password == password)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
