using System;
using System.Security.Cryptography;
using System.Text;

namespace Echo.Application
{
    public static class HashExtention
    {
        public static string GenerateSHA256Hash(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Message cannot be null or empty.", nameof(input));

            // Compute the SHA-256 hash of the input
            using (var sha256 = SHA256.Create())
            {
                var messageBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(messageBytes);

                // Encode the hash in Base64 to shorten its length
                return Convert.ToBase64String(hashBytes)
                    .Replace("+", "") // Remove '+' to make it URL/DB safe
                    .Replace("/", "") // Remove '/' to make it URL/DB safe
                    .Replace("=", ""); // Remove '=' for compactness
            }
        }
    }

}
