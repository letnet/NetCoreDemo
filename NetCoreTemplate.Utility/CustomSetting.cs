using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Utility
{
    public class CustomSetting
    {
        private static IConfigurationSection _customSetting = null;
        public static void LoadAll(IConfiguration configuration)
        {
            _customSetting = configuration.GetSection("CustomSetting");
        }

        public static T GetValue<T>(string key)
        {
            return _customSetting.GetValue<T>(key);
        }
    }
}
