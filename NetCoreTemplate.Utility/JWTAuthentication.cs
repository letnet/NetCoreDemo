using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace NetCoreTemplate.Utility
{
    public class JWTAuthentication
    {
        /// <summary>
        /// 生成一个JWT Token
        /// </summary>
        /// <param name="salt">加密用的盐值</param>
        /// <param name="jwtpayload">主体信息</param>
        /// <returns></returns>
        public static string GetToken<T>(string salt, T jwtpayload)
        {
            string header = JsonConvert.SerializeObject(new JWTHeader());
            string payload = JsonConvert.SerializeObject(jwtpayload);
            string base64string = string.Format("{0}.{1}", Encrypt.ToBase64String(header), Encrypt.ToBase64String(payload));
            string sign = Encrypt.HS256Sign(salt, base64string);
            string token = string.Format("{0}.{1}", base64string, sign);
            return token;
        }

        /// <summary>
        /// 验证一个JWT Token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="salt">加密用的盐值</param>
        /// <param name="token">需要验证的token</param>
        /// <returns>jwtPayload</returns>
        public static T Verify<T>(string salt, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new GreteaException("token不能为空");
            string[] arr = token.Split('.');
            if (arr.Length != 3)
                throw new GreteaException("token格式错误");
            var jwtPayload = JsonConvert.DeserializeObject<T>(Decode.FromBase64String(arr[1]));
            string base64string = token.Substring(0, token.LastIndexOf('.'));
            string sign = Encrypt.HS256Sign(salt, base64string);
            if (arr[2] != sign)
                throw new GreteaException("token验证失败");
            return jwtPayload;
        }
    }
    public class JWTHeader
    {
        public string Typ { get { return "JWT"; } }
        public string Alg { get; set; } = "HS256";
    }
    public class JWTPayload
    {
        public string ID { set; get; }
        /// <summary>
        /// 用户类型：ADMIN管理员、USER普通用户
        /// </summary>
        public string Roles { set; get; }
        public DateTime ExpireTime { set; get; }
    }
}
