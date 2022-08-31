using ICSharpCode.SharpZipLib.BZip2;
using System.IO.Compression;
using WebApi.Interfases;

namespace WebApi.FileOperation
{
    public class ExtractZipFile : IExtract
    { private readonly Settings _settings;
        public ExtractZipFile(Settings settings)
        {
            _settings = settings;
        }
        public async Task<string> ExtractAsync(String[] path)
        {
            string fileName=path[0]+_settings.ZipExtension;
            BZip2.Decompress(File.OpenRead(path[0] + path[1]), File.Create(fileName), true);
            await Task.CompletedTask;
            return fileName;
        }
    }
}
