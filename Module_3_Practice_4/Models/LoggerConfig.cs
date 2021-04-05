using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_3_Practice_4.Models
{
    public class LoggerConfig
    {
        public string LogsDir { get; set; }
        public string BackupDir { get; set; }
        public string FileExtension { get; set; }
        public string DateTimeFormat { get; set; }

        public string LogBackupFrequency { get; set; }
    }
}
