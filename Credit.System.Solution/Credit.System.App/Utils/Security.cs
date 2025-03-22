using System.Security.Cryptography;
using System.Text;

namespace Credit.System.App.Utils
{
    public static class Security
    {
        public static string Encrypt(string plainText, string key)
        {

            try
            {
                if (string.IsNullOrEmpty(plainText))
                    throw new ArgumentNullException(nameof(plainText));

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[16];

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            
                            return Convert.ToBase64String(msEncrypt.ToArray());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Decrypt(string cipherText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    throw new ArgumentNullException(nameof(cipherText));


                using (Aes aesAlg = Aes.Create())
                {
                 
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[16]; 
               
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                  
                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
           
        }

    }




}

