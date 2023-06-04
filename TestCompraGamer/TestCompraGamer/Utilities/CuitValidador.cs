namespace TestCompraGamer.Utilities
{
    public class CuitValidador
    {
        public bool validacion(string cuitStr)
        {
            //Console.WriteLine("El cuit es: " + cuitStr);

            if (cuitStr.Length != 10 || !EsNumero(cuitStr))
            {
                //Console.WriteLine("El CUIT debe contener 10 dígitos numéricos.");
                return false;
            }

            int[] cuit = new int[10];
            for (int i = 0; i < 10; i++)
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

            //Console.WriteLine("El dígito verificador del CUIT es: " + digitoVerificador);

            return digitoVerificador == cuit[10] ? true : false;
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
