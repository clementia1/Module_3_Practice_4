using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Module_3_Practice_4.Services.Abstractions;
using Module_3_Practice_4.Models;

namespace Module_3_Practice_4.Services
{
    public class ConfigService : IConfigService
    {
        public LoggerConfig Read()
        {
            var configFile = File.ReadAllText(Paths.ConfigPath);
            var config = JsonConvert.DeserializeObject<LoggerConfig>(configFile);
            return config;
        }
    }
}
