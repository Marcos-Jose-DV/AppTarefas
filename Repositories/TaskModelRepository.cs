using AppTask.DataBase.Data;
using AppTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Todo.Repositories;

public class TaskModelRepository : ITaskModelRepository
{
    private readonly AppTaskContext _db;

    public TaskModelRepository()
    {
        _db = new AppTaskContext();
    }

    public async Task<IList<TaskModel>> GetTasks()
    {
        var tasks = await _db.TaskModels
            .AsNoTracking()
            .OrderByDescending(x => x.PrevisionDate)
            .ToListAsync();

        return tasks;
    }


    public async Task<TaskModel> GetTaskById(int id)
    {
        var task = await _db.TaskModels
            .Include(x => x.SubTasks)
            .FirstOrDefaultAsync(x => x.Id == id);

        return task;
    }
    public async Task PotsTask(TaskModel model)
    {
        await _db.TaskModels.AddAsync(model);
        await _db.SaveChangesAsync();
    }

    public async Task PutTask(TaskModel model)
    {
        if (model.Id > 0)
        {
            _db.TaskModels.Update(model);
            await _db.SaveChangesAsync();
        }
    }
    public async Task DeleteTask(TaskModel model)
    {
        if (model.Id > 0)
        {
            _db.TaskModels.Remove(model);
            await _db.SaveChangesAsync();
        }
    }
}