using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_3_Practice_4.Models;
using Module_3_Practice_4.Services.Abstractions;

namespace Module_3_Practice_4.Services
{
    public class BackupService : IBackupService
    {
        private IDirectoryService _directoryService;
        private IFileService _fileService;
        private IConfigService _configService;
        private LoggerConfig _config;

        public BackupService()
        {
            _directoryService = new DirectoryService();
            _fileService = new FileService();
            _configService = new ConfigService();
            _config = _configService.GetConfig();

            CreateBackupDir();
        }

        public async Task CreateLogBackup(string logFilepath, DateTime dateTime)
        {
            var datetimeFormatted = dateTime.ToString("HH.mm.ss.ff");
            var backupFilename = $@"{_config.BackupDir}\{datetimeFormatted}{_config.LogFileExtension}";

            await _fileService.FileCopy(logFilepath, backupFilename);
        }

        public void CreateBackupDir()
        {
            _directoryService.CreateIfNotExists(_config.BackupDir);
        }
    }
}
