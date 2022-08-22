using ICSharpCode.SharpZipLib.BZip2;
using System.IO.Compression;
using WebApi.Interfases;

namespace WebApi
{
    public class ExtractZipFileClass : Settings, IExtract
    {
        Settings _settings;
        public ExtractZipFileClass(Settings settings)
        {
            _settings = settings;
        }
        FileInfo ZipFilePath = new FileInfo(zipFileName);

        public void Extract()
        {
            using (FileStream fileToDecompressAsStream = ZipFilePath.OpenRead())
            {
                string decompressedFileName = DecompressFileName;
                using (FileStream decompressedStream = File.Create(decompressedFileName))
                {
                    try
                    {
                        BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
