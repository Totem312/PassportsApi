using System.Diagnostics;
using WebApi.Interfases;
using WebApi.ParallelManager;
using WebApi.ParallelManager.Tasks;

namespace WebApi.FileOperation
{
    public class ReadFile : IReadFile
    {
        private readonly Settings _settings;
        private readonly IFileAddingToDb _addToDb;
        private readonly TaskManager _taskManager;

        public ReadFile(Settings settings, IFileAddingToDb addToDb, TaskManager taskManager)
        {
            _settings = settings;
            _addToDb = addToDb;
            _taskManager = taskManager;
        }

        public async Task ReadAllFile()
        {
            int count = 1;
            var validationList = new List<List<Tuple<uint, uint>>>();
            var chunk = new List<Tuple<uint, uint>>(100_000);
            Stopwatch watch = Stopwatch.StartNew();

            using (StreamReader reader = new StreamReader(_settings.DecompressFileName))
            {
                while (!reader.EndOfStream)
                {
                    if (count > 100_000)
                    {
                        validationList.Add(chunk);
                        count = 1;
                        chunk = new List<Tuple<uint, uint>>(100_000);

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
            
            _taskManager.For<LoadDataTask>(0, validationList.Count, p =>
            {
                var i = p.CurrentIndex;
                p.Task = task => task.Execute(t =>
                {
                    t._addToDb.AddToDb(validationList[i]);
                });
            });
            
            Console.WriteLine(new TimeSpan(watch.ElapsedTicks).TotalSeconds);
            //Console.WriteLine($"Итого:{results.Sum() / 60}");
            await _addToDb.ClearTable();
        }

    }
}
