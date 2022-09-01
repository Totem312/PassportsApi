namespace WebApi.ParallelManager.Tasks;

public abstract class MonadExecution<T> : BaseExecutionTask<Action<T>>
{
    /// <summary>
    /// Тип
    /// </summary>
    private readonly T _type;

    protected MonadExecution(T type)
    {
        _type = type;
    }

    public override void Execute(Action<T> parameter)
    {
        parameter?.Invoke(_type);
    }
}