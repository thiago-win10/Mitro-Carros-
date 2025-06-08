using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Common;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MitroVehicle.Application.Common.AES
{
    public class AesEncryptionService
    {
     
        public string Encrypt(string plainText)
        {
            var encryptionKeyAES = Configuration.EncryptionAESPath;
            var encryptionKeyIVAES = Configuration.EncryptionAESIVPath;

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(encryptionKeyAES);
            aes.IV = Encoding.UTF8.GetBytes(encryptionKeyIVAES);

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var writer = new StreamWriter(cs);
            writer.Write(plainText);
            writer.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            var encryptionKeyAES = Configuration.EncryptionAESPath;
            var encryptionKeyIVAES = Configuration.EncryptionAESIVPath;

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(encryptionKeyAES);
            aes.IV = Encoding.UTF8.GetBytes(encryptionKeyIVAES);

            var buffer = Convert.FromBase64String(cipherText);

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);

            return reader.ReadToEnd();
        }

        //public string Decrypt(string base64Encoded)
        //{
        //    var base64Bytes = Convert.FromBase64String(base64Encoded);
        //    return System.Text.Encoding.UTF8.GetString(base64Bytes);
        //}

        //public  string Encrypt(string plainText)
        //{
        //    var plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        //    return Convert.ToBase64String(plainBytes);
        //}

    }
}
