using System.Security.Cryptography;

namespace RealEstate.WebAPILayer.Tools
{
    public static class PasswordHasher
    {
        private const string Prefix = "PBKDF2";
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int DefaultIterations = 100_000;

        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, DefaultIterations, HashAlgorithmName.SHA256, KeySize);
            return $"{Prefix}${DefaultIterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string storedValue)
        {
            if (string.IsNullOrWhiteSpace(storedValue))
                return false;

            var parts = storedValue.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4 || !string.Equals(parts[0], Prefix, StringComparison.Ordinal))
                return false;

            if (!int.TryParse(parts[1], out var iterations) || iterations <= 0)
                return false;

            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expectedHash.Length);
            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }

        public static bool IsHashed(string storedValue) => storedValue.StartsWith($"{Prefix}$", StringComparison.Ordinal);

        public static bool VerifyOrPlainMatch(string password, string storedValue) =>
            IsHashed(storedValue)
                ? Verify(password, storedValue)
                : string.Equals(password, storedValue, StringComparison.Ordinal);
    }
}
