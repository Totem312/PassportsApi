using ICSharpCode.SharpZipLib.BZip2;
using System.IO.Compression;
using WebApi.Interfases;

namespace WebApi.FileOperation
{
    public class ExtractZipFile : IExtract
    {
        Settings _settings;
        public ExtractZipFile(Settings settings)
        {
            _settings = settings;
        }

        public async Task ExtractAsync()
        {
            BZip2.Decompress(File.OpenRead(_settings.ZipFileName), File.Create(_settings.DecompressFileName), true);
            await Task.CompletedTask;
        }
    }
}
