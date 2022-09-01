namespace WebApi.ParallelManager;

public class TaskManager
    {
        /// <summary>
        /// Скоуп
        /// </summary>
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Настройки задач
        /// </summary>
        private readonly TaskManagerSettings _settings;

        /// <summary>
        /// Параметры задач
        /// </summary>
        private Dictionary<string, TasksSetting> _taskParams;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="scopeFactory"></param>
        /// <param name="settings"></param>
        public TaskManager(IServiceScopeFactory scopeFactory, TaskManagerSettings settings)
        {
            _scopeFactory = scopeFactory;
            _settings = settings;
            _taskParams = settings.TaskSettings.ToDictionary(i => i.OperationId);
        }

        /// <summary>
        /// Параметры
        /// </summary>
        /// <returns></returns>
        private TasksSetting DefaultParam()
        {
            return new TasksSetting
            {
                OperationId = "default",
                MaxDop = 6
            };
        }

        /// <summary>
        /// For
        /// </summary>
        /// <param name="key"></param>
        /// <param name="act"></param>
        /// <typeparam name="Task"></typeparam>
        public void For<Task>(int from, int to, Action<ForTaskInfo<Task>> act = null)
            where Task : IExecutionTask
        {
            using var taskScope = _scopeFactory.CreateScope();
            {
                var task = taskScope.ServiceProvider.GetService<Task>();
                if (task == null)
                    throw new NotImplementedException($"Не найдена задача {typeof(Task)}");

                var key = task.OperationId;

                var currentParam = _taskParams.ContainsKey(key)
                    ? _taskParams[key]
                    : DefaultParam();

                if (currentParam.MaxDop > 0)
                {
                    Parallel.For(from, to,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = currentParam.MaxDop
                        }, i =>
                        {
                            var taskInfo = new ForTaskInfo<Task> { CurrentIndex = i };
                            act?.Invoke(taskInfo);
                            ExecuteTask(taskInfo.Task);
                        }
                    );
                }
                else
                {
                    for (int i = from; i < to; i++)
                    {
                        var taskInfo = new ForTaskInfo<Task> { CurrentIndex = i };
                        act?.Invoke(taskInfo);
                        ExecuteTask<Task>(taskInfo.Task);
                    }
                }
            }
        }

        /// <summary>
        /// Исполнение задач
        /// </summary>
        /// <param name="act"></param>
        /// <typeparam name="Task"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void ExecuteTask<Task>(string key, Action<Task> act = null) where Task : IExecutionTask
        {
            var currentParam = _taskParams.ContainsKey(key)
                ? _taskParams[key]
                : DefaultParam();

            if (currentParam.MaxDop > 0)
            {
                Parallel.Invoke(new ParallelOptions
                    {
                        MaxDegreeOfParallelism = currentParam.MaxDop
                    }, () => ExecuteTask(act));
            }
            else
            {
                ExecuteTask(act);
            }
        }

        /// <summary>
        /// Исполнение задач
        /// </summary>
        /// <param name="act"></param>
        /// <typeparam name="Task"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void ExecuteTask<Task>(Action<Task> act = null) 
            where Task : IExecutionTask
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var task = scope.ServiceProvider.GetService<Task>();

                if (task == null)
                    throw new NotImplementedException($"Задача типа {typeof(Task)} не найдена!");

                if (act == null)
                {
                    task.Execute();
                    return;
                }

                act(task);
            }
        }
    }

    public class ForTaskInfo<T> where T : IExecutionTask
    {
        public Action<T> Task { get; set; }

        public int CurrentIndex { get; set; }
    }