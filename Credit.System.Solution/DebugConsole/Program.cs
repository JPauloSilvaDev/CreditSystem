using System.Security.Cryptography;
using System.Text;

static void Main(string[] args)
{
    string stringToEncrypt = "picacred#12345";
    string key = "my-secret-key-123";

    // Encrypt the string
    string encryptedString = Encrypt(stringToEncrypt, key);
    Console.WriteLine("Encrypted: " + encryptedString);

    // Decrypt the string
    string decryptedString = Decrypt(encryptedString, key);
    Console.WriteLine("Decrypted: " + decryptedString);
}

