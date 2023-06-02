using System.Security.Claims;

namespace TestCompraGamer.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        messagge = "Verificar si se ha enviado un token valido",
                        result = ""
                    };
                }
                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                Usuario_Acceso usuario = Usuario_Acceso.DB().FirstOrDefault(x => x.id.ToString() == id);
                return new
                {
                    success = true,
                    message = "Credenciales exitosas",
                    result = usuario
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                    result = ""
                };
            }
        }
    }
}
