using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace ChatEmcriptacion.Models
{
    public enum KeySizes
    {
        SIZE_512 = 512,
        SIZE_1024 = 1024,
        SIZE_2048 = 2048,
        SIZE_952 = 952,
        SIZE_1369 = 1369
    };
    public class encriptacion
    {


        internal string Encrypt(string plainText, string password)
        {
            if (plainText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = encriptacion.Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        internal string Decrypt(string encryptedText, string password)
        {
            if (encryptedText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = encriptacion.Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);


        }



            private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
            {
                byte[] encryptedBytes = null;

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                        AES.KeySize = 256;
                        AES.BlockSize = 128;
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }

                        encryptedBytes = ms.ToArray();
                    }
                }

                return encryptedBytes;
            }

            private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
            {
                byte[] decryptedBytes = null;

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                        AES.KeySize = 256;
                        AES.BlockSize = 128;
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);
                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }

                        decryptedBytes = ms.ToArray();
                    }
                }

                return decryptedBytes;
            }

        // encriptacion 3DES
        internal string Encript3DES(string message, string key) {

            try {

                TripleDESCryptoServiceProvider des;
                MD5CryptoServiceProvider hashmd5;
                byte[] keyhash, buff;
                string stEncripted;

                hashmd5 = new MD5CryptoServiceProvider();
                keyhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));

                hashmd5 = null;
                des = new TripleDESCryptoServiceProvider();
                des.Key = keyhash;
                des.Mode = CipherMode.ECB;
                buff = ASCIIEncoding.ASCII.GetBytes(message);
                stEncripted = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buff, 0, buff.Length));
                return stEncripted;


            } catch (Exception ex) {
                throw ex;
            }
            


        }


        internal string Decript3DES(string message, string key) 
        {

            try {
                TripleDESCryptoServiceProvider des;
                MD5CryptoServiceProvider hashmd5;
                byte[] keyhash, buff;
                string stDecripted;

                hashmd5 = new MD5CryptoServiceProvider();
                keyhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));

                hashmd5 = null;
                des = new TripleDESCryptoServiceProvider();
                des.Key = keyhash;
                des.Mode = CipherMode.ECB;
                buff = Convert.FromBase64String(message);
                stDecripted = ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buff,0,buff.Length));

                return stDecripted;


            } catch(Exception ex) { throw ex; }

        }

        //encriptacion RSA

        public static RSAParameters  publicKey;
        public static RSAParameters privateKey;

        internal string EncriptacionRSA(string texto) {
            generateKeys();
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(texto));

            string result = Convert.ToBase64String(encrypted);

            return result;

        }

        internal string DecriptacionRSA(string texto)
        {
            
            byte[] asciiString = Convert.FromBase64String(texto);
            byte[] decrypted = Decrypt(asciiString);


            string result = Encoding.UTF8.GetString(decrypted);


            return result;

        }

        static void generateKeys() {
            using (var rsa = new RSACryptoServiceProvider(2048)) {

                rsa.PersistKeyInCsp = false;
          
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);



            }


        }

        static byte[] Encrypt(byte[] input) {
            byte[] encrypted;

            using (var rsa = new RSACryptoServiceProvider(2048)) {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publicKey);
                encrypted = rsa.Encrypt(input, true);

                        
 
            }

            return encrypted;

        }

        static byte[] Decrypt(byte[] input) {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048)) {

                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                decrypted = rsa.Decrypt(input, true);

            }
            return decrypted;

        }


        }
    }
