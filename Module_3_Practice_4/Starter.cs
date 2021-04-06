using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_3_Practice_4.Services;
using Module_3_Practice_4.Services.Abstractions;

namespace Module_3_Practice_4
{
    public class Starter
    {
        private ILoggerService _loggerService = new LoggerService();
        private IBackupService _backupService = new BackupService();

        public void Run()
        {
            var logFilepath = _loggerService.LogFilepath;
            _loggerService.CreateBackup += _backupService.CreateLogBackup;

            Task.Run(() => Function1());
            Task.Run(() => Function2());
            Console.ReadKey();
        }

        private async void Function1()
        {
            for (int i = 0; i < 60; i++)
            {
                await _loggerService.Log($"Emitted from {nameof(Function1)}");
            }
        }

        private async void Function2()
        {
            for (int i = 0; i < 60; i++)
            {
                await _loggerService.Log($"Emitted from {nameof(Function2)}");
            }
        }
    }
}
