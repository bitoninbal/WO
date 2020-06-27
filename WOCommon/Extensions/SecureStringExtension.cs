using System;
using System.Runtime.InteropServices;
using System.Security;
using WOCommon.Utilities;

namespace WOCommon.Extensions
{
    public static class SecureStringExtension
    {
        /// <summary>
        /// Decrypting SecureString value to a string.
        /// </summary>
        /// <param name="secureString">The SecureString value</param>
        /// <returns>Returns bytes that represent the SecureString's value.</returns>
        public static byte[] ConvertToBytes(this SecureString secureString)
        {
            var result = new byte[secureString.Length * 2];
            var valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                for (int i = 0; i < secureString.Length; i++)
                {
                    result[i] = Marshal.ReadByte(valuePtr, i * 2);
                    result[i + 1] = Marshal.ReadByte(valuePtr, i * 2 + 1);
                }
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }

            return result;
        }
        /// <summary>
        /// Hashes SecureString and returns it as string.
        /// </summary>
        /// <param name="secureString">The SecureString to hash.</param>
        /// <returns>Hashed SecureString's value.</returns>
        public static string HashValue(this SecureString secureString)
        {
            var bytes = secureString.ConvertToBytes();
            var hashedString = CryptographyUtility.HashBytes(bytes);

            Array.Clear(bytes, 0, bytes.Length);

            return hashedString;
        }
    }
}
