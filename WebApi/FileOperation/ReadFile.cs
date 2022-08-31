using WebApi.Interfases;
using static WebApi.FileOperation.ReadFile;
using System.Diagnostics;
using System.Collections.Generic;

namespace WebApi.FileOperation
{
    public class ReadFile : IReadFile
    {
        private readonly IFileAddingToDb _addToDb;
        public ReadFile(IFileAddingToDb addToDb)
        {
            _addToDb = addToDb;
        }

        public async Task ReadAllFile(string fileName)
        {
            int count = 1;
            var validationList = new List<List<Tuple<uint, uint>>>();
            var chunk = new List<Tuple<uint, uint>>();
            Stopwatch watch = Stopwatch.StartNew();

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    if (count > 100_000)
                    {
                        validationList.Add(chunk);
                        count = 1;
                        chunk = new List<Tuple<uint, uint>>();

                    }
                    string[] columns = (await reader.ReadLineAsync())!.Trim().Split(',');
                    if (uint.TryParse(columns[0], out var serial) && uint.TryParse(columns[1], out var number))
                    {
                        chunk.Add(Tuple.Create(serial, number));
                        count++;
                    }
                }
                validationList.Add(chunk);
            }
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 6;
            await Parallel.ForEachAsync(validationList, options, async (item, token) =>
            {
                await _addToDb.AddToDb(item);
            });
            Console.WriteLine(new TimeSpan(watch.ElapsedTicks).TotalSeconds);
            await _addToDb.ClearTable();
        }

    }
}
