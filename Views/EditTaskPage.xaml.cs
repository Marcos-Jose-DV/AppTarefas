using AppTask.Models;
using System.Globalization;
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
        CreatedDate();
        ValidDataFromForm(Entry_TaskName.Text, Editor_TaskDescription.Text);

        _task.Name = Entry_TaskName.Text;
        _task.Description = Editor_TaskDescription.Text;
        _task.PrevisionDate = DatePicker_TaskDate.Date;
        _task.Created = DateTime.UtcNow;
        _task.IsCompleted = false;
     
        Close();
    }
    private void CreatedDate()
    {
        DatePicker_TaskDate.Date = new DateTime(DatePicker_TaskDate.Date.Year, DatePicker_TaskDate.Date.Month, DatePicker_TaskDate.Date.Day, 23, 59, 59);
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

    private bool ValidDataFromForm(string name, string description)
    {
        if(!string.IsNullOrEmpty(name) || !string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        if (!string.IsNullOrEmpty(description) || !string.IsNullOrWhiteSpace(description))
        {
            return false;
        }

        return true;
    }
}