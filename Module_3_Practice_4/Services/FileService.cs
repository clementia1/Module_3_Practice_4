using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Module_3_Practice_4.Models;
using Module_3_Practice_4.Services.Abstractions;

namespace Module_3_Practice_4.Services
{
    public class FileService : IFileService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IConfigService _configService;
        private readonly LoggerConfig _config;
        private StreamWriter _streamWriter;
        private static SemaphoreSlim sem = new SemaphoreSlim(1);

        public FileService()
        {
            _directoryService = new DirectoryService();
            _configService = new ConfigService();
            _config = _configService.GetConfig();

            _directoryService.CreateIfNotExists(_config.LogsDir);
        }

        public async Task WriteLineAsync(string filepath, string text)
        {
            await sem.WaitAsync();

            using (FileStream stream = File.Open(filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                using (_streamWriter = new StreamWriter(stream, Encoding.Default))
                {
                    await _streamWriter.WriteLineAsync(text);
                }
            }

            sem.Release();
        }

        public async Task FileCopy(string sourceFile, string destinationFile)
        {
            await sem.WaitAsync();

            // var tcs = new TaskCompletionSource<bool>();
            try
            {
                File.Copy(sourceFile, destinationFile);
                /*using (FileStream src = File.Open(sourceFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var dest = File.OpenWrite(destinationFile))
                    {
                        src.CopyTo(dest);

                        // tcs.SetResult(true);
                    }
                }*/
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);

                // "Cannot copy: file in use"
            }

            sem.Release();

            // return await tcs.Task;
        }
    }
}
