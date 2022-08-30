using WebApi.Interfases;
using static WebApi.FileOperation.ReadFile;
using System.Diagnostics;
using System.Collections.Generic;

namespace WebApi.FileOperation
{
    public class ReadFile : IReadFile
    {
        private readonly Settings _settings;
        private readonly IFileAddingToDb _addToDb;

        public ReadFile(Settings settings, IFileAddingToDb addToDb)
        {
            _settings = settings;
            _addToDb = addToDb;
        }

        public async Task ReadAllFile()
        {
            int count = 1;
            var validationList = new List<List<Tuple<uint, uint>>>();
            var chank = new List<Tuple<uint, uint>>();
            Stopwatch watch = Stopwatch.StartNew();

            using (StreamReader reader = new StreamReader(_settings.DecompressFileName))
            {
                while (!reader.EndOfStream)
                {
                    if (count > 100_000)
                    {
                        validationList.Add(chank);
                        count = 1;
                        chank = new List<Tuple<uint, uint>>();

                    }
                    string[] columns = reader.ReadLine().Trim().Split(',');
                    if (uint.TryParse(columns[0], out var serial) && uint.TryParse(columns[1], out var number))
                    {
                        chank.Add(Tuple.Create(serial, number));
                        count++;
                    }
                }
                validationList.Add(chank);
            }
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 6;
            await Parallel.ForEachAsync(validationList, options, async (item, token) =>
            {
                await _addToDb.AddToDb(item);
                });


            Console.WriteLine(new TimeSpan(watch.ElapsedTicks).TotalSeconds);
            //Console.WriteLine($"Итого:{results.Sum() / 60}");
            await _addToDb.ClearTable();


        }

    }
}
