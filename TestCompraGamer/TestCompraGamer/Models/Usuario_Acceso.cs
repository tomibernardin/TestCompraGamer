namespace TestCompraGamer.Models
{
    public class Usuario_Acceso
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }

        public static List<Usuario_Acceso> DB()
        {
            var list = new List<Usuario_Acceso>()
            {
                new Usuario_Acceso {
                    id = 1,
                    usuario = "Pepe",
                    password = "password1"
                },
                new Usuario_Acceso {
                    id = 2,
                    usuario = "Moni",
                    password = "password2"
                },
                new Usuario_Acceso {
                    id = 3,
                    usuario = "Paola",
                    password = "password3"
                }
            };
            return list;
        }
    }
}
