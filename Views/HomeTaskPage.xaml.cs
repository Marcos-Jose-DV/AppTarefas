using AppTask.Models;
using Todo.Repositories;

namespace Todo.Views;

public partial class HomeTaskPage : ContentPage
{
    private readonly ITaskModelRepository _repository;

    public HomeTaskPage()
    {
        InitializeComponent();
        _repository = new TaskModelRepository();
        LoadData();
    }
    private async void LoadData()
    {
        var tasks = await _repository.GetTasks();
        CollectionViewTasks.ItemsSource = tasks;
        LabelText.IsVisible = tasks.Count <= 0;
    }

    private async void AddNewTask(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new EditTaskPage());
    }

    private void SearchFocus(object sender, TappedEventArgs e)
    {
        Search.Focus();
    }

    private async void DeleteTask(object sender, TappedEventArgs e)
    {
        TaskModel task = (TaskModel)e.Parameter;
        var result = await DisplayAlert("Comfirmar exclusão!", $"Tem certeza que deseja apagar essa tarefa: {task.Name}?", "Sim", "Não");

        if (result)
        {
            await _repository.DeleteTask(task);
            LoadData();
        }
    }

    private async void CompletedTask(object sender, TappedEventArgs e)
    {
        TaskModel task = (TaskModel)e.Parameter;
        task.IsCompleted = ((CheckBox)sender).IsChecked;
        await _repository.PutTask(task);
    }
}