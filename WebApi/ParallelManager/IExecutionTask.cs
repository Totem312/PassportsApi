namespace WebApi.ParallelManager;

public interface IExecutionTask<T> : IExecutionTask
{
    /// <summary>
    /// Исполнение
    /// </summary>
    void Execute(T parameter);
}
public interface IExecutionTask
{
    /// <summary>
    /// Идентифкатор операции
    /// </summary>
    string OperationId { get; }
    void Execute(object parameter = null);
}