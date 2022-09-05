using ICSharpCode.SharpZipLib.BZip2;
using WebApi.Interfases;

namespace WebApi.FileOperation
{
    public class ExtractZipFile : IExtract
    {
        IFilePathService _filePath;
        public ExtractZipFile(IFilePathService filePath)
        {         
            _filePath = filePath;
        }
        public async Task<string> ExtractAsync(String path)
        {
            string fileName = _filePath.GetFilePath;
            BZip2.Decompress(File.OpenRead(path), File.Create(fileName), true);
            await Task.CompletedTask;
            return fileName;
        }
    }
}
