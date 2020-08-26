using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCoreTemplate.Utility
{
    /// <summary>
    /// 加密
    /// </summary>
    public class Encrypt
    {

        /// <summary> 
        /// DES对称加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DES(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ms.Close();
            cs.Close();
            des.Dispose();
            return ret.ToString();
        }

        /// <summary>
        /// 加密到HMACSHA256
        /// </summary>
        /// <param name="salt">盐值</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string HS256Sign(string salt, string content)
        {
            var hmacsha1 = new HMACSHA256(Encoding.UTF8.GetBytes(salt));
            var dataBuffer = Encoding.UTF8.GetBytes(content);
            var hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="Password">等待加密的字符串</param>
        /// <returns>加密后的32位字符串</returns>
        public static string MD5(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var m = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(m).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string ToBase64String(string content)
        {
            var dataBuffer = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(dataBuffer);
        }
    }
}
