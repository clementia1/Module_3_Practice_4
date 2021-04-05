using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Module_3_Practice_4.Models;
using Module_3_Practice_4.Services.Abstractions;

namespace Module_3_Practice_4.Services
{
    public class FileService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IConfigService _configService;
        private readonly LoggerConfig _config;
        private readonly string _filename;
        private StreamWriter _streamWriter;
        private static SemaphoreSlim sem = new SemaphoreSlim(1);

        public FileService()
        {
            _directoryService = new DirectoryService();
            _configService = new ConfigService();
            _config = _configService.Read();

            var datetimeFormatted = DateTime.UtcNow.ToString(_config.DateTimeFormat);
            _filename = $@"{_config.LogsDir}\{datetimeFormatted}{_config.FileExtension}";

            _directoryService.CreateIfNotExists(_config.LogsDir);
        }

        public async Task WriteLineAsync(string text)
        {
            await File.AppendAllTextAsync(_filename, text, Encoding.Default);
        }
    }
}
