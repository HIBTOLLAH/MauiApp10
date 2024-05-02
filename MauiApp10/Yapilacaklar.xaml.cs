using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace MauiApp10
{
    public partial class Yapilacaklar : ContentPage
    {
        public ObservableCollection<string> DailyTasks { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public Yapilacaklar()
        {
            InitializeComponent();
            DailyTasks = new ObservableCollection<string>();
            LoadTasks();

            AddTaskCommand = new Command(OnAddTask);
            EditTaskCommand = new Command<string>(OnEditTask);
            DeleteTaskCommand = new Command<string>(OnDeleteTask);
            SaveCommand = new Command(OnSave);

            BindingContext = this;
        }

        private void LoadTasks()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dailytasks.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                DailyTasks = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
            }
        }

        private void SaveTasks()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dailytasks.json");
            string json = JsonSerializer.Serialize(DailyTasks);
            File.WriteAllText(filePath, json);
        }

        private async void OnAddTask()
        {
            string newTask = await DisplayPromptAsync("Günlük Görev Ekle", "Yeni görevinizi girin:", "Ekle", "Ýptal", null, -1, Keyboard.Default, "");

            if (!string.IsNullOrWhiteSpace(newTask))
            {
                DailyTasks.Add(newTask);
                SaveTasks();
            }
        }

        private async void OnEditTask(string task)
        {
            string editedTask = await DisplayPromptAsync("Görevi Düzenle", "Yeni görevinizi girin:", "Kaydet", "Ýptal", task, -1, Keyboard.Default, "");

            if (!string.IsNullOrWhiteSpace(editedTask))
            {
                int index = DailyTasks.IndexOf(task);
                DailyTasks[index] = editedTask;
                SaveTasks();
            }
        }

        private async void OnDeleteTask(string task)
        {
            bool answer = await DisplayAlert("Sil", "Bu görevi silmek istediðinizden emin misiniz?", "Evet", "Hayýr");

            if (answer)
            {
                DailyTasks.Remove(task);
                SaveTasks();
            }
        }

        private void OnSave()
        {
            SaveTasks();
        }
    }
}
