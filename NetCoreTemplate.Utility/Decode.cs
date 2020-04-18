using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCoreTemplate.Utility
{
    /// <summary>
    /// 解密
    /// </summary>
    public class Decode
    {
        /// <summary> 
        /// DES对称解密数据
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DES(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Encrypt.MD5(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Encrypt.MD5(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            string msg = Encoding.Default.GetString(ms.ToArray());
            cs.Close();
            ms.Close();
            des.Dispose();
            return msg;
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FromBase64String(string content)
        {
            var dataBuffer = Convert.FromBase64String(content);
            return Encoding.UTF8.GetString(dataBuffer);
        }
    }
}
