using System.Reflection;
using WebApi.ParallelManager;
using WebApi.ParallelManager.Tasks;

namespace WebApi.Services;

public static class TaskManagerService
{
    /// <summary>
    /// Добавление менеджера задач
    /// </summary>
    /// <param name="builder"></param>
    public static void AddTaskManger(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<TaskManagerSettings>();
        builder.Services.AddScoped<TaskManager>();
        builder.AddExecutionTasks();
    }

    private static void AddExecutionTasks(this WebApplicationBuilder builder)
    {
        var asms = new List<Assembly>();
        asms.Add(Assembly.GetCallingAssembly());
        asms.AddRange(Assembly.GetCallingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

        asms = asms.Where(i => i.FullName.Contains("WebApi")).ToList();
        
        var allTypes = asms.SelectMany(i => i.GetTypes()).ToList();
        foreach (var type in
                 from assembly in asms
                 from type in assembly.GetTypes()
                 where (type.GetInterfaces().Any(i => i == typeof(IExecutionTask)) || type.IsAssignableFrom(typeof(IExecutionTask)))
                       && !type.IsInterface
                 select type)
        {
            if (type.IsAbstract)
            {
                var childs = allTypes.Where(i => i.IsSubclassOf(type)).ToList();
        
                foreach (var child in childs.Where(i => !i.IsAbstract))
                {
                    builder.Services.AddScoped(child);
                }
        
                continue;
            }
        
            builder.Services.AddScoped(type);
        }
    }
}