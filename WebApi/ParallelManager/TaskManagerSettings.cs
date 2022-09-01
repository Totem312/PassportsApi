namespace WebApi.ParallelManager;

public class TaskManagerSettings
{
    /// <summary>
    /// Настройки задач
    /// </summary>
    public  List<TasksSetting> TaskSettings { get; set; }

    /// <summary>
    /// Ctor
    /// </summary>
    public TaskManagerSettings()
    {
        TaskSettings = new List<TasksSetting>()
        {

        };
    }



}

public class TasksSetting
{
    /// <summary>
    /// Идентификатор операции
    /// </summary>
    public string OperationId { get; set; }

    /// <summary>
    /// уровень паралеллизма
    /// </summary>
    public int MaxDop { get; set; }

}