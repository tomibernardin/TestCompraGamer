namespace TestCompraGamer.Utilities
{
    public class CuitValidador
    {
        public bool validacion(string cuitStr)
        {
            Console.WriteLine("El cuit es: " + cuitStr);

            if (cuitStr.Length != 11 || !EsNumero(cuitStr))
            {
                Console.WriteLine("El CUIT debe contener 11 dígitos numéricos.");
                return false;
            }

            int[] cuit = new int[11];
            for (int i = 0; i < 11; i++)
            {
                cuit[i] = int.Parse(cuitStr[i].ToString());
            }

            int suma = 0;
            int[] multiplicadores = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 10; i++)
            {
                suma += cuit[i] * multiplicadores[i];
            }

            int digitoVerificador = 11 - (suma % 11);
            if (digitoVerificador == 11)
            {
                digitoVerificador = 0;
            }

            Console.WriteLine("El dígito verificador del CUIT es: " + digitoVerificador + " El numero final en el cuit es " + cuit[cuit.Length -1]);

            return digitoVerificador == cuit[cuit.Length - 1] ? true : false;
        }

        static bool EsNumero(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
