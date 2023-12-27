using AppTask.Models;

namespace Todo.Views;

public partial class EditTaskPage : ContentPage
{
	public EditTaskPage()
	{
		InitializeComponent();
	}

    private void AddStep(object sender, EventArgs e)
    {
        var msg = DisplayPromptAsync("Etapa", "Digite o nome da etapa:","Adicionar", "Cancelar");
    }

    private void CloseModal(object sender, EventArgs e)
    {
        Close();
    }

    private void SaveData(object sender, EventArgs e)
    {
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
        TaskModel task = new();
    }
}