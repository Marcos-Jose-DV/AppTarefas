using AppTask.Models;
using System.Globalization;
using System.Text;
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
        bool valid = ValidDataFromForm();
      
        if(valid)
        {
            CreatedDate();
            SaveInDatabase();
        }
    }
    private bool ValidDataFromForm()
    {
        bool validResult = true;
        Label_TaskName_Required.IsVisible = false;
        Label_TaskDescription_Required.IsVisible = false;

        if (string.IsNullOrEmpty(Entry_TaskName.Text) || string.IsNullOrWhiteSpace(Entry_TaskName.Text))
        {
            Label_TaskName_Required.IsVisible = true;
            validResult = false;
        }
        if (string.IsNullOrEmpty(Editor_TaskDescription.Text) || string.IsNullOrWhiteSpace(Editor_TaskDescription.Text))
        {
            Label_TaskDescription_Required.IsVisible = true;
            validResult = false;
        }

        return validResult;
    }

    private void CreatedDate()
    {
        DatePicker_TaskDate.Date = new DateTime(DatePicker_TaskDate.Date.Year, DatePicker_TaskDate.Date.Month, DatePicker_TaskDate.Date.Day, 23, 59, 59);
    }
    private void SaveInDatabase()
    {
        _task.Name = Entry_TaskName.Text;
        _task.Description = Editor_TaskDescription.Text;
        _task.PrevisionDate = DatePicker_TaskDate.Date;
        _task.Created = DateTime.UtcNow;
        _task.IsCompleted = false;

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
}