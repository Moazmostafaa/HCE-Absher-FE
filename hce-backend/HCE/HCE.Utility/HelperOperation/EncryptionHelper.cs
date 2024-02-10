using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.HelperOperation
{
    public static class EncryptionHelper
    {
        public static Tuple<string, string> AesEncrypt(string data, string key)
        {
            return AesEncrypt(data, Encoding.UTF8.GetBytes(key));
        }

        public static string AesDecrypt(string data, string key, string iv)
        {
            return AesDecrypt(data, Encoding.UTF8.GetBytes(key), Convert.FromBase64String(iv));
        }

        static Tuple<string, string> AesEncrypt(string data, byte[] key)
        {
            var result = AesEncrypt(Encoding.UTF8.GetBytes(data), key);
            string message = Convert.ToBase64String(result.Item1);
            string iv = Convert.ToBase64String(result.Item2);
            return new Tuple<string, string>(message, iv);
        }

        static string AesDecrypt(string data, byte[] key, byte[] iv)
        {
            return Encoding.UTF8.GetString(AesDecrypt(Convert.FromBase64String(data), key, iv));
        }

        static Tuple<byte[], byte[]> AesEncrypt(byte[] data, byte[] key)
        {
            if (data == null || data.Length <= 0)
            {
                throw new ArgumentNullException($"{nameof(data)} cannot be empty");
            }
            using (var aes = new AesCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                aes.GenerateIV();
                var iv = aes.IV;
                using (var encrypter = aes.CreateEncryptor(aes.Key, iv))
                using (var cipherStream = new MemoryStream())
                {
                    using (var tCryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (var tBinaryWriter = new BinaryWriter(tCryptoStream))
                    {
                        // prepend IV to data
                        cipherStream.Write(iv);
                        tBinaryWriter.Write(data);
                        tCryptoStream.FlushFinalBlock();
                    }

                    var cipherBytes = cipherStream.ToArray();

                    return new Tuple<byte[], byte[]>(cipherBytes, iv);
                }
            }
        }

        static byte[] AesDecrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data == null || data.Length <= 0)
            {
                throw new ArgumentNullException($"{nameof(data)} cannot be empty");
            }

            using (var aes = new AesCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                // get first KeySize bytes of IV and use it to decrypt
                Array.Copy(data, 0, iv, 0, iv.Length);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, iv), CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cs))
                    {
                        // decrypt cipher text from data, starting just past the IV
                        binaryWriter.Write(
                            data,
                            iv.Length,
                            data.Length - iv.Length
                        );
                    }

                    var dataBytes = ms.ToArray();

                    return dataBytes;
                }
            }
        }
    }
}
