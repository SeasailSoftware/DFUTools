using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM32FirmwareUpdater
{
    internal class LocalConfig
    {

        public string Theme { get; set; } = "Light.Green";
        public string Culture { get; set; } = "zh-CN";
        internal static LocalConfig Load(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = "localConfig.json";
            if (File.Exists(path))
            {
                var context = File.ReadAllText(path);
                var options = JsonConvert.DeserializeObject<LocalConfig>(context);
                return options;
            }
            return null;
        }

        internal void Save(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = "localConfig.json";
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }
    }
}
