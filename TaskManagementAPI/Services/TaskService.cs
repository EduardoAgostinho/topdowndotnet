using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TasksModel = TaskManagementAPI.Models.TasksModel;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TasksModel>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TasksModel> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TasksModel> CreateTaskAsync(TasksModel task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TasksModel> UpdateTaskAsync(TasksModel task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
