using WebApi.Interfases;
using static WebApi.FileOperation.ReadFile;
using System.Diagnostics;

namespace WebApi.FileOperation
{
    public class ReadFile:IReadFile
    {
        private readonly Settings _settings;
        public ReadFile(Settings settings)
        {
            _settings = settings;
        }
       
        public void ReadAllFile()
        {
            var lines = File.ReadAllLines(_settings.DecompressFileName);
            var dict = new Dictionary<uint, HashSet<uint>>(9999);
            Stopwatch stopwatch = new Stopwatch() ;
            stopwatch.Start();         
            for (uint i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Trim().Split(',');
                if(uint.TryParse(columns[0], out var serial) && uint.TryParse(columns[1], out var number))
                {
                    if (dict.ContainsKey(serial))
                    {
                    dict[serial].Add(number);
                    }
                    else dict.Add(serial, new HashSet<uint>(9999){ number});
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Cловарь с копасити -  "+stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            var dict2 = new Dictionary<uint, HashSet<uint>>();  
            stopwatch.Start();
            for (uint i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Trim().Split(',');
                if (uint.TryParse(columns[0], out var serial) && uint.TryParse(columns[1], out var number))
                {
                    if (dict2.ContainsKey(serial))
                    {
                        dict2[serial].Add(number);
                    }
                    else dict2.Add(serial, new HashSet<uint>() { number });
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Cловарь без копасити - "+stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            List<Tuple<uint,uint>> values = new List<Tuple<uint, uint>>();
            stopwatch.Start();
            for (uint i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Trim().Split(',');
                if (uint.TryParse(columns[0], out var serial) && uint.TryParse(columns[1], out var number))
                {
                values.Add(new Tuple<uint, uint>(serial, number));
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Лист без копасити - " + stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            List<Tuple<uint, uint>> value = new List<Tuple<uint, uint>>(146430339);
            stopwatch.Start();
            for (uint i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Trim().Split(',');
                if (uint.TryParse(columns[0], out var serial) && uint.TryParse(columns[1], out var number))
                {
                    value.Add(new Tuple<uint, uint>(serial, number));
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Лист с копасити - " + stopwatch.ElapsedMilliseconds);
        }

    }
}
