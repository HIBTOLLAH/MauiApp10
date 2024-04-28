using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
namespace MauiApp10;

public partial class Yapilacaklar : ContentPage

{
    public ObservableCollection<string> DailyTasks { get; set; }
    public ICommand AddTaskCommand { get; }
    public ICommand EditTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand SaveCommand { get; }
    public Yapilacaklar()
	{
		InitializeComponent();
        DailyTasks = new ObservableCollection<string>();



        AddTaskCommand = new Command(OnAddTask);
        EditTaskCommand = new Command<string>(OnEditTask);
        DeleteTaskCommand = new Command<string>(OnDeleteTask);
        SaveCommand = new Command(OnSave);

        BindingContext = this;
    }
    private void LoadTasks()
    {
        // Yerel depolamadan g�nl�k g�revleri y�kle
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dailytasks.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DailyTasks = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
        }
        else
        {
            // E�er g�rev dosyas� yoksa, yeni bir g�rev listesi olu�tur
            DailyTasks = new ObservableCollection<string>();
        }
    }

    private void SaveTasks()
    {
        // Yerel depolamada g�ncel g�rev listesini sakla
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dailytasks.json");
        string json = JsonSerializer.Serialize(DailyTasks);
        File.WriteAllText(filePath, json);
    }

    private async void OnAddTask()
    {
        // Kullan�c�n�n girdi�i g�revi almak i�in bir dialog ekran� a�
        string newTask = await DisplayPromptAsync("G�nl�k G�rev Ekle", "Yeni g�revinizi girin:", "Ekle", "�ptal", null, -1, Keyboard.Default, "");

        // Kullan�c� iptal etmediyse ve bir g�rev girdiyse, listeye ekle
        if (!string.IsNullOrWhiteSpace(newTask))
        {
            DailyTasks.Add(newTask);
            SaveTasks(); // G�rev eklendikten sonra g�revleri kaydet
        }
    }

    private async void OnEditTask(string task)
    {
        // Kullan�c�n�n d�zenlemek istedi�i g�revi almak i�in bir dialog ekran� a�
        string editedTask = await DisplayPromptAsync("G�revi D�zenle", "Yeni g�revinizi girin:", "Kaydet", "�ptal", task, -1, Keyboard.Default, "");

        // Kullan�c� iptal etmediyse ve yeni g�rev bo� de�ilse, g�revi g�ncelle
        if (!string.IsNullOrWhiteSpace(editedTask))
        {
            int index = DailyTasks.IndexOf(task);
            DailyTasks[index] = editedTask;
            SaveTasks(); // G�rev g�ncellendikten sonra g�revleri kaydet
        }
    }

    private async void OnDeleteTask(string task)
    {
        // G�revi silmek isteyip istemedi�ini kullan�c�ya sor
        bool answer = await DisplayAlert("Sil", "Bu g�revi silmek istedi�inizden emin misiniz?", "Evet", "Hay�r");

        if (answer)
        {
            // G�revi listeden sil
            DailyTasks.Remove(task);
            SaveTasks(); // G�rev silindikten sonra g�revleri kaydet
        }
    }

    private void OnSave()
    {
        SaveTasks();
    }

}
