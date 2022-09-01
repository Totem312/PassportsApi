namespace WebApi.ParallelManager;

public abstract class BaseExecutionTask<T> : IExecutionTask<T>
{
    /// <summary>
    /// Идентфикатор операции
    /// </summary>
    public abstract string OperationId { get; }

    public void Execute(object parameter = null)
    {
        Execute(default);
    }


    public abstract void Execute(T parameter);

}