using WebApi.FileOperation;
using WebApi.Interfases;

namespace WebApi.ParallelManager.Tasks;

public class LoadDataTask: MonadExecution<ReadFile>
{
    /// <summary>
    /// Расчет сервисов
    /// </summary>
    public override string OperationId => "LoadData";
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="type"></param>
    public LoadDataTask(IReadFile type): base((ReadFile)type)
    {
        
    }
    
}