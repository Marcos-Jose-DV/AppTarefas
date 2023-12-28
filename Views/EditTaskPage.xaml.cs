using AppTask.Models;
using System.Threading.Tasks;
using Todo.Repositories;

namespace Todo.Views;

public partial class EditTaskPage : ContentPage
{
    private readonly ITaskModelRepository _repository;
    private readonly TaskModel _task;
    public EditTaskPage()
    {
        InitializeComponent();
        _repository = new TaskModelRepository();
        _task = new TaskModel();

        BindableLayout.SetItemsSource(SubTasks_Steps, _task.SubTasks);
    }

    private async void AddStep(object sender, EventArgs e)
    {
        var stepName = await DisplayPromptAsync("Etapa", "Digite o nome da etapa:", "Adicionar", "Cancelar");

        if (!string.IsNullOrWhiteSpace(stepName))
        {
            _task.SubTasks.Add(new SubTaskModel { Name = stepName, IsCompleted = false });
        }
    }

    private void CloseModal(object sender, EventArgs e)
    {
        Close();
    }

    private void SaveData(object sender, EventArgs e)
    {

        TaskModel task = new TaskModel
        {
            Name = Entry_TaskName.Text,
            Description = Editor_TaskDescription.Text,
            PrevisionDate = DatePicker_TaskDate.Date,
            Created = DateTime.Now,
            IsCompleted = false,
        };

        WeakEventManager.ReferenceEquals(this, task);
        _repository.PotsTask(task);
        Close();
    }

    private async void Close()
    {
        await Navigation.PopModalAsync();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        DatePicker_TaskDate.WidthRequest = width - 30;
    }

    private void DeleteTask(object sender, TappedEventArgs e)
    {
        var subTask = (SubTaskModel)e.Parameter;
        _task.SubTasks.Remove(subTask);
    }

    private bool IsNullOrWhiteSpace(string name)
    {
     
    }
}