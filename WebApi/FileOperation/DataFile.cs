using WebApi.Interfases;

namespace WebApi.FileOperation
{
    public class DataFile:IDataFile
    {
        IFilePathService _path;
        public DataFile(IFilePathService path)
        {
            _path = path;
        }
        public async Task WriteFileAsync(List<List<(uint series, uint number)>> listTuple)
        {
            using (StreamWriter writer = new StreamWriter(_path.GetTextFilePath))
            {
                (string series, string number) heder = new("series", "number");
                await writer.WriteLineAsync($"{heder.series},{heder.number}");
                for (int i = 0; i < listTuple.Count; i++)
                {
                    foreach (var tuple in listTuple[i])
                    {
                        await writer.WriteLineAsync($"{tuple.series},{tuple.number}");
                    }
                }
            };
        }
    }
}
