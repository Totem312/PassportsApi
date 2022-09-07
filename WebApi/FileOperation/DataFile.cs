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
        public async Task WriteFile(List<List<Tuple<uint, uint>>> listTuple)
        {
            using (StreamWriter writer = new StreamWriter(_path.GetTextFilePath))
            {
                var heder = new Tuple<string, string>("serial", "number");
                await writer.WriteLineAsync($"{heder.Item1},{heder.Item2}");
                for (int i = 0; i < listTuple.Count; i++)
                {
                    foreach (var tuple in listTuple[i])
                    {
                        await writer.WriteLineAsync($"{tuple.Item1},{tuple.Item2}");
                    }
                }
            };
        }
    }
}
