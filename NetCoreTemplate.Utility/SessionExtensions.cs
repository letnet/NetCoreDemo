using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Utility
{
    public static class SessionExtensions
    {
        public static void Set(this ISession session, string key, object value)
        {
            string txt = JsonConvert.SerializeObject(value);
            byte[] bytes = Encoding.UTF8.GetBytes(txt);
            session.Set(key, bytes);
        }

        public static T Get<T>(this ISession session, string key)
        {
            if (session.TryGetValue(key, out byte[] bytes))
            {
                var value = Encoding.UTF8.GetString(bytes);
                return JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                return default(T);
            }
        }
    }
}
