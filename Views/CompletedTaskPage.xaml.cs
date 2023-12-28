using AppTask.Models;
using Todo.Repositories;

namespace Todo.Views;

public partial class CompletedTaskPage : ContentPage
{

    private readonly ITaskModelRepository _repository;
    private IList<TaskModel> _tasks;

    public CompletedTaskPage()
    {
        InitializeComponent();
        _repository = new TaskModelRepository();
        LoadData();
    }
    public async void LoadData()
    {
        _tasks = await _repository.GetTasks();
        CollectionViewTasks.ItemsSource = _tasks.Where(x => x.IsCompleted == true);
        LabelText.IsVisible = _tasks.Count <= 0;
    }

    private void DeleteTask(object sender, TappedEventArgs e)
    {

    }

    private async void OnTapPutTask(object sender, TappedEventArgs e)
    {
        var task = (TaskModel)e.Parameter;
        await Navigation.PushModalAsync(new EditTaskPage(await _repository.GetTaskById(task.Id)));
    }

    private async void CompletedTask(object sender, TappedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        TaskModel task = (TaskModel)e.Parameter;
        if (DeviceInfo.Platform != DevicePlatform.WinUI)
        {
            checkBox.IsChecked = !checkBox.IsChecked;
        }

        TaskModel existingTask = await _repository.GetTaskById(task.Id);
        if (existingTask != null)
        {
            existingTask.IsCompleted = checkBox.IsChecked;
            await _repository.PutTask(existingTask);
            LoadData();
        }
    }

    private void OnTextChangedTask(object sender, TextChangedEventArgs e)
    {
        var word = e.NewTextValue;
        var task = _tasks.Where(x => x.Name.ToLower().Contains(word.ToLower()));

        CollectionViewTasks.ItemsSource = task;
    }

    private void SearchFocus(object sender, TappedEventArgs e)
    {

    }

    private async void CloseModal(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
        var navPage = (NavigationPage)App.Current.MainPage;
        var homepage = (HomeTaskPage)navPage.CurrentPage;
        homepage.LoadData();
    }
}