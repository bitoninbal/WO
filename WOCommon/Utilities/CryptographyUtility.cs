using System.Security.Cryptography;
using System.Text;

namespace WOCommon.Utilities
{
    /// <summary>
    /// Help methods for Cryptography in WO application.
    /// </summary>
    public abstract class CryptographyUtility
    {
        #region Private Methods
        /// <summary>
        /// Uses Sha256 for hashing a string.
        /// </summary>
        /// <param name="bytes">The string to hash</param>
        /// <returns>Hashed string.</returns>
        public static string HashBytes(byte[] bytes)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var hasedBytes = sha256Hash.ComputeHash(bytes);
                var sb = new StringBuilder();

                foreach (var b in hasedBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
        #endregion
    }
}
