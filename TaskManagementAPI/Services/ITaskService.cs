using TasksModel = TaskManagementAPI.Models.TasksModel;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TasksModel>> GetTasksAsync();
        Task<TasksModel> GetTaskByIdAsync(int id);
        Task<TasksModel> CreateTaskAsync(TasksModel task);
        Task<TasksModel> UpdateTaskAsync(TasksModel task);
        Task DeleteTaskAsync(int id);
    }
}
