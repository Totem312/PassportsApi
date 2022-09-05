using WebApi.FileOperation;
using WebApi.Interfases;
using WebApi.Interfeses;
using WebApi.Passports;

namespace WebApi.ParallelManager.Tasks;

public class LoadDataTask: MonadExecution<PassportService>
{
    /// <summary>
    /// Расчет сервисов
    /// </summary>
    public override string OperationId => "LoadData";
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="type"></param>
    public LoadDataTask(IServiseRepository type): base((PassportService)type)
    {
        
    }
    
}