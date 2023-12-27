using AppTask.Models;

namespace Todo.Repositories;

public interface ITaskModelRepository
{
    Task<IList<TaskModel>> GetTasks();

    Task<TaskModel> GetTaskById(int id);

    Task PotsTask(TaskModel model);

    Task PutTask(TaskModel model);

    Task DeleteTask(TaskModel model);
}
