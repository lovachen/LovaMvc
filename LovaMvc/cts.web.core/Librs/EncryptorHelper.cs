using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 安全辅助类
    /// </summary>
    public class EncryptorHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string GetMD5(string sourceString)
        {
            MD5 md5 = MD5.Create();
            byte[] source = md5.ComputeHash(Encoding.UTF8.GetBytes(sourceString));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 生成Salt盐
        /// </summary>
        /// <param name="size">随机数长度，默认32字节</param>
        /// <returns></returns>
        public static string CreateSaltKey(int size = 32)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// UTF-8 编码方式的 HmacSha1 加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="input">加密字符串</param>
        /// <returns>返回16进制大写的加字符串</returns>
        public static string HmacSha1(string key, string input)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            HMACSHA1 hmac = new HMACSHA1(keyBytes);
            byte[] hashBytes = hmac.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.AppendFormat("{0:X2}", hashBytes[i]);
            return sb.ToString();
        }

        #region BASE64

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string EncodeBase64(string sourceString)
        {
            return EncodeBase64(sourceString, Encoding.UTF8);
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DecodeBase64(string sourceString)
        {
            return DecodeBase64(sourceString, Encoding.UTF8);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static byte[] DecodeBase64Byte(string sourceString)
        {
            return Convert.FromBase64String(sourceString);
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>string：base64编码字符串</returns>
        public static string EncodeBase64(string str, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            byte[] byteData = encoding.GetBytes(str);
            return Convert.ToBase64String(byteData, 0, byteData.Length);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string DecodeBase64(string str, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            byte[] byteData = Convert.FromBase64String(str);
            return encoding.GetString(byteData);
        }

        #endregion


        /// <summary>
        /// 使用加密服务提供程序实现加密生成随机数
        /// validationKey 的有效值为 20 到 64。
        /// decryptionKey 的有效值为 8 或 24。
        /// </summary>
        /// <param name="numBytes"></param>
        /// <returns>16进制格式字符串</returns>
        public static string CreateMachineKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];
            rng.GetBytes(buff);
            System.Text.StringBuilder hexString = new System.Text.StringBuilder(64);
            for (int i = 0; i < buff.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", buff[i]));
            }
            return hexString.ToString();
        }

        #region DES加密解密

        /// <summary>
        /// Des加密方法
        /// </summary>
        /// <param name="val">待加密数据</param>
        /// <param name="key">8位字符</param>
        /// <param name="iv">8位字符</param>
        /// <returns></returns>
        public static string DESEncrypt(string val, string key, string iv)
        {
            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                int i = cryptoProvider.KeySize;
                MemoryStream ms = new MemoryStream();
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);
                sw.Write(val);
                sw.Flush();
                cst.FlushFinalBlock();
                sw.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch// (Exception ex)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Des解密方法
        /// </summary>
        /// <param name="val">待解密数据</param>
        /// <param name="key">8位字符</param>
        /// <param name="iv">8位字符</param>
        /// <returns></returns>
        public static string DESDecrypt(string val, string key, string iv)
        {
            try
            {
                byte[] byKey = ASCIIEncoding.ASCII.GetBytes(key);
                byte[] byIV = ASCIIEncoding.ASCII.GetBytes(iv);

                byte[] byEnc;
                try
                {
                    byEnc = Convert.FromBase64String(val);
                }
                catch
                {
                    return null;
                }
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }
            catch// (System.Exception ex)
            {
                return String.Empty;
            }
        }

        #endregion

        #region RSA 加密解密

        /// <summary>
        /// 生成一对秘钥。arr[0]私钥，arr[1]公钥
        /// </summary>
        /// <returns></returns>
        public static string[] GenerateRSAKeys()
        {
            string[] sKeys = new String[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);
            return sKeys;
        }

        /// <summary>
        /// 生成并保存 RSA 公钥与私钥
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <returns>存储文件格式：私钥 key.json,公钥 key.public.json</returns>
        public static RSAParameters GenerateRSAKeysAndSave(string filePath)
        {
            RSAParameters publicKeys, privateKeys;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            File.WriteAllText(Path.Combine(filePath, "key.json"), JsonConvert.SerializeObject(privateKeys));
            File.WriteAllText(Path.Combine(filePath, "key.public.json"), JsonConvert.SerializeObject(publicKeys));
            return privateKeys;
        }

        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <param name="withPrivate"></param>
        /// <param name="keyParameters"></param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters keyParameters)
        {
            string filename = withPrivate ? "key.json" : "key.public.json";
            keyParameters = default(RSAParameters);
            if (!Directory.Exists(filePath))
                return false;
            string path = Path.Combine(filePath, filename);
            if (!File.Exists(path))
                return false;
            keyParameters = JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(path));
            return true;
        }


        ///<summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey">公钥</param>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        public static string RSAEncrypt(string publickey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey">私钥</param>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string RSADecrypt(string privatekey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        #endregion

        #region 3DES 加密解密

        /// <summary>
        /// UTF8 编码 3DES 加密
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="key">24字节的秘钥byte数组</param>
        /// <param name="mode"></param>
        /// <param name="iv">CBC 必须，8 位加密向量</param>
        /// <returns>Base64字符串</returns>
        public static string Encrypt3DES(string input, byte[] key, CipherMode mode = CipherMode.ECB, string iv = null)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = mode
            };
            if (mode == CipherMode.CBC)
                des.IV = Encoding.UTF8.GetBytes(iv);
            var desEncrypt = des.CreateEncryptor();
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="input">3DES 加密得到的Base64字符串</param>
        /// <param name="key">24字节的密钥字节数组</param>
        /// <param name="mode"></param>
        /// <param name="iv">CBC 必须填写iv。8位</param>
        /// <returns></returns>
        public static string Decrypt3DES(string input, byte[] key, CipherMode mode = CipherMode.ECB, string iv = null)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = mode,
                Padding = PaddingMode.PKCS7
            };
            if (mode == CipherMode.CBC)
                des.IV = Encoding.UTF8.GetBytes(iv);
            var desDecrypt = des.CreateDecryptor();
            var result = "";
            byte[] buffer = Convert.FromBase64String(input);
            result = Encoding.UTF8.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            return result;

        }

        /// <summary>
        /// utf-8 编码的 3des 加密,并转为16进制的字符串。
        /// 48位 16进制 的秘钥加密
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="hexKey">48位 16进制 的秘钥加密</param>
        /// <returns></returns>
        public static string ECBEncrypt3DESHex(string input, string hexKey)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = HexToBytes(hexKey),
                Mode = CipherMode.ECB
            };
            var desEncrypt = des.CreateEncryptor();
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            byte[] result = desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length);
            return ByteToHex(result);
        }

        /// <summary>
        /// utf-8 编码的 解密3des加密成16进制的字符串
        /// 48位 16进制 的秘钥加密
        /// </summary>
        /// <param name="hexInput">16进制的加密字符串</param>
        /// <param name="hexKey">48位 16进制 的秘钥加密</param>
        /// <returns></returns>
        public static string ECBDecrypt3DESByHex(string hexInput, string hexKey)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = HexToBytes(hexKey),
                Mode = CipherMode.ECB
            };
            var desDecrypt = des.CreateDecryptor();
            byte[] buffer = HexToBytes(hexInput);
            byte[] result = desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(result);
        }


        #endregion

        #region 进制转换

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static string ByteToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
                for (int i = 0; i < bytes.Length; i++)
                    sb.Append(bytes[i].ToString("X2"));
            return sb.ToString();
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 使用加密服务提供程序实现加密生成随机数
        /// </summary>
        /// <param name="numBytes"></param>
        /// <returns></returns>
        public static string CreateRNGKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];
            rng.GetBytes(buff);
            return ByteToHex(buff);
        }



        #endregion


        #region AES 加密解密

        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="sourceString">原文</param>
        /// <param name="key">key</param>
        /// <param name="iv">向量</param>
        /// <param name="encoding">编码</param>
        /// <param name="cipherMode">指定模块</param>
        /// <param name="paddingMode">指定填充</param>
        /// <returns></returns>
        public static string AESEncrypt(string sourceString, string key, string iv, Encoding encoding, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] toEncryptArray = encoding.GetBytes(sourceString);
            var rm = new RijndaelManaged
            {
                IV = encoding.GetBytes(iv),
                Key = encoding.GetBytes(key),
                Mode = cipherMode,
                Padding = paddingMode
            };
            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="val"></param>
        /// <param name="key">base64 编码的 key</param>
        /// <param name="iv">base64 编码的 iv</param>
        /// <param name="encoding"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static string AESDecrypt(string val, string key, string iv, Encoding encoding, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            return AESDecrypt(val, encoding.GetBytes(key), encoding.GetBytes(iv), encoding, cipherMode, paddingMode);
        }


        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="val"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string AESDecrypt(string val, byte[] key, byte[] iv, Encoding encoding, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            try
            {
                byte[] encryptedData = Convert.FromBase64String(val);

                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = key;
                rijndaelCipher.IV = iv;
                rijndaelCipher.Mode = cipherMode;
                rijndaelCipher.Padding = paddingMode;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                return encoding.GetString(plainText);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// AES 256位无向量加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static string AESEncrypt(string sourceString, string key, Encoding encoding, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] toEncryptArray = encoding.GetBytes(sourceString);
            var rm = new RijndaelManaged
            { 
                Key = encoding.GetBytes(key),
                Mode = cipherMode,
                Padding = paddingMode
            };
            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES 256位无向量解密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static string AESDecrypt(string sourceString, string key, Encoding encoding, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            try
            {
                Byte[] toEncryptArray = Convert.FromBase64String(sourceString);
                RijndaelManaged rm = new RijndaelManaged
                {
                    Key = encoding.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = rm.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return encoding.GetString(resultArray);
            }
            catch
            {
                return "";
            }
        }

        #endregion
    }
}
