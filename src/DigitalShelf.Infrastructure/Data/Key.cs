using System.Security.Cryptography;

namespace DigitalShelf.Infrastructure.Data
{
    public class Key
    {
        public static string GenerateSecret()
        {
            // Gerar uma chave secreta aleatória com 32 bytes (256 bits)
            byte[] keyBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }

        // Chave secreta gerada
        public static string Secret = GenerateSecret();
    }
}