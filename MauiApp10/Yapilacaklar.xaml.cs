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
        // Yerel depolamadan günlük görevleri yükle
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dailytasks.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DailyTasks = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
        }
        else
        {
            // Eðer görev dosyasý yoksa, yeni bir görev listesi oluþtur
            DailyTasks = new ObservableCollection<string>();
        }
    }

    private void SaveTasks()
    {
        // Yerel depolamada güncel görev listesini sakla
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dailytasks.json");
        string json = JsonSerializer.Serialize(DailyTasks);
        File.WriteAllText(filePath, json);
    }

    private async void OnAddTask()
    {
        // Kullanýcýnýn girdiði görevi almak için bir dialog ekraný aç
        string newTask = await DisplayPromptAsync("Günlük Görev Ekle", "Yeni görevinizi girin:", "Ekle", "Ýptal", null, -1, Keyboard.Default, "");

        // Kullanýcý iptal etmediyse ve bir görev girdiyse, listeye ekle
        if (!string.IsNullOrWhiteSpace(newTask))
        {
            DailyTasks.Add(newTask);
            SaveTasks(); // Görev eklendikten sonra görevleri kaydet
        }
    }

    private async void OnEditTask(string task)
    {
        // Kullanýcýnýn düzenlemek istediði görevi almak için bir dialog ekraný aç
        string editedTask = await DisplayPromptAsync("Görevi Düzenle", "Yeni görevinizi girin:", "Kaydet", "Ýptal", task, -1, Keyboard.Default, "");

        // Kullanýcý iptal etmediyse ve yeni görev boþ deðilse, görevi güncelle
        if (!string.IsNullOrWhiteSpace(editedTask))
        {
            int index = DailyTasks.IndexOf(task);
            DailyTasks[index] = editedTask;
            SaveTasks(); // Görev güncellendikten sonra görevleri kaydet
        }
    }

    private async void OnDeleteTask(string task)
    {
        // Görevi silmek isteyip istemediðini kullanýcýya sor
        bool answer = await DisplayAlert("Sil", "Bu görevi silmek istediðinizden emin misiniz?", "Evet", "Hayýr");

        if (answer)
        {
            // Görevi listeden sil
            DailyTasks.Remove(task);
            SaveTasks(); // Görev silindikten sonra görevleri kaydet
        }
    }

    private void OnSave()
    {
        SaveTasks();
    }

}
