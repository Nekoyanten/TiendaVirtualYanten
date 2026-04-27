using System.Security.Cryptography;
using System.Text;

namespace TiendaVirtualYanten.Helpers
{
    public class HashHelper
    {
        // Convierte una cadena de texto en un Hash SHA256
        public static string GetSha256(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el hash a partir de los bytes del texto
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Convertir el array de bytes a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
