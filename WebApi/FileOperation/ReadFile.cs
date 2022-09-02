using System.Diagnostics;
using WebApi.Interfases;
using WebApi.ParallelManager;
using WebApi.ParallelManager.Tasks;

namespace WebApi.FileOperation
{
    public class ManagerFile : IManagerFile
    {  
        public async Task<List<List<Tuple<uint, uint>>>> ReadAllFileAsync(string fileName)
        {
            int count = 1;
            var validationList = new List<List<Tuple<uint, uint>>>();
            var chunk = new List<Tuple<uint, uint>>(100_000);
            using (StreamReader reader = new StreamReader(fileName))
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
            return validationList;          
        }
       //TODO WriteFile Method
    }
}
