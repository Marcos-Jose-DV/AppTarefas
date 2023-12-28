using AppTask.Models;
using Todo.Repositories;

namespace Todo.Views;

public partial class HomeTaskPage : ContentPage
{
    private readonly ITaskModelRepository _repository;
    private IList<TaskModel> _tasks;

    public HomeTaskPage()
    {
        InitializeComponent();
        _repository = new TaskModelRepository();
        LoadData();
    }
    public async void LoadData()
    {
        _tasks = await _repository.GetTasks();
        CollectionViewTasks.ItemsSource = _tasks;
        LabelText.IsVisible = _tasks.Count <= 0;
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
        CheckBox checkBox = (CheckBox)sender;
        TaskModel task = (TaskModel)e.Parameter;
        if (DeviceInfo.Platform != DevicePlatform.WinUI)
        {
            checkBox.IsChecked = !checkBox.IsChecked;
        }
        task.IsCompleted = checkBox.IsChecked;
        await _repository.PutTask(task);
    }

    private async void OnTapPutTask(object sender, TappedEventArgs e)
    {
        var task = (TaskModel)e.Parameter;
        await Navigation.PushModalAsync(new EditTaskPage(await _repository.GetTaskById(task.Id)));
    }

    private void OnTextChangedTask(object sender, TextChangedEventArgs e)
    {
        var word = e.NewTextValue;

        var search = _tasks.Where(x => x.Name.ToLower().Contains(word.ToLower())).ToList();
        CollectionViewTasks.ItemsSource = search;
    }
}