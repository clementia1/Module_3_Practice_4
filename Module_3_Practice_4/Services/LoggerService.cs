using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_3_Practice_4.Models;
using Module_3_Practice_4.Services.Abstractions;

namespace Module_3_Practice_4.Services
{
    public class LoggerService : ILoggerService
    {
        private IFileService _fileService;
        private IConfigService _configService;
        private LoggerConfig _loggerConfig;
        private int _backupFrequency;
        private int _logCount;

        public LoggerService()
        {
            _fileService = new FileService();
            _configService = new ConfigService();
            _loggerConfig = _configService.GetConfig();
            _backupFrequency = int.Parse(_loggerConfig.LogBackupFrequency);
        }

        public event Action<DateTime> CreateBackup;

        public async Task Log(string message)
        {
            var logMessage = $"{DateTime.UtcNow}: {message}";

            try
            {
                await _fileService.WriteLineAsync(logMessage);
                _logCount++;

                if (_logCount % _backupFrequency == 0)
                {
                    var time = DateTime.UtcNow;
                    Console.WriteLine(time);
                    CreateBackup?.Invoke(time);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
