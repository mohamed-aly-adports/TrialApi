using System.Security.Cryptography;
using System.Text;

namespace Trial.Infrastructure.Security
{
    public class EncryptionHelper
    {
        /// <summary>
        /// يشفر نصاً باستخدام AES ويمرر مفتاح التشفير كنص.
        /// سيتم إنشاء IV عشوائي وتضمينه مع الناتج المشفر (Base64).
        /// </summary>
        public static string EncryptString(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = CreateAesKey(key);
                aesAlg.GenerateIV();
                byte[] iv = aesAlg.IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // نبدأ بكتابة الـ IV على الـ MemoryStream لتضمينه مع النص المشفر.
                    msEncrypt.Write(iv, 0, iv.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// يفك تشفير نص مشفر (Base64) باستخدام AES والمفتاح النصي.
        /// </summary>
        public static string DecryptString(string cipherText, string key)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = CreateAesKey(key);

                // استخراج الـ IV من النص المشفر.
                int ivSize = aesAlg.BlockSize / 8;
                byte[] iv = new byte[ivSize];
                Array.Copy(fullCipher, 0, iv, 0, ivSize);
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(fullCipher, ivSize, fullCipher.Length - ivSize))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// يحول مفتاح نصي إلى مصفوفة بايت بمقاس مناسب باستخدام SHA256.
        /// </summary>
        private static byte[] CreateAesKey(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }
    }
}
