using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module_3_Practice_4.Models;
using Module_3_Practice_4.Services.Abstractions;

namespace Module_3_Practice_4.Services
{
    public class LoggerService : ILoggerService
    {
        private IFileService _fileService;
        private IConfigService _configService;
        private LoggerConfig _config;
        private string _logFilepath;
        private int _backupFrequency;
        private int _logCount;

        public LoggerService()
        {
            _fileService = new FileService();
            _configService = new ConfigService();
            _config = _configService.GetConfig();
            _backupFrequency = int.Parse(_config.LogBackupFrequency);

            Init();
        }

        public event Action<string> CreateBackup;

        public string LogFilepath => _logFilepath;

        public async Task Log(string message)
        {
            var logMessage = $"{DateTime.UtcNow} {message}";

            try
            {
                await _fileService.WriteLineAsync(_logFilepath, logMessage);
                _logCount++;
                InvokeBackupEvent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InvokeBackupEvent()
        {
            if (_logCount % _backupFrequency == 0)
            {
                CreateBackup?.Invoke(_logFilepath);
            }
        }

        private void Init()
        {
            var datetimeFormatted = DateTime.UtcNow.ToString(_config.DateTimeFormat);
            _logFilepath = $@"{_config.LogsDir}\{datetimeFormatted}{_config.LogFileExtension}";
        }
    }
}
