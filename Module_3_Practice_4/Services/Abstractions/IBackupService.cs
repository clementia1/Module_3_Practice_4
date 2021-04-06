using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_3_Practice_4.Services.Abstractions
{
    public interface IBackupService
    {
        Task CreateLogBackup(string logFilepath, DateTime dateTime);
    }
}
